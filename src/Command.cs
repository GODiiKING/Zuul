//! Represents a command entered by the player.
// A command consists of up to three words: the main command, a second word, and an optional third word.
class Command
{
    // Key Properties
    // The main command word (e.g., "go", "take", "use").
    public string CommandWord { get; init; }

    // Key Properties
    // The second word of the command (e.g., "north", "key").
    public string SecondWord { get; init; }

    // Key Properties
    // The third word of the command (optional, e.g., "door").
    public string ThirdWord { get; init; }

    // Constructor: Creates a Command object.
    // Parameters:
    // - first: The main command word (can be null if the command is invalid).
    // - second: The second word of the command (can be null if not provided).
    // - third: The third word of the command (can be null if not provided).
    public Command(string first, string second, string third)
    {
        CommandWord = first;   // Set the main command word.
        SecondWord = second;   // Set the second word.
        ThirdWord = third;     // Set the third word.
    }


    // Key Methods:
    // Checks if the command is unknown (i.e., the main command word is null).
    // Returns:
    // - True if the command is invalid or not recognized, false otherwise.
    public bool IsUnknown()
    {
        return CommandWord == null;
    }


    // Key Methods:
    // Checks if the command has a second word.
    // Returns:
    // - True if the second word is not null, false otherwise.
    public bool HasSecondWord()
    {
        return SecondWord != null;
    }


    // Key Methods:
    // Checks if the command has a third word.
    // Returns:
    // - True if the third word is not null, false otherwise.
    public bool HasThirdWord()
    {
        return ThirdWord != null;
    }
}