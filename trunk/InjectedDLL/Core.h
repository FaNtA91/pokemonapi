#if MSC_VER > 100
#pragma once
#endif
#ifndef _CORE_H_
#define _CORE_H_

#include <string>

enum PipePacketType : unsigned char
{
	PipePacketType_DefaultTemplate = 0x00,
	PipePacketType_HooksEnableDisable = 0x01,
	PipePacketType_SetConstant = 0x02,
	PipePacketType_DisplayText = 0x03,
	PipePacketType_RemoveText = 0x04,
	PipePacketType_RemoveAllText = 0x05,
	PipePacketType_DisplayCreatureText = 0x06,
	PipePacketType_RemoveCreatureText = 0x07,
	PipePacketType_UpdateCreatureText = 0x08,

	PipePacketType_UnloadDll = 0x0D,
	PipePacketType_HookReceivedPacket = 0x0E,
	PipePacketType_HookSentPacket = 0x0F,
	PipePacketType_HookSendToServer = 0x10
};

typedef void _PrintText(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
static _PrintText *PrintText = 0;

typedef int (WINAPI *PRECV)(SOCKET s, char* buf, int len, int flags);
static PRECV OrigRecv = 0;
typedef int (WINAPI *PSEND)(SOCKET s, char* buf, int len, int flags);
static PSEND OrigSend = 0;



void MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
void MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);


int WINAPI MyRecv(SOCKET s, char* buf, int len, int flags);
int WINAPI MySend(SOCKET s,char* buf, int len, int flags);
bool case_insensitive_compare(std::string a, std::string b);

void DisableHooks();
DWORD HookCall(DWORD dwAddress, DWORD dwFunction);
void UnhookCall(DWORD dwAddress, DWORD dwOldCall);
BYTE* Nop(DWORD dwAddress, int size);
void UnNop(DWORD dwAddress, BYTE* OldBytes, int size);

void StartUninjectSelf();
void __declspec(noreturn) UninjectSelf();
void UnloadSelf();

inline void PipeOnRead();
void PipeThreadProc(HMODULE Module);
void CALLBACK ReadFileCompleted(DWORD errorCode, DWORD bytesCopied, OVERLAPPED* overlapped);

void ParseHooksEnableDisable(BYTE *Buffer, int position);
void ParseSetConstant(BYTE *Buffer, int position);
void ParseDisplayText(BYTE *Buffer, int position);
void ParseRemoveText(BYTE *Buffer, int position);
void RemoveAllText();
void ParseDisplayCreatureText(BYTE *Buffer, int position);
void ParseRemoveCreatureText(BYTE *Buffer, int position);
void ParseUpdateCreatureText(BYTE *Buffer, int position);

void ParseHookSendToServer(BYTE *Buffer, int position);

BOOL CALLBACK EnumWindowsProc(HWND hwnd,LPARAM lParam);
LRESULT WINAPI SubClassProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
//LRESULT CALLBACK MouseHookProc(int nCode,WPARAM wParam, LPARAM lParam);
#endif