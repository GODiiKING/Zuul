using System.Collections.Generic;

// Represents a library of valid commands for the game.
// This class is responsible for managing and validating the commands that the player can use.
class CommandLibrary
{
    // A list that holds all valid command words.
    // This ensures that only predefined commands are recognized by the game.
    private readonly List<string> validCommands;

    // Constructor: Initializes the list of valid command words.
    public CommandLibrary()
    {
        validCommands = new List<string>(); // Initialize the list.

        // Add all valid commands to the list.
        // These are the commands that the player can use in the game.
        validCommands.Add("help");   // Displays help information.
        validCommands.Add("go");     // Moves the player to another room.
        validCommands.Add("quit");   // Quits the game.
        validCommands.Add("look");   // Provides a description of the current room.
        validCommands.Add("status"); // Displays the player's current status.
        validCommands.Add("take");   // Allows the player to pick up an item.
        validCommands.Add("drop");   // Allows the player to drop an item.
        validCommands.Add("use");    // Allows the player to use an item.
    }


    // Key Methods:
    // Checks whether a given string is a valid command word.
    // Parameters:
    // - instring: The command word to check.
    // Returns:
    // - True if the command is valid, false otherwise.
    public bool IsValidCommandWord(string instring)
    {
        return validCommands.Contains(instring); // Check if the command exists in the list.
    }


    // Key Methods:
    // Returns a list of valid command words as a comma-separated string.
    // This is useful for displaying all available commands to the player.
    // Example: "help, go, quit, look, status, take, drop, use"
    public string GetCommandsString()
    {
        return String.Join(", ", validCommands); // Join the commands with commas and return the result.
    }
}