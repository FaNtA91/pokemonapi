### Table of Contents ###
  * [Get a client object to work with](Examples#Get_a_client_object_to_work_with.md)
  * [Perform name spy or level spy](Examples#Perform_name_spy_or_level_spy.md)
  * [Setup a global keyboard hook](Examples#Setup_a_global_keyboard_hook.md)
  * [Eat a mushroom in your inventory](Examples#Eat_a_mushroom_in_your_inventory.md)
  * [Attack the first rattata found in the battelist](Examples#Attack_the_first_rattata_found_in_the_battelist.md)
  * [Replace all the trees with small fir trees](Examples#Replace_all_the_trees_with_small_fir_trees.md)
  * [Make a simple lighthack](Examples#Make_a_simple_lighthack.md)
  * [Draw players hitpoints in percents above his name](Examples#Draw_players_hitpoints_in_percents_above_his_name.md)
  * [Using Util.Timer](Examples#Using_Util.Timer.md)

### Note ###
All code samples are in C#. You can convert the code samples to VB.NET using this tool: http://www.developerfusion.com/tools/convert/csharp-to-vb/

Most of these samples assume you are using Pokemon and Pokemon.Objects:

```
using Pokemon;
using Pokemon.Objects;
```

## Get a client object to work with ##
```
using Pokemon.Util;

Client client = ClientChooser.ShowBox();
if (client == null)
{
	System.Console.WriteLine("No active client.");
}
```

## Perform name spy or level spy ##
```
// Show names on other floors
client.Map.NameSpyOn();

// Move the screen to the floor below
client.Map.LevelSpyOn(-1);
```

## Setup a global mouse hook ##
The following code will show you how to set up a global mouse hook and capture when the ButtonDown event is raised

```
// Enable mouse hook and set the button down event to a function

if (!MouseHook.Enabled)
{
    MouseHook.Enable();
    MouseHook.ButtonDown = GlobalMouseDown;
}

// The function to handle the button down event
public bool GlobalMouseDown(System.Windows.Forms.MouseButtons button)
{
	if (button == Windows.Forms.MouseButtons.Left) {
		MessageBox.Show("Left mouse button was clicked down.");
	}

	return true;
}
```

## Setup a global keyboard hook ##
Put the following in a button's click event. This example captures the hotkey Ctrl + Alt + A in the client only (doesn't let it go to the client). All other keypresses, client or otherwise, are ignored.

```
if (!KeyboardHook.Enabled)
{
    KeyboardHook.Enable();
    KeyboardHook.Add(Keys.A, new KeyboardHook.KeyPressed(delegate()
    {
        if (client.Window.IsActive)
        {
            if (KeyboardHook.Control && KeyboardHook.Alt)
            {
                string text = "You want to capture the hotkey " +
                Pokemon.KeyboardHook.KeyToString(Keys.A) + " in client!";
                MessageBox.Show(text);
            	return false;
      	     }
        }
      	return true;
    }));
}
else
{
    KeyboardHook.Disable();
}
```

## Eat a mushroom in your inventory ##
```
client.Inventory.UseItem(Pokemon.Constants.Items.Food.WhiteMushroom.Id);
```

## Attack the first rattata found in the battelist ##
```
var rat = client.BattleList.GetCreatures().FirstOrDefault(c => c.Name.Equals("Rattata"));
if (rat != null)
    rat.Attack();
```

## Replace all the trees with small fir trees ##
```
client.Map.ReplaceTrees();
```

## Make a simple lighthack ##
```
Player player=client.GetPlayer();
player.Light = Pokemon.Constants.LightSize.Full;
player.LightColor = Pokemon.Constants.LightColor.White;
```

## Draw players hitpoints in percents above his name ##
```
Player player = client.GetPlayer();
client.Screen.DrawCreatureText(
    player.Id,
    new Location(0, -10, 0),
    Color.Green, 
    Pokemon.Constants.ClientFont.NormalBorder,
    player.HPBar.ToString() + "%"
);
```

## Using Util.Timer ##
```
using Pokemon.Util;

// This code should go in MainForm_Load, for instance
Timer timer = new Timer();
timer.Interval = 1000; // 1 second
timer.Execute += DoSomething;
timer.Start();
 
void DoSomething()
{
    // Make sure you use form.Invoke if you need to modify a form's controls in here
}
```