//! Represents the player in the game.
// The Player class manages the player's health, current room, inventory, and actions.
class Player
{

	// !fields
	//auto property

	//* The player's current health.
    // Health starts at 100 and decreases when the player takes damage.
	public int Health { get; private set; }

	//* The room the player is currently in.
    // This is updated as the player moves between rooms.
	public Room CurrentRoom { get; set; }

	// *The player's inventory, represented as a backpack.
    // The backpack has a limited weight capacity.
	public Inventory Backpack { get; private set; }
	
	//* Constructor: Initializes the player's attributes.
	public Player()
	{
		CurrentRoom = null;  // The player starts with no assigned room.
		Health = 100; // The player starts with full health (100).

		// Initialize the backpack with a weight capacity of 25.
		Backpack = new Inventory(25);

	}

	//methods

	//* Allows the player to use an item from their backpack.
    // Parameters:
    // - itemName: The name of the item to use.
    // Returns:
    // - True if the item was successfully used, false otherwise.
	public bool Use(string itemName)
	{

		// Retrieve the item from the backpack.
		Item item = Backpack.Get(itemName); 

		if (item == null) // If the item is not found, 
		{
			return false; // return false.
		}

		// Handle specific item usage based on the item's name.
		switch (itemName)
		{
			case "truth":
				//! CurrentRoom.Chest.Put(itemName, item1);
				Console.WriteLine("If you’re reading this, then you’ve finally woken up. You don’t remember, do you? The years you spent chasing shadows, the nightmares that followed you");
				this.Heal(10); // Heal the player by 10 points.
				Backpack.Remove(itemName); // Remove the item from the backpack.
				break;
			case "past":
				//! CurrentRoom.Chest.Put(itemName, item2);
				Console.WriteLine("You wanted the truth so badly, but you never realized it would cost you your mind. The things you saw — the things you exposed — weren’t just stories.");
				this.Heal(10); // Heal the player by 10 points.
				Backpack.Remove(itemName); // Remove the item from the backpack.
				break;
			case "key":
				//! CurrentRoom.Chest.Put(itemName, item3);
				Console.WriteLine("You’re not just trapped in the Tower of Fear Nexus. You are the tower. And it will never let you go. There is no exit, Crane. There never was.");
				this.Heal(10); // Heal the player by 10 points.
				Backpack.Remove(itemName); // Remove the item from the backpack.
				break;

				// Additional cases for other items can be added here.
				
		}
		return true; // Indicate that the item was successfully used.
	}



	//* Allows the player to take an item from the current room's chest.
    // Parameters:
    // - itemName: The name of the item to take.
    // Returns:
    // - True if the item was successfully taken, false otherwise.
	public bool TakeFromChest(string itemName)
	{
		// Remove the item from the Room.
		// Retrieve the item from the room's chest.
		Item item = CurrentRoom.Chest.Get(itemName);

		// If the item is not found, display a message and return false.
		if (item == null)
		{
			Console.WriteLine("There is no " + itemName + " in this room."); // display a message
			return false; // return false
		}

		// Check if the item fits in the Backpack
		if (item.Weight > Backpack.FreeWeight())
		{
			Console.WriteLine("You cannot carry the " + itemName + " Because it's too heavy."); // display a message
			// Put the item back in the chest
			CurrentRoom.Chest.Put(itemName, item);
			return false; // return false
		}

		// Put it in your Backpack
		// Add the item to the backpack.
		if (Backpack.Put(itemName, item))
		{
			Console.WriteLine("You have picked up the " + itemName); // display a message
			return true; // return true
		}
		return false; // Indicate failure if the item could not be added.
	}



	//* Reduces the player's health by a specified amount.
    // Parameters:
    // - amount: The amount of damage to apply.
    // Returns:
    // - The player's updated health.
	public int Damage(int amount)
	{
		this.Health -= amount; // Subtract the damage from the player's health.
		if (this.Health < 0)
		{
			this.Health = 0; // Ensure health does not go below 0.
		}
		return this.Health;
	} // player loses some Health



	//* Increases the player's health by a specified amount.
    // Parameters:
    // - amount: The amount of health to restore.
    // Returns:
    // - The player's updated health.
	public int Heal(int amount)
	{

		this.Health += amount; // Add the healing amount to the player's health.
		if (this.Health > 100)
		{
			this.Health = 100; // Ensure health does not exceed 100.
		}
		return this.Health;


	} // player gains some Health



	//* Checks if the player is still alive.
    // Returns:
    // - True if the player's health is greater than 0, false otherwise.
	public bool IsAlive()
	{
		if (this.Health == 0)
		{
			return false;
		}
		return true;
	} // returns true if player is alive
}