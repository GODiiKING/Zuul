class Player
{
	//field



	//fields
	public int health { get; set; }
	//auto property
	public Room CurrentRoom { get; set; }
	public Inventory backpack;
	//constructor
	public Player()
	{
		CurrentRoom = null;
		health = 100;

		//100kg because we are strong
		backpack = new Inventory(20);

	}

	//methods

public bool TakeFromChest(string itemName)
{
    // Remove the Item from the Room.
    Item item = CurrentRoom.Chest.Get(itemName);

    if (item == null)
    {
        Console.WriteLine("There is no " + itemName + " in this room.");
        return false;
    }

    // Check if the item fits in the backpack
    if (item.Weight > backpack.FreeWeight())
    {
        Console.WriteLine("You cannot carry the " + itemName + "Because it's too heavy.");
        // Put the item back in the chest
        CurrentRoom.Chest.Put(itemName, item);
        return false;
    }

    // Put it in your backpack
    if (backpack.Put(itemName, item))
    {
        Console.WriteLine("You have picked up the " + itemName);
        return true;
    }
	return false;
}


			public bool DropToChest(string itemName)
			{
				// TODO implement:
				// Remove Item from your inventory.
				// Add the Item to the Room.
				// Inspect returned values.
				// Communicate to the user what's happeni.ng
				// Return true/false for success/failure.


				return false;
			}
	public int Damage(int amount)
	{
		this.health -= amount;
		if (this.health < 0)
		{
			this.health = 0;
		}
		return this.health;
	} // player loses some health

	public int Heal(int amount)
	{

		this.health += amount;
		if (this.health > 100)
		{
			this.health = 100;
		}
		return this.health;


	} // player gains some health

	public bool IsAlive()
	{
		if (this.health == 0)
		{
			// Console.WriteLine("You died, noob! Write 'quit' to exit the game");	
			return false;
		}
		return true;
	} // returns true if player is alive
}