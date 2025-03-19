// Represents an inventory system for storing items.
// This can be used for the player's backpack or a room's chest.
class Inventory
{
    //! Fields

    // The maximum weight capacity of the inventory.
    private int maxWeight; 

    // A dictionary to store items in the inventory.
    // The key is the item's name (e.g., "key", "sword"), and the value is the Item object.
    private Dictionary<string, Item> items;

    // Constructor
    // Constructor: Initializes the inventory with a specified maximum weight.
    // Parameters:
    public Inventory(int maxWeight) // - maxWeight: The maximum weight capacity of the inventory.
    {
        this.maxWeight = maxWeight; // Set the maximum weight.
        items = new Dictionary<string, Item>();  // Initialize the dictionary to store items.
    }

    //! Methods

    // Calculate the total weight of all items in the inventory.
    // Returns:
    // - The total weight of the items.
    public int TotalWeight()
    {
        int total = 0; // Initialize the total weight.
        foreach (var item in items.Values) // Iterate through all items in the inventory.
        {
            total += item.Weight; // Add each item's weight to the total.
        }
        return total;  // Return the total weight.
    }

    // Calculate the remaining weight capacity of the inventory.
    // Returns:
    // - The free weight available in the inventory.
    public int FreeWeight()
    {
        return maxWeight - TotalWeight(); // Subtract the total weight from the maximum weight.
    }



    // Add an item to the inventory if there is enough free weight.
    // Parameters:
    // - itemName: The name of the item to add.
    // - item: The Item object to add.
    // Returns:
    // - True if the item was successfully added, false otherwise.
    public bool Put(string itemName, Item item)
    {
        if (item.Weight <= FreeWeight()) // Check if the item fits within the free weight.
        {
            items[itemName] = item; // Add the item to the inventory.
            return true; // Indicate success.
        }
        return false;  // Indicate failure if there isn't enough space.
    }


    // Retrieve an item from the inventory and remove it.
    // Parameters:
    // - itemName: The name of the item to retrieve.
    // Returns:
    // - The Item object if it exists, or null if it doesn't.
    public Item Get(string itemName)
    {
        items.TryGetValue(itemName, out Item item); // Try to get the item from the dictionary.
        items.Remove(itemName);  // Remove the item from the inventory.
        return item;  // Return the item (or null if it wasn't found).
    }


    // Remove an item from the inventory without returning it.
    // Parameters:
    // - itemName: The name of the item to remove.
    public void Remove(string itemName)
    {
        items.Remove(itemName); // Remove the item from the dictionary.
    }


    // Display the names of all items in the inventory.
    // Returns:
    // - A comma-separated string of item names, or "Nothing" if the inventory is empty.
    public string ShowInventory()
    {
        if (items.Count == 0) // Check if the inventory is empty.
        {
            return "Nothing"; // Return "Nothing" if there are no items.
        }
        return string.Join(", ", items.Keys);// Join the item names with commas and return the result.
    }


    //! Check if a specific item exists in the inventory.
    // Parameters:
    // - itemName: The name of the item to check.
    // Returns:
    // - True if the item exists, false otherwise.
    public bool HasItem(string itemName)
{
    return items.ContainsKey(itemName); // Check if the item exists in the dictionary.
    // Assuming 'items' is a dictionary storing the inventory
}
}