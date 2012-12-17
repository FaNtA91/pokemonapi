#include "stdafx.h"
#include <windows.h>
#include <string>
#include <sstream>
#include <list>
//#include <atlbase.h>
#include <assert.h>
#include "Constants.h"
#include "Core.h"
#include "Packet.h"



#ifdef _MANAGED
#pragma managed(push, off)
#endif

using namespace std;

/* DisplayText. Credits for Displaying text goes to Stiju and Zionz. Thanks for the help!*/

list<NormalText> DisplayTexts;		//Used for normal text displyaing
list<PlayerText> CreatureTexts;		//Used for storing current text to display above creature
DWORD OldPrintName = 0;				//Used for restoring PrintText when uninjecting DLL
DWORD OldPrintFPS = 0;				//Used for restoring PrintFPS when uninjecting DLL
BYTE* OldNopFPS = 0;				//Used for restoring conditional jump (FPS)


//recv/send
DWORD OrigSendAddress = 0;
DWORD OrigRecvAddress = 0;
SOCKET sock = 0;

//Asynchronisation variables
//CHANDLE pipe;						//Holds the Pipe handle (CHandle is from ATL library)
HANDLE pipe;
OVERLAPPED overlapped = { 0 };		
DWORD errorStatus = ERROR_SUCCESS;
bool mustUnload = false;

DWORD CurrentPID;
BOOL fUnicode;
HWND hwndTibia=0;
WNDPROC oldWndProc;

LRESULT WINAPI SubClassProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	if(msg == WM_LBUTTONDOWN)
	{
		POINT pos;
		pos.x=(WORD)lParam;
		pos.y=(WORD)(lParam>>16);
	}
	return fUnicode ? CallWindowProcW(oldWndProc, hwnd, msg, wParam, lParam) : 
					CallWindowProcA(oldWndProc, hwnd, msg, wParam, lParam);
}

int WINAPI MyRecv(SOCKET s, char* buf, int len, int flags)
{	
	//sock=s;
	int bytesCount=OrigRecv(s,buf,len,flags);
	if(bytesCount>0)
	{		
		Packet* packet = new Packet(((WORD)buf)+1);
		packet->AddByte(0x0E);
		for(int i=0;i<bytesCount;i++)
			packet->AddByte((BYTE)buf[i]);
		WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 
	}
	return bytesCount;
}

int WINAPI MySend(SOCKET s,char* buf, int len, int flags)
{
	sock=s;
	if(len>0)
	{
		Packet* packet = new Packet(((WORD)buf)+1);
		packet->AddByte(0x0F);
		for(int i=0;i<len;i++)
			packet->AddByte((BYTE)buf[i]);
		WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 
	}

	return OrigSend(s,buf,len,flags);
}

void MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign)
{
	list<PlayerText>::iterator it;

	//Displaying Original Text
	PrintText(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);

	/* Write own text */

	DWORD *EntityID = (DWORD*)(lpText - 4);

	//Displaying texts
	EnterCriticalSection(&CreatureTextCriticalSection);
	for(it = CreatureTexts.begin(); it != CreatureTexts.end(); ++it) {
		if(it->CreatureId == 0)
		{
			//compare insensitive incase creature name isn't case sensitive (thanks DarkstaR)
			if(case_insensitive_compare(lpText, it->CreatureName))
				PrintText(0x01, nX + it->RelativeX, nY + it->RelativeY, it->TextFont, it->cR, it->cG, it->cB, it->DisplayText, 0x00);
		}
		else if(*EntityID == it->CreatureId)
		{
			PrintText(0x01, nX + it->RelativeX, nY + it->RelativeY, it->TextFont, it->cR, it->cG, it->cB, it->DisplayText, 0x00);
		}
	}
	LeaveCriticalSection(&CreatureTextCriticalSection);
}

void MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign)
{
	bool *fps = (bool*)Consts::ptrShowFPS;
	if(*fps == true)
	{
		PrintText(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);
		//nY += 12; ??????
	}
	
	
	
	EnterCriticalSection(&NormalTextCriticalSection);
	list<NormalText>::iterator ntIT;
	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
		PrintText(0x01, ntIT->x, ntIT->y, ntIT->font, ntIT->r, ntIT->g, ntIT->b, ntIT->text, 0x00); //0x01 Surface, 0x00 Align

	LeaveCriticalSection(&NormalTextCriticalSection);
	
	
}

//thanks/credits to DarkstaR
bool case_insensitive_compare( string a, string b)
{ 
	char char1, char2, blank = ' ' ;   
	int len1 = a.length() ; 
	int len2 = b.length() ;  
	if (len1 != len2) return false ; 
	
	for (int i = 0 ; i < len1 ; ++i )
	{ 
		// get a single character from the current position in the string
		char1 = *(a.substr(i,1).data());
		char2 = *(b.substr(i,1).data()); 
		// make lowercase for compare 
		char1 |= blank;
		char2 |= blank; 
		//Test
		if (char1 == char2) continue; 
		return false; 
	} 
	//Everything matched up, return true
	return true; 
} 

void EnableHooks()
{
	if(HooksEnabled)
	{
		#if _DEBUG
			MessageBoxA(0, "The hook is already injected", "", MB_ICONINFORMATION);
		#endif
		return;
	}

	OldPrintName = HookCall(Consts::ptrPrintName, (DWORD)&MyPrintName);
	OldPrintFPS = HookCall(Consts::ptrPrintFPS, (DWORD)&MyPrintFps);


	//recv/send
	OrigSendAddress=(DWORD)GetProcAddress(GetModuleHandleA("WS2_32.dll"),"send");
	OrigSend=(PSEND)OrigSendAddress;


	OrigRecvAddress=(DWORD)GetProcAddress(GetModuleHandleA("WS2_32.dll"),"recv");
	OrigRecv=(PRECV)OrigRecvAddress;

	OldNopFPS = Nop(Consts::ptrNopFPS, 6); //Showing the FPS all the time..

	//subclassing tibia's main window procedure
	oldWndProc = (WNDPROC) ((fUnicode) ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)SubClassProc) : 
											SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)SubClassProc));

	HooksEnabled = true;
}

void DisableHooks()
{	
	#if _DEBUG
		MessageBoxA(0, "Removing all hooks...", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif
	if (OldPrintName)
		UnhookCall(Consts::ptrPrintName, OldPrintName);
	if (OldPrintFPS)
		UnhookCall(Consts::ptrPrintFPS, OldPrintFPS);
	if (OldNopFPS)
		UnNop(Consts::ptrNopFPS, OldNopFPS, 6);


	//recv/send
	#if _DEBUG
		MessageBoxA(0, "Removing send/recv hooks...", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif

	fUnicode ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc) : 
			SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc);

	HooksEnabled = false;
}

