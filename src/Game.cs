using System;
using System.Diagnostics;

//! Represents the main game logic.
// The Game class initializes the game world, processes player commands, and manages the game loop.
class Game
{
	//! Private fields

	// The parser for interpreting player commands.
	private Parser parser;

	// The player object, which tracks the player's state (e.g., health, inventory, current room).
	private Player player;

    // A stopwatch to track time spent in certain rooms (e.g., the Exit Hall).
	private Stopwatch stopwatch;

	// A reference to the Exit Hall room, where special logic applies.
	private Room chamber;

	// Constructor
	//! Constructor: Initializes the game.
	public Game()
	{
		parser = new Parser(); // Create a new parser for handling commands.
		player = new Player(); // Create a new player.
		CreateRooms(); // Initialize the rooms and items in the game.
		stopwatch = new Stopwatch(); // Initialize the stopwatch for tracking time.
	}

	// Initialize the Rooms (and the Items)
	// Initialize the rooms and items in the game world.
	private void CreateRooms()
	{
		// Create the rooms
		// Create the rooms with descriptions //! (Room 1).
		Room theAwakeningChamber = new Room("Freezing, prison cell where the air reeks of rot. Chains hang from the walls, some still holding skeletal remains.");
		// Create the rooms with descriptions //! (Room 2).
		Room hallOfEchoes = new Room("The grand hall is cold and the echoes of distant screams are almost too faint to notice.");
		// Create the rooms with descriptions (Room 4).
		Room theShatteredLibrary = new Room("Bookshelves tower over the player, but most have collapsed and the walls are covered in scratched writings. Some books whisper with voices filled with agony and despair.");
		// Create the rooms with descriptions (Room 5).
		Room theBloodstainedSanctuary = new Room("A altar coated in thick, dried blood. The stained-glass windows reveal twisted, grotesque figures instead of saints. The air is thick with the scent of rotting incense and something far worse.");
		// Create the rooms with descriptions (Room 6).
		Room theSilentPrison = new Room("Rows of iron-barred cells stretch into the shadows. Faint breathing can be heard, though no one is visible.");
		// Create the rooms with descriptions (Room 7).
		Room theMirrorChamber = new Room("A grand hallway lined with warped mirrors. The reflections do not match reality, twisting into horrifying versions of the player. Some mirrors show movements that should not exist, as if something is waiting on the other side.");
		// Create the rooms with descriptions (Room 8).
		Room theAbyssalHallway = new Room("The walls seem to close in with each step, and the deeper the player goes, the more they feel hands brushing against them in the darkness");
		// Create the rooms with descriptions //! (Room 3 + Endgame + Stopwatch).
		Room theExitHall = new Room("The only known exit, and there is a door in the east of the room that need a key, glowing faintly as if warning the player. (You're taking 5 damage per second)"); //! (You're taking 5 damage per second)
		chamber = theExitHall; // Assign the Exit Hall to the chamber field for special logic.

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
		// Create items and add them to rooms.
		player.CurrentRoom = theAwakeningChamber; // Set the starting room for the player.
		Item note = new Item(1, "The Truth You Forgot."); // Truth //! (Room 1). (weight: 1kg)
		Item note2 = new Item(1, "The Price of Knowing."); // Past //! (Room 2). (weight: 1)
		Item key = new Item(1, "The Light That You Can't Reach."); // Key //! (Room 3 + Endgame). (weight: 1kg)


		theAwakeningChamber.Chest.Put("truth", note); //! (Room 1).
		hallOfEchoes.Chest.Put("past", note2); //! (Room 2).
		//! (Room 3 + Endgame).
		theExitHall.Chest.Put("key", key);
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome(); // Display the welcome message.

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false; // Flag to track if the game should end.
		while (!finished)
		{

			stopwatch.Start(); // Start the stopwatch for tracking time in the Exit Hall. //! (Only room 3)

			Command command = parser.GetCommand(); // Get the player's command.
			OverFlowChamber(command); // Apply special logic for the Exit Hall.


			finished = ProcessCommand(command); // Process the command and check if the game should end.
			//! if player is NOT alive (!) then finished is true
			if (!player.IsAlive())  // Check if the player has died.
			{
				finished = true;
				Console.WriteLine("You died!");
			}
			stopwatch.Reset(); // Reset the stopwatch after processing the command.
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine("Welcome to 'Tower of Fear Nexus'");

		Console.WriteLine("Crane Ravenlock was once an investigative journalist, known for chasing the darkest stories. But the truth came with a price.");

		Console.WriteLine("The horrors he uncovered never left him. They followed him into his dreams.");

		Console.WriteLine("Type 'help' if you need help.");
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
	// Check if the command has a second word (the name of the item to use).
    if (!command.HasSecondWord())
    {
        Console.WriteLine("Use what?"); // Prompt the player to specify an item.
        return; // Exit the method if no second word is provided.
    }

 	// Get the name of the item to use from the command's second word.
    string itemName = command.SecondWord;
    string target = command.HasThirdWord() ? command.

	// Get the target (third word) if it exists, otherwise set it to null.
	ThirdWord : null; // Get the third word if it exists

    // Check if the player is in theExitHall and using the key with the correct third word
    if (itemName == "key" && target == "east")
    {
        // Check if the player has the key in their inventory
        if (!player.Backpack.HasItem("key"))
        {
            Console.WriteLine("You don't have the key in your inventory."); // Inform the player.
            return; // Exit the method if the key is not in the inventory.
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

		// Check if the command has a second word (the direction to go).
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return; // Exit the method if no direction is provided.
		}

		// Get the direction from the command's second word.
		string direction = command.SecondWord;

		// Try to go to the next room.
		// Try to get the next room in the specified direction.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		// Check if there is a room in the specified direction.
		if (nextRoom == null)
		{
			// If no room exists in that direction, inform the player.
			Console.WriteLine("There is no door to " + direction + "!");
			return;  // Exit the method if no room is found.
		}

		// Apply a penalty: The player loses 10 health each time they enter a new room.
		//! Player loses 10 health each time they enter a new room................................................................................................
		player.Damage(10);
		// Moving the player cause (10) damage.
		player.CurrentRoom = nextRoom;
		// Display the description of the new room
		Console.WriteLine(player.CurrentRoom.GetLongDescription());

	}

	//methods
	// Check if the command has a second word (the name of the item to take).
	private void Take(Command command)
{
    // Check if the command has a second word (the name of the item to take).
    if (!command.HasSecondWord())
    {
        Console.WriteLine("Take what?"); // Prompt the player to specify an item.
        return; // Exit the method if no second word is provided.
    }

    // Get the name of the item to take from the command's second word.
    string itemName = command.SecondWord;

    // Delegate the logic to the Player.TakeFromChest method.
    // This method handles retrieving the item from the room's chest and adding it to the player's backpack.
    if (!player.TakeFromChest(itemName))
    {
        // The Player.TakeFromChest method already handles messages, so no need to add anything here.
        return; // Exit the method if the item could not be taken.
    }

		// Handle special cases for specific items.
		switch (itemName)
		{
			case "truth": // Inform the player about the item.
				Console.WriteLine("You picked the note: The Truth You Forgot.");
				break;
			case "price": // Inform the player about the item.
				Console.WriteLine("You picked the note: The Price of Knowing.");
				break;


			case "key": // Inform the player about the item.
				Console.WriteLine("You picked the key: The Light That You Can't Reach.");
				break;
			// For all other items, display a generic message.
			default:
				Console.WriteLine("You picked up the " + itemName + ".");
				break;
		}
		// Add the item to the player's backpack.
		// player.Backpack.Put(itemName, item);

	}

	

	private void Drop(Command command)
	{
		// Check if the command has a second word (the name of the item to drop).
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Drop what?"); // Prompt the player to specify an item.
			return; // Exit the method if no second word is provided.
		}
		// Get the name of the item to drop from the command's second word.
		string itemName = command.SecondWord;

		// Attempt to retrieve the item from the player's backpack.
		Item item = player.Backpack.Get(itemName);
		if (item != null) // Check if the item exists in the player's backpack.
		{
			// If the item exists, place it in the current room's chest.
			player.CurrentRoom.Chest.Put(itemName, item);
			// Inform the player that the item has been dropped.
			Console.WriteLine($"You dropped the {itemName}.");
		}
		else
		{
			// If the item does not exist in the backpack, inform the player.
			Console.WriteLine($"You don't have a {itemName} to drop.");
		}
	}

	//! Special logic for the Exit Hall room.
	// This method is called when the player is in the Exit Hall room.
	private void OverFlowChamber(Command command)
	{
		// Check if the player is currently in the Exit Hall (chamber).
		if (player.CurrentRoom == chamber) // Use a proper identifier
		{
			// Stop the stopwatch to calculate the time spent in the Exit Hall.
			stopwatch.Stop(); 
			int s = stopwatch.Elapsed.Seconds; // Get the elapsed time in seconds. 
			// For each second spent in the Exit Hall, apply 5 damage to the player.
			for (int i = 0; i < s; i++)
			{
				player.Damage(5); //! Apply 5 damage per second spent in the Exit Hall.
			}
			// Inform the player that the room is causing damage.
			Console.WriteLine("Tower of Fear Nexus is slowly killing you!");

			// Check if the player's health has dropped to 0 or below.
			if (!player.IsAlive())
			{
				// Inform the player that they have succumbed to the damage. (not needed)
				Console.WriteLine("You`re succumbing to the Tower of Fear Nexus!");
			}
		}

	}
}