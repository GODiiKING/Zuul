using System;

//! The Parser class is responsible for reading and interpreting user input.
// It converts the player's input into a Command object that the game can process.
class Parser
{
    // Holds all valid command words.
    // This ensures that only predefined commands are recognized by the parser.
    private readonly CommandLibrary commandLibrary;

    // Constructor: Initializes the parser and its command library.
    public Parser()
    {
        commandLibrary = new CommandLibrary(); // Create a new CommandLibrary instance.
    }

    // Reads and interprets the user input. Returns a Command object.
    // This method splits the input into up to three words and validates the first word.
    // Returns:
    // - A Command object containing the parsed words if the command is valid.
    // - A "null" Command object if the command is invalid.
    public Command GetCommand()
    {
        Console.Write("> "); // Print a prompt for the player to enter a command.

        string word1 = null; //? The first word of the command (e.g., "go").
        string word2 = null; //? The second word of the command (e.g., "north").
        string word3 = null; //? The third word of the command (optional).

        // Read the player's input and split it into words.
        string[] words = Console.ReadLine().Split(' '); // Split input by spaces.
        if (words.Length > 0) { word1 = words[0]; } // Assign the first word if it exists.
        if (words.Length > 1) { word2 = words[1]; } // Assign the second word if it exists.
        if (words.Length > 2) { word3 = words[2]; } // Assign the third word if it exists.

        // Check if the first word is a valid command.
        if (commandLibrary.IsValidCommandWord(word1))
        {
            // If valid, create and return a Command object with the parsed words.
            return new Command(word1, word2, word3);
        }

        // If the first word is not valid, return a "null" Command object.
        return new Command(null, null, null);
    }

    // Prints a list of valid command words from the command library.
    // This is useful for displaying the available commands to the player.
    public void PrintValidCommands()
    {
        Console.WriteLine("Your command words are:"); // Print a header.
        Console.WriteLine(commandLibrary.GetCommandsString()); // Print the list of valid commands.
    }
}