DWORD HookCall(DWORD dwAddress, DWORD dwFunction)
{   
	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	//CALL opcode = 0xE8 <4 byte for distance>
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	//Calculate the distance
	dwNewCall = dwFunction - dwAddress - 5;
	memcpy(&callByte[1], &dwNewCall, 4);

	VirtualProtect((LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect); //Gain access to read/write
	memcpy(&dwOldCall, (LPVOID)(dwAddress+1), 4); //Get the old function address for unhooking
	memcpy((LPVOID)(dwAddress), &callByte, 5); //Hook the function
	VirtualProtect((LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect); //Restore access

	return dwOldCall; //Return old funtion address for unhooking
}

void UnhookCall(DWORD dwAddress, DWORD dwOldCall)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	memcpy(&callByte[1], &dwOldCall, 4);

	VirtualProtect((LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), &callByte, 5);
	VirtualProtect((LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect);
}

BYTE* Nop(DWORD dwAddress, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE* OldBytes;
	VirtualProtect((LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	OldBytes = new BYTE[size];
	memcpy(OldBytes, (LPVOID)(dwAddress), size);
	memset((LPVOID)(dwAddress), 0x90, size);
	VirtualProtect((LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	return OldBytes;
}

void UnNop(DWORD dwAddress, BYTE* OldBytes, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtect((LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), OldBytes, size);
	VirtualProtect((LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	delete [] OldBytes;
	OldBytes = 0;
}

void __declspec(noreturn) UninjectSelf()
{	
	DWORD ExitCode=0;
	__asm
	{
		push hMod
		push ExitCode
		jmp dword ptr [FreeLibraryAndExitThread] 
	}
}

void StartUninjectSelf()
{
	try
	{
		CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)UninjectSelf, hMod, NULL, NULL);
	}
	catch (...)
	{
		#if _DEBUG
			MessageBoxA(0,"StartUninjectSelf -> Unable to uninject from process." ,"PokemonAPI Injected DLL - Fatal Error", MB_ICONERROR | MB_TOPMOST );
		#endif
	}
}

void UnloadSelf()
{
	if(HooksEnabled) 
	{
		//remove all text
		#if _DEBUG
			MessageBoxA(0, "Removing all text...", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
		#endif
		list<NormalText>::iterator ntIT;
		EnterCriticalSection(&NormalTextCriticalSection);
		for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
		{
			delete [] ntIT->text;
			delete [] ntIT->TextName;
		}
		DisplayTexts.clear();
		LeaveCriticalSection(&NormalTextCriticalSection);

		DisableHooks();
	}

	#if _DEBUG
		MessageBoxA(0, "Detaching pipe...", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif

	//pipe.Detach();
	CloseHandle(pipe);
	//::DeleteFileA(PipeName.c_str());
	//TerminateThread(PipeThread, EXIT_SUCCESS);
	#if _DEBUG
		MessageBoxA(0, "Unhooking winproc...", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif



	#if _DEBUG
		MessageBoxA(0, "Deleting critical sections...", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif

	DeleteCriticalSection(&PipeReadCriticalSection);
	DeleteCriticalSection(&NormalTextCriticalSection);
	DeleteCriticalSection(&CreatureTextCriticalSection);

	#if _DEBUG
		MessageBoxA(0, "Uninjecting self...", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif

	StartUninjectSelf();

	#if _DEBUG
		MessageBoxA(0, "Done.", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif
}

inline void PipeOnRead()
{
	int position = 0;
	WORD len = Packet::ReadWord(Buffer, &position);
	BYTE PacketID = Packet::ReadByte(Buffer, &position);

	switch (PacketID){

		case PipePacketType_HooksEnableDisable:
			ParseHooksEnableDisable(Buffer, position);
			break;
		case PipePacketType_SetConstant:
			ParseSetConstant(Buffer, position);
			break;
		case PipePacketType_DisplayText:
			ParseDisplayText(Buffer, position);
			break;
		case PipePacketType_RemoveText:
			ParseRemoveText(Buffer, position);
			break;
		case PipePacketType_RemoveAllText:
			RemoveAllText();
			break;
		case PipePacketType_DisplayCreatureText:
			ParseDisplayCreatureText(Buffer, position);
			break;
		case PipePacketType_RemoveCreatureText:
			ParseRemoveCreatureText(Buffer, position);
			break;
		case PipePacketType_UpdateCreatureText:
			ParseUpdateCreatureText(Buffer, position);
			break;
		case PipePacketType_UnloadDll:
			mustUnload=true;
			break;
		case PipePacketType_HookSendToServer:		
			ParseHookSendToServer(Buffer, position);
			break;
		case PipePacketType_HookReceivedPacket:
		case PipePacketType_HookSentPacket:
			//OUTGOING PACKETS
			break;
		default:
			#if _DEBUG
				MessageBoxA(0, "Unknown PacketType!", "Error!", MB_ICONERROR);
			#endif
			break;
	}
}

void ParseHooksEnableDisable(BYTE *Buffer, int position)
{
	BYTE Enable = Packet::ReadByte(Buffer, &position);
	/* Testing that every constant contains a value */
	if(!Consts::ptrPrintFPS || !Consts::ptrPrintName || !Consts::ptrShowFPS || !Consts::ptrNopFPS || 
		!Consts::ptrSend) 
	{
		#if _DEBUG
			MessageBoxA(0, "Every constant must contain a value before injecting.", "Error", MB_ICONERROR);
		#endif
		return;
	}

	if(Enable) 
	{
		EnableHooks();
	} 
	else 
	{
		if(!HooksEnabled) 
		{
			#if _DEBUG
				MessageBoxA(0, "Enable the function hooks before uninjecting", "Information", MB_ICONINFORMATION);
			#endif
			return;
		}

		DisableHooks();
	}
}

void ParseSetConstant(BYTE *Buffer, int position)
{
	PipeConstantType type = (PipeConstantType)Packet::ReadByte(Buffer, &position);
	DWORD value = Packet::ReadDWord(Buffer, &position);

	switch(type)
	{
	case PrintName:
		Consts::ptrPrintName = value;
		break;
	case PrintFPS:
		Consts::ptrPrintFPS = value;
		break;
	case ShowFPS:
		Consts::ptrShowFPS = value;
		break;
	case PrintTextFunc:
		PrintText = (_PrintText*)value;
		break;
	case NopFPS:
		Consts::ptrNopFPS = value;
		break;

	case Send:
		Consts::ptrSend = value;
		break;
	default:
		break;
	};
}

void ParseDisplayText(BYTE *Buffer, int position)
{
	string TextName = Packet::ReadString(Buffer, &position);
	int PosX = Packet::ReadWord(Buffer, &position);
	int PosY = Packet::ReadWord(Buffer, &position);
	int ColorRed = Packet::ReadWord(Buffer, &position);
	int ColorGreen = Packet::ReadWord(Buffer, &position);
	int ColorBlue = Packet::ReadWord(Buffer, &position);
	int Font = Packet::ReadWord(Buffer, &position);
	string Text = Packet::ReadString(Buffer, &position);

	NormalText NewText;
	NewText.b = ColorBlue;
	NewText.g = ColorGreen;
	NewText.r = ColorRed;
	NewText.x = PosX;
	NewText.y = PosY;
	NewText.font = Font;

	NewText.TextName = new char[TextName.size() + 1];
	NewText.text = new char[Text.size() + 1];

	memcpy(NewText.TextName, TextName.c_str(), TextName.size() + 1);
	memcpy(NewText.text, Text.c_str(), Text.size() + 1);

	EnterCriticalSection(&NormalTextCriticalSection);

	DisplayTexts.push_back(NewText);

	LeaveCriticalSection(&NormalTextCriticalSection);
}

void ParseRemoveText(BYTE *Buffer, int position)
{
	string RemovalTextName = Packet::ReadString(Buffer, &position);
	list<NormalText>::iterator ntIT;
	EnterCriticalSection(&NormalTextCriticalSection);

	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ) 
	{
		if (ntIT->TextName == RemovalTextName)
		{
			delete [] ntIT->TextName;
			delete [] ntIT->text;
			ntIT = DisplayTexts.erase(ntIT);
		} 
		else
			++ntIT;
	}

	LeaveCriticalSection(&NormalTextCriticalSection);

}

void RemoveAllText()
{
	list<NormalText>::iterator ntIT;
	EnterCriticalSection(&NormalTextCriticalSection);

	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
	{
		delete [] ntIT->text;
		delete [] ntIT->TextName;
	}

	DisplayTexts.clear();
	LeaveCriticalSection(&NormalTextCriticalSection);
}

void ParseDisplayCreatureText(BYTE *Buffer, int position)
{
	int Id = Packet::ReadDWord(Buffer, &position);
	string CName = Packet::ReadString(Buffer, &position);
	int nX = Packet::ReadShort(Buffer, &position);
	int nY = Packet::ReadShort(Buffer, &position);
	int ColorR = Packet::ReadWord(Buffer, &position);
	int ColorG = Packet::ReadWord(Buffer, &position);
	int ColorB = Packet::ReadWord(Buffer, &position);
	int TxtFont = Packet::ReadWord(Buffer, &position);
	string Text = Packet::ReadString(Buffer, &position);
	char *lpText = (char*)calloc(Text.size() + 1, sizeof(char));
	char *cText = (char*)calloc(CName.size() + 1, sizeof(char));
	strcpy(lpText, Text.c_str());
	strcpy(cText, CName.c_str());
	PlayerText Creature = {0};
	Creature.cB = ColorB;
	Creature.cG = ColorG;
	Creature.cR = ColorR;
	Creature.CreatureId = Id;
	Creature.DisplayText = lpText;
	Creature.CreatureName = cText;
	Creature.RelativeX = nX;
	Creature.RelativeY = nY;
	Creature.TextFont = TxtFont;

	EnterCriticalSection(&CreatureTextCriticalSection);
	CreatureTexts.push_back(Creature);
	LeaveCriticalSection(&CreatureTextCriticalSection);
}

void ParseRemoveCreatureText(BYTE *Buffer, int position)
{	
	int Id = Packet::ReadDWord(Buffer, &position);
	string Name = Packet::ReadString(Buffer, &position);

	list<PlayerText>::iterator ptIT;
	EnterCriticalSection(&CreatureTextCriticalSection);
	for(ptIT = CreatureTexts.begin(); ptIT != CreatureTexts.end(); ) 
	{
		if (ptIT->CreatureId == 0) 
		{
			if (ptIT->CreatureName == Name) 
			{
				free(ptIT->DisplayText);
				free(ptIT->CreatureName);
				ptIT->DisplayText = 0; //Just to make sure I won't try to free this twice
				ptIT->CreatureName = 0;
				ptIT = CreatureTexts.erase(ptIT);
			} 
			else
				++ptIT;
		} 
		else if (ptIT->CreatureId == Id) 
		{
			free(ptIT->DisplayText);
			free(ptIT->CreatureName);
			ptIT->DisplayText = 0; //Just to make sure I won't try to free this twice
			ptIT->CreatureName = 0;
			ptIT = CreatureTexts.erase(ptIT);
		} 
		else 
			++ptIT;
	}
	LeaveCriticalSection(&CreatureTextCriticalSection);
}

void ParseUpdateCreatureText(BYTE *Buffer, int position)
{
	int ID = Packet::ReadDWord(Buffer, &position);
	string CName = Packet::ReadString(Buffer, &position);
	int PosX = Packet::ReadShort(Buffer, &position);
	int PosY = Packet::ReadShort(Buffer, &position);
	string NewText = Packet::ReadString(Buffer, &position);
	char *lpNewText = (char*)calloc(NewText.size() + 1, sizeof(char));
	char *OldText;
	strcpy(lpNewText, NewText.c_str());

	EnterCriticalSection(&CreatureTextCriticalSection);

	list<PlayerText>::iterator newit;
	for(newit = CreatureTexts.begin(); newit != CreatureTexts.end(); ++newit) 
	{
		if (newit->CreatureId == 0) 
		{
			if (newit->CreatureName == CName && newit->RelativeX == PosX && newit->RelativeY == PosY) 
			{
				OldText = newit->DisplayText;
				strcpy(OldText, "");
				newit->DisplayText = lpNewText;
				free(OldText);
				OldText = 0;
				break;
			}
		}
		else if (newit->CreatureId == ID && newit->RelativeX == PosX && newit->RelativeY == PosY) 
		{
			OldText = newit->DisplayText;
			strcpy(OldText, "");
			newit->DisplayText = lpNewText;
			free(OldText);
			OldText = 0;
			break;
		}
	}

	LeaveCriticalSection(&CreatureTextCriticalSection);
}


void ParseHookSendToServer(BYTE *Buffer, int position)
{			
	int packetLen = Packet::ReadWord(Buffer,&position);
	char* buf =new char[packetLen+2];
	memcpy((LPVOID)buf,(LPVOID)(Buffer+position-2),packetLen+2);

	int ret=OrigSend(sock,buf,packetLen+2,0);	
	delete buf;
}

void PipeThreadProc(HMODULE Module)
{
	//Connect to Pipe
	if (WaitNamedPipeA(PipeName.c_str(), NMPWAIT_WAIT_FOREVER)) 
	{
		//pipe.Attach(::CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL));
		pipe=CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);

		if (pipe == INVALID_HANDLE_VALUE)
		{
			errorStatus = ::GetLastError();
			#if _DEBUG
				MessageBoxA(0, "Pipe connection failed!", "PokemonAPI Injected DLL - Fatal Error", MB_ICONERROR);
			#endif
			UnloadSelf();
			return;
		} 
		else 
		{
			//Pipe is ready. Let's start listening for incoming packets
			PipeConnected = true;
			
			if (!mustUnload)
			{
				if(!::ReadFileEx(pipe, Buffer, sizeof(Buffer), &overlapped, ReadFileCompleted))
				{
					errorStatus = ::GetLastError();					
					#if _DEBUG
						MessageBoxA(0, "Pipe read error!", "PokemonAPI Injected DLL - Fatal Error", MB_ICONERROR);
					#endif
					UnloadSelf();
					return;
				} 
				else 
				{
					while (errorStatus == ERROR_SUCCESS)
					{
						const DWORD sleepResult = ::SleepEx(INFINITE, TRUE);
						assert(WAIT_IO_COMPLETION == sleepResult);
					}
				}
			}
			else
			{
				UnloadSelf();
			}
		}
	} 
	else 
	{
		#if _DEBUG
			MessageBoxA(0, "Failed waiting for pipe, maybe pipe is not ready?", "PokemonAPI Injected DLL - Fatal Error", MB_ICONERROR);
		#endif
	}
}

void CALLBACK ReadFileCompleted(DWORD errorCode, DWORD bytesCopied, OVERLAPPED* overlapped)
{
	errorStatus = errorCode;

	if (errorStatus == ERROR_SUCCESS)
	{
		PipeOnRead();

		if (!mustUnload)
		{
			if (!::ReadFileEx(pipe, Buffer, sizeof(Buffer), overlapped, ReadFileCompleted))
			{
				errorStatus = ::GetLastError();				
				#if _DEBUG
					MessageBoxA(0, "Pipe read error!", "PokemonAPI Injected DLL - Fatal Error", MB_ICONERROR);
				#endif
				UnloadSelf();
			}
		}
		else
		{
			UnloadSelf();
		}
	}
	else
	{
		// pipe disconnected clean everything and remove the hook
		#if _DEBUG
			MessageBoxA(0, "Pipe disconnected, cleaning up.", "PokemonAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
		#endif

		UnloadSelf();
	}
}


BOOL CALLBACK EnumWindowsProc(HWND hwnd,LPARAM lParam)
{
	DWORD PID ;
	DWORD threadID;
	threadID=GetWindowThreadProcessId(hwnd,&PID);
	if(PID==CurrentPID)
	{
		hwndTibia=hwnd;
		//::MessageBoxA(0,"found","",0);
	}
	return hwndTibia ?0:1;
}

extern "C" bool APIENTRY DllMain (HMODULE hModule, DWORD reason, LPVOID reserved)
{
	switch (reason)
	{
		case DLL_PROCESS_ATTACH: //DLL was injected
		{
			hMod = hModule;
			/* Get Current Process ID and use it as Pipename (Pipe is named as PokemonAPI<processID> */
			CurrentPID = GetCurrentProcessId();

			EnumWindows(EnumWindowsProc,0);
			fUnicode=IsWindowUnicode(hwndTibia);

			std::stringstream sout;
			sout << "\\\\.\\pipe\\PokemonApi" << CurrentPID;
			PipeName =  sout.str();
	
			InitializeCriticalSection(&PipeReadCriticalSection);
			InitializeCriticalSection(&NormalTextCriticalSection);
			InitializeCriticalSection(&CreatureTextCriticalSection);

			PipeConnected = false;
			//Start new thread for Pipe
			PipeThread = CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)PipeThreadProc, hMod, NULL, NULL);
			break;
		}
		case DLL_PROCESS_DETACH: //DLL was uninjected
		{
			TerminateThread(PipeThread, EXIT_SUCCESS);
			DeleteCriticalSection(&PipeReadCriticalSection);
			DeleteCriticalSection(&NormalTextCriticalSection);
			DeleteCriticalSection(&CreatureTextCriticalSection);
			
			fUnicode ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc) : 
						SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc);

			break;
		}
	}

	return true;
}
