# Echoes_Of_The_Forgotten_World

## Project Stucture

Assets/
├── Animations/ (Animation clips and controllers)
├── Audio/ (Sound effects and music)
├── Fonts/ (Custom fonts for UI)
├── Materials/ (Material assets for 2D objects)
├── Prefabs/ (Reusable game objects)
│ ├── Characters/ (Player and NPC prefabs)
│ ├── Environment/ (Level elements and obstacles)
│ ├── UI/ (UI element prefabs)
│ └── Effects/ (Visual effects prefabs)
├── Scenes/ (Unity scene files)
├── Scripts/ (All C# code files)
│ ├── Player/ (Player control scripts)
│ ├── Enemies/ (Enemy behavior scripts)
│ ├── UI/ (User interface scripts)
│ └── Utilities/ (Helper functions and tools)
├── Sprites/ (2D graphics)
│ ├── Characters/ (Player and enemy sprites)
│ ├── Environment/ (Background and terrain sprites)
│ ├── Items/ (Collectible and usable item sprites)
│ └── UI/ (Icons and UI element sprites)
└── Tilemaps/ (Tilemap assets for 2D levels)


## Angela's Blog 
# Breaking Into Game Development: My Unity Journey

## Project Snapshot
Our team is developing a 2D game in Unity inspired by classic arcade games. This project gives us the opportunity to explore game development principles while delivering something engaging and nostalgic.

## My Development Focus
I concentrated on Level 1 - Scene 3, where I was responsible for:
- Sourcing and implementing scene assets
- Creating the arcade machine centerpiece
- Developing the character "materialization" effect as players enter the arcade machine
    - This scene serves as a crucial transition point in our game, setting up the player's entry into the game world.
- I developed the enemy ship and it's firing capablities. This was complicated as I wanted the movment to be a bit more 'natural' instead of a harsh zig-zag pattern.

## Technical Hurdles & Growth
Working with Unity presented unique challenges compared to my previous coding experiences:

**Physics System Complexity**  
Unity's physics engine required significant self-education. I dove into documentation, articles, and tutorial videos to master the system's nuances. This research-heavy approach paid off when implementing the materialization effect, which needed precise physics calculations to appear natural.

**GitHub Integration Lessons**  
Unity's relationship with GitHub differs substantially from typical Java or Python projects. We discovered prefabs often required special handling with commands like:

git checkout upstream/main -- Assets/LevelLoader.prefab

These path-specific checkouts became essential to our workflow.

**Merge Conflict Evolution**  
The dual-nature of Unity development—working in both the Unity editor and VSCode—created multi-dimensional merge conflicts. Changes to the same sprite or physics component in Unity would create conflicts beyond just code, requiring careful coordination.

## Process Adaptation
This project revealed how different development environments benefit from tailored approaches:

1. We established a prefab notification system for pull requests, alerting team members when prefabs required special attention
2. We developed a "layering" approach to scene development, working in separate scenes before integration—similar to layers in Photoshop
3. This compartmentalization minimized conflicts while maintaining creative freedom

## Personal Growth
Returning to C# has been refreshing after my earlier alternative language project. The practical application within Unity expanded my understanding of the language a great deal.

More importantly, I've gained appreciation for how project requirements should shape development processes, not the other way around. Our team's adaptability in creating custom workflows specifically for Unity collaboration demonstrates this principle.
