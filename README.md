# Zuul

A boring textadventure.

## How to play

Install the latest [dotnet](https://dotnet.microsoft.com/en-us/download) or latest LTS version.

Open this directory (with the Zuul.csproj file) in the terminal and type:

```
dotnet run
```
# Zuul: Tower of Fear Nexus

![Tower of Fear Nexus](https://github.com/username/repository/raw/branch-name/path/to/image.png)

**Zuul** is a text-based adventure game where you explore the mysterious and eerie **Tower of Fear Nexus**. As the protagonist, **Crane Ravenlock**, you must navigate through rooms, solve puzzles, and uncover the truth while battling the psychological toll of your journey.

---

## **Table of Contents**

- [Zuul](#zuul)
  - [How to play](#how-to-play)
- [Zuul: Tower of Fear Nexus](#zuul-tower-of-fear-nexus)
  - [**Table of Contents**](#table-of-contents)
  - [**Introduction**](#introduction)
  - [**Features**](#features)
  - [**How to Play**](#how-to-play-1)
    - [**Prerequisites**](#prerequisites)
    - [**Running the Game**](#running-the-game)
  - [**Commands**](#commands)
  - [**Game Mechanics**](#game-mechanics)
    - [**Rooms**](#rooms)
    - [**Inventory**](#inventory)
    - [**Health**](#health)
    - [**Special Rooms**](#special-rooms)
  - [**Project Structure**](#project-structure)
  - [**UML Diagram**](#uml-diagram)
  - [**How to Contribute**](#how-to-contribute)
  - [**License**](#license)

---

## **Introduction**

In **Zuul**, you play as **Crane Ravenlock**, a former investigative journalist haunted by the horrors he uncovered. Trapped in the **Tower of Fear Nexus**, you must explore its rooms, collect items, and solve puzzles to escape. But beware—the tower is alive, and it will do everything to keep you trapped.

---

## **Features**

- **Exploration**: Navigate through interconnected rooms, each with unique descriptions and challenges.
- **Inventory Management**: Collect, use, and drop items to progress through the game.
- **Health System**: Manage your health as you face penalties for certain actions.
- **Command-Based Gameplay**: Interact with the game using text commands.
- **Dynamic Rooms**: Each room has its own description, exits, and items to discover.
- **Time-Based Challenges**: Certain rooms, like the Exit Hall, impose time-based penalties.

---

## **How to Play**

### **Prerequisites**

1. Install the latest [.NET SDK](https://dotnet.microsoft.com/en-us/download) (or the latest LTS version).
2. Clone or download this repository to your local machine.

### **Running the Game**

1. Open a terminal and navigate to the project directory (where the `Zuul.csproj` file is located).
2. Run the following command to start the game:
   ```bash
   dotnet run
   ```

---

## **Commands**

The game uses a command-based system. Here are the available commands:

| Command   | Description                                                                 |
|-----------|-----------------------------------------------------------------------------|
| `help`    | Displays a list of valid commands.                                          |
| `go <direction>` | Moves the player to another room in the specified direction (e.g., `go north`). |
| `look`    | Displays the items in the current room's chest.                             |
| `status`  | Shows the player's current health and inventory status.                     |
| `take <item>` | Picks up an item from the room's chest and adds it to your backpack.    |
| `drop <item>` | Drops an item from your backpack into the room's chest.                |
| `use <item>` | Uses an item from your backpack.                                         |
| `quit`    | Ends the game.                                                              |

---

## **Game Mechanics**

### **Rooms**
- Each room has a unique description, exits, and a chest for storing items.
- Use the `go <direction>` command to move between rooms.

### **Inventory**
- The player has a backpack with a limited weight capacity.
- Use the `take`, `drop`, and `use` commands to manage your inventory.

### **Health**
- The player starts with 100 health points.
- Certain actions, like entering specific rooms, reduce health.
- Use healing items to restore health.

### **Special Rooms**
- **Exit Hall**: This room imposes a time-based penalty, reducing your health by 5 points per second. Use the key to escape.

---

## **Project Structure**

The project is organized as follows:

```
src/
├── Command.cs          # Represents a command entered by the player.
├── CommandLibrary.cs   # Manages and validates valid commands.
├── Game.cs             # Main game logic, including the game loop and command processing.
├── Inventory.cs        # Represents an inventory system for storing items.
├── Item.cs             # Represents individual items in the game.
├── Parser.cs           # Parses player input into commands.
├── Player.cs           # Represents the player, including health and inventory.
├── Room.cs             # Represents rooms in the game world.
Program.cs              # Entry point of the application.
README.md               # Documentation for the project.
```

---

## **UML Diagram**

The following UML diagram represents the structure of the project, including the relationships between the main classes:

![UML Diagram](UML.graphml)

You can open the `UML.graphml` file in a tool like [yEd Graph Editor](https://www.yworks.com/products/yed) to view and edit the diagram.

---

---

## **How to Contribute**

We welcome contributions to improve **Zuul**! Here's how you can help:

1. Fork the repository.
2. Create a new branch for your feature or bug fix:
   ```bash
   git checkout -b feature-name
   ```
3. Make your changes and commit them:
   ```bash
   git commit -am "Add feature-name"
   ```
4. Push your changes to your fork:
   ```bash
   git push origin feature-name
   ```
5. Open a pull request to the main repository.

---

## **License**

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

Enjoy exploring the **Tower of Fear Nexus** and uncovering its secrets!