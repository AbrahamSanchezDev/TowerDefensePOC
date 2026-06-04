# Tower Defense POC

## 📋 Project Summary

**Tower Defense Proof of Concept** is a tower defense game prototype developed as a code challenge for a job interview to demonstrate clean architecture, organized code structure, and effective game development practices using Unity.

**Development Timeline:** November 25-27, 2023 (~5 hours development)  
**Challenge Objective:** Build a functional tower defense game showcasing code quality, scalability, and professional development patterns.

WebGL BuildLink https://abrahamsanchezdev.github.io/TowerDefensePOC/
---

## 🎮 Project Overview

This is a fully functional Tower Defense game prototype where players defend against waves of enemies by strategically placing and managing defensive towers (Summons). The project demonstrates:

- **Clean Architecture:** Separation of concerns with distinct systems for gameplay, UI, AI, and data management
- **Scalable Design:** Modular systems that can be easily extended with new enemies, towers, and game mechanics
- **Professional Patterns:** Implementation of design patterns including Singletons, Event Systems, and Scriptable Objects
- **Game State Management:** Robust state machine for handling game states (Idle, Playing, Pause, Win, Lost)
- **Responsive UI:** Dynamic UI system that updates in real-time based on game events

### Key Features

✅ **Tower Placement System** - Place defensive towers strategically on a grid  
✅ **Enemy Wave System** - Progressive enemy spawning with AI pathfinding  
✅ **Resource Management** - Player currency system for purchasing towers  
✅ **Visual Feedback** - Animations and visual effects for gameplay clarity  
✅ **Audio System** - Environmental and action-based sound effects  
✅ **Level Progression** - Multiple levels with increasing difficulty  
✅ **Win/Lose Conditions** - Clear victory and defeat mechanics  

---

## 🎨 Preview

![Tower Defense POC - Gameplay Preview](preview_1.png)

---
## 📦 Project Structure

```
TowerDefensePOC/
├── Assets/
│   ├── Scripts/                    # Core C# gameplay code
│   │   ├── Behaviors/              # Behavior tree implementations
│   │   ├── Data/                   # Game data structures & databases
│   │   │   ├── IDamageable.cs      # Damage interface
│   │   │   ├── SummonDb.cs         # Tower database
│   │   │   ├── SummonData.cs       # Tower data definition
│   │   │   └── PrefabsRef.cs       # Prefab references
│   │   ├── Enemies/                # Enemy AI & control systems
│   │   │   ├── EnemyAI.cs          # AI pathfinding logic
│   │   │   ├── EnemyControl.cs     # Enemy spawning & management
│   │   │   └── MovementController.cs
│   │   ├── Summons/                # Tower/Summon systems
│   │   │   └── SummonAI.cs         # Tower attack behavior
│   │   ├── Player/                 # Player control & towers
│   │   │   ├── PlayerControl.cs    # Player tower management
│   │   │   └── SummonSlot.cs       # Tower placement slots
│   │   ├── UI/                     # User interface systems
│   │   │   ├── GameCanvasUi.cs     # Main UI canvas
│   │   │   ├── GameStatusUi.cs     # Game status display
│   │   │   └── SummonButtonUi.cs   # Tower purchase buttons
│   │   ├── PlayerInputs/           # Input handling
│   │   ├── Selectable/             # Interactive object selection
│   │   ├── Visuals/                # Visual effect systems
│   │   ├── GameControl.cs          # Game state manager (Singleton)
│   │   ├── LevelControl.cs         # Level management
│   │   └── MainSetup.cs            # Scene initialization
│   ├── Prefabs/                    # Reusable game objects
│   ├── Scenes/                     # Game scenes
│   ├── Resources/                  # Runtime-loaded assets
│   ├── Art/                        # Sprites & visual assets
│   ├── GameInfo/                   # Game configuration
│   └── TextMesh Pro/               # Text rendering assets
├── ProjectSettings/                # Unity project configuration
├── Packages/                       # Project dependencies
└── README.md                       # This file
```

