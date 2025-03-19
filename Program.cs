//! The entry point of the application
// This is where the program starts execution
class Program
{
    public static void Main(string[] args)
    {
        // Create an instance of the Game class.
        // The Game constructor initializes the game world, including:
        // - Setting up rooms and their connections.
        // - Adding items to rooms or the player's inventory.
        // - Initializing the player and other game elements.
        Game game = new Game(); //! The Game constructor is responsible for initializing the game world. 

        /// Start the game loop.
        // The Play method contains the main game logic, which:
        //! - Continuously prompts the player for commands.
        // - Processes those commands (e.g., moving between rooms, using items).
        // - Ends the game when the player wins, loses, or quits.
        game.Play();
    }
}

// The following lines are placeholders for Git commands and comments:
// These are not part of the program logic but may be used for version control purposes.

// git remote add origin https://github.com/GODiiKING/Zuul.git
// Adds a remote repository to the local Git repository

//* git commit -am ""
// Commits all changes with a message (currently empty)

//* git push origin master
// Pushes the committed changes to the master branch of the remote repository