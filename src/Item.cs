// Represents an item that can be found in the game.
public class Item //! Items can be carried by the player or stored in a room's chest.
{
    //! Fields

    //! The weight of the item.
    // Key Properties
    // This determines how much space the item takes in the player's inventory.
    // For example, heavier items may prevent the player from carrying additional items.
    public int Weight { get; }

    //! A description of the item.
    // Key Properties
    // This provides details about the item, such as its name or purpose.
    // Example: "A rusty key" or "A shiny golden sword."
    public string Description { get; }

    //! Constructor: Initializes an item with a specified weight and description.
    // Parameters:
    // - weight: The weight of the item.
    // - description: A brief description of the item.
    //! Constructor
    public Item(int weight, string description)
    {
        // This value is immutable after the item is created.
        Weight = weight; // Set the item's weight.
        // This value is also immutable after the item is created.
        Description = description; // Set the item's description.
    }
    //! Constructor
}