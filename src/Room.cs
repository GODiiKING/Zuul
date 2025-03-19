using System.Collections.Generic;

//! Represents a room in the game world.
// A room can have a description, exits to other rooms, and a chest for storing items.
class Room
{
    //! Private fields

    // The description of the room (e.g., "in a kitchen" or "in a courtyard").
    public string Description { get; private set; }
    private string description;

    // A dictionary to store the exits of this room.
    // The key is the direction (e.g., "north", "east"), and the value is the neighboring room.
    private Dictionary<string, Room> exits;

    // An inventory object to represent the chest in the room.
    // The chest can hold items with a large capacity.
    private Inventory chest;

    // Constructor: Creates a room with a given description.
    // Initially, the room has no exits and an empty chest.
    public Room(string desc)
    {
        description = desc; // Set the room's description.
        exits = new Dictionary<string, Room>(); // Initialize the exits dictionary.
        chest = new Inventory(10000000); // Initialize the chest with a large capacity.
    }

    // Define an exit for this room.
    // Parameters:
    // - direction: The direction of the exit (e.g., "north").
    // - neighbor: The room that this exit leads to.
    public void AddExit(string direction, Room neighbor)
    {
        exits.Add(direction, neighbor);
    }

    // Return the short description of the room.
    // This is just the description provided when the room was created.
    public string GetShortDescription()
    {
        return description;
    }

    // Return a long description of the room, including its description and exits.
    // Example:
    // "You are in the kitchen.
    //  Exits: north, west"
    public string GetLongDescription()
    {
        string str = ""; // Initialize the description string.
        str += description; // Add the room's description.
        str += ".\n"; // Add a newline for formatting.
        str += GetExitString(); // Add the exits string.
        return str;
    }

    // Return the room that is reached if we go from this room in the given direction.
    // Parameters:
    // - direction: The direction to move (e.g., "north").
    // Returns:
    // - The neighboring room if it exists, or null if there is no exit in that direction.
    public Room GetExit(string direction)
    {
        if (exits.ContainsKey(direction))
        {
            return exits[direction];
        }
        return null;
    }

    // Return a string describing the room's exits.
    // Example:
    // "Exits: north, west"
    private string GetExitString()
    {
        string str = "Exits: "; // Start the string with "Exits: ".
        str += string.Join(", ", exits.Keys); // Join all exit directions with commas.
        return str;
    }

    // Property to access the chest in the room.
    // The chest is an inventory object that can hold items.
    public Inventory Chest
    {
        get { return chest; }
    }
}