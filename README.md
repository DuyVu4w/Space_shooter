# Space Shooter

A Unity-based Space Shooter game featuring dynamic enemy spawning, progressive levels, and optimized performance using design patterns.

## Features

* **Level Progression System**: Multiple levels with a progression system. Levels are locked until the previous one is completed, and progress is saved using `PlayerPrefs`.
* **Dynamic Enemy Spawning**: Implements the **Strategy Design Pattern** to allow different enemy formation spawning strategies (e.g., Line, Square, V-Shape).
* **Performance Optimization**: Utilizes the **Object Pool Pattern** (`ObjectPooler`) to manage frequently created/destroyed objects like Bullets, Enemies, and Explosion VFX, ensuring smooth gameplay without garbage collection spikes.
* **Audio System**: Dedicated `AudioManager` and `UISfxController` to handle background music, in-game sound effects, and UI interactions.
* **Complete UI Flow**: Includes a Main Menu, asynchronous Level Selection menu with scroll views, Options scene, and Game Over/Victory result panels.
* **Clean Architecture**: Makes use of Singletons for managers (`GameController`, `AudioManager`) and modular data management.

## 📁 Project Structure

The codebase is organized modularly under `Assets/Scripts/`:
- **`Spawn/`**: Spawning strategies (`LineSpawnStrategy`, `SquareSpawnStrategy`, etc.).
- **`ObjectPool/`**: Object pooling implementation.
- **`Level/` & `Data/`**: Logic for level handling, data saving, and loading.
- **`UI/`**: Handlers for all UI panels and buttons.
- **`SFX/`**: Audio management scripts.
- **`Enemy/` & `PlayerController.cs`**: Core entities logic.

## 🛠️ Technologies & Patterns Used

* **Game Engine**: Unity
* **Language**: C#
* **Design Patterns**: 
  * Strategy Pattern (Spawning)
  * Object Pool Pattern (Memory management)
  * Singleton Pattern (Global managers)

## 🎮 How to Play

1. Open the project in Unity.
2. Open the **MainMenu** scene located in `Assets/Scenes/MainMenu.unity`.
3. Press Play to start the game! Navigate through the level selection and defeat the incoming enemy fleets.

## 📝 License

This project is for educational and personal development purposes.
