class Player
{

	//fields
	//auto property
	public int Health { get; private set; }
	public Room CurrentRoom { get; set; }
	public Inventory Backpack { get; private set; }
	//constructor
	public Player()
	{
		CurrentRoom = null;
		Health = 100;

		//100kg because we are strong
		Backpack = new Inventory(25);

	}

	//methods


	//use the item
	public bool Use(string itemName)
	{
		Item item = Backpack.Get(itemName);

		if (item == null)
		{
			return false;
		}

		switch (itemName)
		{
			case "truth":
				// CurrentRoom.Chest.Put(itemName, item1);
				Console.WriteLine("If you’re reading this, then you’ve finally woken up. You don’t remember, do you? The years you spent chasing shadows, the nightmares that followed you");
				this.Heal(10);
				Backpack.Remove(itemName);
				break;
			case "past":
				// CurrentRoom.Chest.Put(itemName, item2);
				Console.WriteLine("You wanted the truth so badly, but you never realized it would cost you your mind. The things you saw — the things you exposed — weren’t just stories.");
				this.Heal(10);
				Backpack.Remove(itemName);
				break;
			case "key":
				// CurrentRoom.Chest.Put(itemName, item7);
				Console.WriteLine("You’re not just trapped in the Tower of Fear Nexus. You are the tower. And it will never let you go. There is no exit, Crane. There never was.");
				this.Heal(10);
				Backpack.Remove(itemName);
				break;
				// default:
				// return item.Use(); // Call the Use method on the Item instance


				
		}
		return true;
	}
	public bool TakeFromChest(string itemName)
	{
		// Remove the Item from the Room.
		Item item = CurrentRoom.Chest.Get(itemName);

		if (item == null)
		{
			//!This writeline is not needed! Its only here for conveinience
			Console.WriteLine("There is no " + itemName + " in this room.");
			return false;
		}

		// Check if the item fits in the Backpack
		if (item.Weight > Backpack.FreeWeight())
		{
			//!This writeline is not needed! Its only here for conveinience
			Console.WriteLine("You cannot carry the " + itemName + " Because it's too heavy.");
			// Put the item back in the chest
			CurrentRoom.Chest.Put(itemName, item);
			return false;
		}

		// Put it in your Backpack
		if (Backpack.Put(itemName, item))
		{
			//!This writeline is not needed! Its only here for conveinience
			Console.WriteLine("You have picked up the " + itemName);
			return true;
		}
		return false;
	}


	// public bool DropToChest(string itemName)
	// {
	// 	return false;
	// }

	public int Damage(int amount)
	{
		this.Health -= amount;
		if (this.Health < 0)
		{
			this.Health = 0;
		}
		return this.Health;
	} // player loses some Health

	public int Heal(int amount)
	{

		this.Health += amount;
		if (this.Health > 100)
		{
			this.Health = 100;
		}
		return this.Health;


	} // player gains some Health

	public bool IsAlive()
	{
		if (this.Health == 0)
		{
			// Console.WriteLine("You died, noob! Write 'quit' to exit the game");	
			return false;
		}
		return true;
	} // returns true if player is alive
}