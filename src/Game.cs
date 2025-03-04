using System;

class Game
{
	// Private fields
	private Parser parser;
	private Room currentRoom;
	private Player player; // private Room currentRoom; //! Phase 1

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player(); //! Phase 1
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		// New rooms //! 04/03/2025
		Room basement = new Room("in the basement");
		Room attic = new Room("in the attic");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		attic.AddExit("down", outside); //! 04/03/2025
		basement.AddExit("up", outside); //! 04/03/2025

		// Create your Items here
		// ...
		// And add them to the Rooms
		// ...

		// Start game outside
		currentRoom = outside;
		player.CurrentRoom = outside; //! Phase 1
		Item mousetail = new Item(1, "Why did you even pick this up? Pretty gross if u ask me");

		outside.AddItem(mousetail); //! 04/03/2025
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
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(currentRoom.GetLongDescription());
		Console.WriteLine(player.CurrentRoom.GetLongDescription()); //! Phase 1
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
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
				case "look": //! Phase 1
				PrintLook();
				break;
				case "status": //! 04/03/2025
				PrintStatus();
				break;
			case "take": //! 04/03/2025
				Take(command);
				break;
			case "drop": //! 04/03/2025
				Drop(command);
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
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	private void PrintStatus()
	{
		//Console.WriteLine("Your health is: " + player.health);
		Console.WriteLine("Your backpack contains: "+ player.backpack.showInventory());
		// player.backpack.PrintItems();
	}
	private void PrintLook() //! Phase 1
	{
		Console.WriteLine("There are no items in this area...");
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction); //! Phase 1
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		currentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription()); //! Phase 1
	}

	//methods //! 04/03/2025
	private void Take(Command command)
	{
		//TODO implement

		Console.WriteLine("You have picked up" );

	}

	private void Drop(Command command)
	{
		//TODO implement
		Console.WriteLine("You have dropped the item");
	}
}