---

## 🏗️ Architecture Highlights

### Game State Management
- **Singleton Pattern:** `GameControl` manages global game state
- **State Machine:** Handles Idle, Playing, Pause, Win, and Lost states
- **Event System:** UnityEvents for state transitions and gameplay events

### Systems Design

| System | Responsibility |
|--------|-----------------|
| **GameControl** | Global game state and event coordination |
| **MainSetup** | Scene initialization and component creation |
| **PlayerControl** | Tower management and player actions |
| **EnemyControl** | Enemy spawning and wave management |
| **LevelControl** | Level progression and difficulty |
| **UI System** | Real-time HUD and game status display |
| **PlayerInputs** | User input handling and tower placement |
| **SummonAI** | Tower attack behavior and targeting |
| **EnemyAI** | Enemy pathfinding and movement |

### Data Management
- **Scriptable Objects:** `SummonDb` and `SummonData` for tower definitions
- **Prefab References:** Centralized `PrefabsRef` for asset management
- **Interfaces:** `IDamageable` for flexible damage application

---


## 🛠️ Technology Stack

- **Engine:** Unity 6.3 LTS
- **Language:** C# 9.0
- **UI Framework:** Unity UI (uGUI)
- **Input System:** Unity Standard Input Manager
- **Version Control:** Git

---

## 🎯 Code Quality Standards

This project demonstrates:

- ✅ **Consistent Naming Conventions:** Clear, descriptive variable and method names
- ✅ **Modular Architecture:** Independent, reusable systems
- ✅ **Separation of Concerns:** Each class has a single responsibility
- ✅ **Event-Driven Communication:** Loose coupling between systems
- ✅ **Scalable Design:** Easy to add new towers, enemies, and mechanics
- ✅ **Comments & Documentation:** Clear inline documentation where needed
- ✅ **Prefab-Based Design:** Reusable game object templates

---

## 🚀 How to Build & Run

### Prerequisites
- Unity 6.3 LTS or later
- Visual Studio or Visual Studio Code with C# support

### Setup
1. Clone the repository
2. Open the project in Unity Editor
3. Load the main scene from `Assets/Scenes/`
4. Press Play in the Editor

### Controls
- **Click:** Select and place towers
- **Hover:** Preview tower placement
- **UI Buttons:** Purchase and place defensive towers

---

## 📈 Development Insights

### Build Timeline
- **Day 1 (Nov 25):** Core systems setup, game state management, grid environment
- **Day 2 (Nov 26):** Enemy AI, tower mechanics, UI implementation
- **Day 3 (Nov 27):** Win/lose conditions, visual polish, audio integration

### Development Approach
- Started with core game loop and state management
- Iteratively added gameplay systems (enemies, towers, UI)
- Focused on clean code structure from the beginning
- Implemented proper separation of concerns throughout

---

## 💡 Key Learning Outcomes

This project showcases ability to:

1. **Architect scalable game systems** - Clean separation between gameplay, UI, and data
2. **Implement design patterns** - Singletons, Events, State Machines
3. **Build responsive UI** - Real-time updates and player feedback
4. **Write maintainable code** - Clear naming, modular structure, proper interfaces
5. **Problem-solve efficiently** - Delivered complete POC in limited timeframe
6. **Follow best practices** - Professional code organization and documentation

---

## 📝 License

This project was created as a code challenge demonstration.

---

## 👤 Author

Created as a job interview code challenge demonstrating professional game development practices.

**Project Duration:** November 25-27, 2023  
**Estimated Development Time:** ~5 hours  
**Purpose:** Code Challenge - Showcase clean architecture and code quality

---

## 🔗 Contact & Contribution

For questions about this project or the implementation approach, feel free to reach out.

---

**Last Updated:** November 27, 2023
