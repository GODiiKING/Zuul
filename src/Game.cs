using System;
using System.Diagnostics;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	private Stopwatch stopwatch;
	private Room chamber;
	// private Room currentRoom;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
		stopwatch = new Stopwatch();
	}

	// Initialize the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room theAwakeningChamber = new Room("Freezing, prison cell where the air reeks of rot. Chains hang from the walls, some still holding skeletal remains.");

		Room hallOfEchoes = new Room("The grand hall is cold and the echoes of distant screams are almost too faint to notice.");

		Room theShatteredLibrary = new Room("Bookshelves tower over the player, but most have collapsed and the walls are covered in scratched writings. Some books whisper with voices filled with agony and despair.");

		Room theBloodstainedSanctuary = new Room("A altar coated in thick, dried blood. The stained-glass windows reveal twisted, grotesque figures instead of saints. The air is thick with the scent of rotting incense and something far worse.");

		Room theSilentPrison = new Room("Rows of iron-barred cells stretch into the shadows. Faint breathing can be heard, though no one is visible.");

		Room theMirrorChamber = new Room("A grand hallway lined with warped mirrors. The reflections do not match reality, twisting into horrifying versions of the player. Some mirrors show movements that should not exist, as if something is waiting on the other side.");

		Room theAbyssalHallway = new Room("The walls seem to close in with each step, and the deeper the player goes, the more they feel hands brushing against them in the darkness");

		Room theExitHall = new Room("The only known exit, but the door is needs a key, glowing faintly as if warning the player. (You're taking 5 damage per second)");
		chamber = theExitHall;

		// Initialize room exits
		theAwakeningChamber.AddExit("east", hallOfEchoes);
		theAwakeningChamber.AddExit("south", theBloodstainedSanctuary);
		theAwakeningChamber.AddExit("west", theShatteredLibrary);
		theAwakeningChamber.AddExit("down", theMirrorChamber);


		hallOfEchoes.AddExit("west", theAwakeningChamber);
		hallOfEchoes.AddExit("east", theExitHall);
		theExitHall.AddExit("west", hallOfEchoes);


		theShatteredLibrary.AddExit("west", theAwakeningChamber);

		theBloodstainedSanctuary.AddExit("north", theAwakeningChamber);
		theBloodstainedSanctuary.AddExit("east", theSilentPrison);

		theSilentPrison.AddExit("west", theBloodstainedSanctuary);
		theSilentPrison.AddExit("up", theAbyssalHallway);


		theMirrorChamber.AddExit("up", theAwakeningChamber);
		theAbyssalHallway.AddExit("down", theSilentPrison);


		// Create your Items here
		// ...
		// And add them to the Rooms
		// ...

		// startRoom game startRoom
		player.CurrentRoom = theAwakeningChamber;
		Item note = new Item(1, "The Truth You Forgot."); // Truth
		Item note2 = new Item(1, "The Price of Knowing."); // Past
		Item key = new Item(1, "The Light That You Can't Reach."); // Key


		theAwakeningChamber.Chest.Put("truth", note);
		hallOfEchoes.Chest.Put("past", note2);


		theExitHall.Chest.Put("key", key);
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{

			stopwatch.Start();

			Command command = parser.GetCommand();
			OverFlowChamber(command);


			finished = ProcessCommand(command);
			//! if player is NOT alive (!) then finished is true
			if (!player.IsAlive())
			{
				finished = true;
				Console.WriteLine("You died!");
			}
			stopwatch.Reset();
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to 'Tower of Fear Nexus'");

		Console.WriteLine("Crane Ravenlock was once an investigative journalist, known for chasing the darkest stories. But the truth came with a price.");

		Console.WriteLine("The horrors he uncovered never left him. They followed him into his dreams.");

		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());

	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				PrintLook();
				break;
			case "status":
				PrintStatus();
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "use":
				PrintUse(command);
				break;
			case "overFlowChamber":
				break;

		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("Lost and alone.");
		Console.WriteLine("You don`t know if an exit truly exists.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	private void PrintStatus()
	{
		Console.WriteLine("Your Health is: " + player.Health);
		Console.WriteLine("Your backpack contains: " + player.Backpack.ShowInventory());
		Console.WriteLine("You are carrying: " + player.Backpack.TotalWeight() + "kg. You have " + player.Backpack.FreeWeight() + "kg free space.");
	}

// use is the command word.
// key is the second word.
// east is the third word.
private void PrintUse(Command command)
{
    if (!command.HasSecondWord())
    {
        Console.WriteLine("Use what?");
        return;
    }

    string itemName = command.SecondWord;
    string target = command.HasThirdWord() ? command.ThirdWord : null; // Get the third word if it exists

    // Check if the player is in theExitHall and using the key with the correct third word
    if (itemName == "key" && target == "east")
    {
        // Check if the player has the key in their inventory
        if (!player.Backpack.HasItem("key"))
        {
            Console.WriteLine("You don't have the key in your inventory.");
            return;
        }

        if (player.CurrentRoom == chamber) // chamber is theExitHall
        {
            Console.WriteLine("As you use 'key' on the door to the east, it opens, revealing a path to freedom.");
            Console.WriteLine("Congratulations! You have escaped the Tower of Fear Nexus.");
            Console.WriteLine("Press [Enter] to continue.");
            Environment.Exit(0); // End the game
        }
        else
        {
            Console.WriteLine("Doesn't seem to work here.");
        }
    }
    else if (player.Use(itemName) == false)
    {
        Console.WriteLine($"You don't have a {itemName} to use.");
    }
}

	private void PrintLook()
	{
		Console.WriteLine("Items in the room: " + player.CurrentRoom.Chest.ShowInventory());
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{


		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}
		//! Player loses 10 health each time they enter a new room
		player.Damage(10);

		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());

	}

	//methods
	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Take what?");
			return;
		}

		string itemName = command.SecondWord;

		Item item = player.CurrentRoom.Chest.Get(itemName);

		if (item == null)
		{
			Console.WriteLine("There is no " + itemName + " in this room.");
			return;
		}

		switch (itemName)
		{
			case "truth":
				Console.WriteLine("You picked the note: The Truth You Forgot.");
				break;
			case "price":
				Console.WriteLine("You picked the note: The Price of Knowing.");
				break;


			case "key":
				Console.WriteLine("You picked the key: The Light That You Can't Reach.");
				break;

			default:
				Console.WriteLine("You picked up the " + itemName + ".");
				break;
		}

		player.Backpack.Put(itemName, item);

	}

	private void Drop(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Drop what?");
			return;
		}

		string itemName = command.SecondWord;

		Item item = player.Backpack.Get(itemName);
		if (item != null)
		{
			player.CurrentRoom.Chest.Put(itemName, item);
			Console.WriteLine($"You dropped the {itemName}.");
		}
		else
		{
			Console.WriteLine($"You don't have a {itemName} to drop.");
		}
	}

	private void OverFlowChamber(Command command)
	{
		if (player.CurrentRoom == chamber) // Use a proper identifier
		{
			
			stopwatch.Stop();
			int s = stopwatch.Elapsed.Seconds;

			for (int i = 0; i < s; i++)
			{
				player.Damage(5);
			}
			Console.WriteLine("Tower of Fear Nexus is slowly killing you!");


			if (!player.IsAlive())
			{
				Console.WriteLine("You succumbed to the Tower of Fear Nexus!");
			}
		}

	}
}