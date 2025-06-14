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



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



## **Angela's Blog**
**June 14 2025**

**"Echoes of the Forgotten World"**:

**Project Overview**
"Echoes of the Forgotten World" is our Unity-based game featuring enemy wave combat and teleportation mechanics through an arcade machine interface. This project gave us the opportunity to work with layered gameplay mechanics and polish our user interface and C# skills.

**My role**
Project Manager

**Work Completed**
Over four weeks, I implemented several systems:

* **Enemy Wave System** - I designed and created the enemy ship assets for our three-wave combat sequence. Ashtin and I worked together extensively on debugging the spawn mechanics to get the wave progression working properly, as well as implementing the process of layering scenes. This process is similar conceptually to PhotoShop, the advantage of this is it allows (2) or more people to effectively work on the same scene but in different files and avoid merge conflicts. Once each scene is complete, they are simply layered on top of eachother, giving the effect of a single cohesive file.

* **Enhanced Teleportation Mechanism** - This became my favorite problem to solve. The original teleportation happened in a single frame—completely jarring. I rebuilt it using coroutines to spread the effect across multiple frames, creating that smooth transition we were after. I also added the white screen overlay for the arcade machine power-on effect. Coroutines are not dissimilar to async/await functions in JavaScript.
  
* **Audio Integration** - Implemented music systems throughout to enhance immersion and provide proper audio feedback for different game states.

* **UI Development** - Created both Game Over and Main Menu screens. The Main Menu's "Start Game" button provides seamless entry, while the Game Over screen includes restart functionality (with continue functionality implemented by Ashtin) to keep players engaged.

* **Level Polish** - Enhanced Level 1's aesthetic quality with sound effects and additional sprite assets.

**Technical Hurdles & Growth (Challenges & Solutions)**
The biggest challenge plagued us for half the development cycle: file synchronization nightmares during GitHub merges. We consistently found missing files after fetching and merging changes locally.

**The Line Ending Mystery** After diving into research, we discovered the culprit: line ending conversion between "LF" and "CRLF" formats. Mark and Ashtin were developing on Mac while I used Windows. These different line ending formats made GitHub perceive conflicts that didn't actually exist, completely overwhelming our merge process.

**Solution Implementation** We resolved this by implementing project-level configuration to maintain consistent "LF" line endings across all platforms.

**Process Adaptation**
This project reinforced how development workflows must adapt to specific technology challenges:

1. Unity's complex file structures and binary assets create unique merge conflict scenarios
2. Cross-platform development requires careful attention to seemingly minor details like line endings
3. Clear communication becomes even more critical when troubleshooting issues spanning multiple development platforms

**Key Learnings**
This project reinforced how every technology stack has its own quirks, and Unity definitely has a high propensity for merge conflicts due to its architecture. Working through these challenges as a team gave us all fresh perspective on how important clear communication becomes when troubleshooting technical issues across different development platforms. Most importantly, I think we gained renewed appreciation for how project requirements should drive process decisions. 


![image](https://github.com/user-attachments/assets/5b57caf5-401d-41fb-9e9c-3fe4d9f20ceb)

![image](https://github.com/user-attachments/assets/100947e2-8aac-46d3-9ec2-3f9b6f50b115)

![image](https://github.com/user-attachments/assets/b1dc2624-d786-4f6a-b89f-bae1a0605abe)


## Poster

![image](https://github.com/user-attachments/assets/86c8d75f-e8b4-4532-b8b9-a634450a71e5)






--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





## Angela's Blog 
# Breaking Into Game Development: My Unity Journey

## Project Overview
Our team is developing a 2D game in Unity inspired by classic arcade games. This project gives us the opportunity to explore game development principles while delivering something engaging and nostalgic.

## Work Completed
I concentrated on Level 1 - Scene 3, where I was responsible for:
- Sourcing and implementing scene assets
- Creating the arcade machine centerpiece
- Developing the character "materialization" effect as players enter the arcade machine
    - This scene serves as a crucial transition point in our game, setting up the player's entry into the game world.
- I developed the enemy ship and it's firing capablities. This was complicated as I wanted the movment to be a bit more 'natural' instead of a harsh zig-zag pattern.

## Technical Hurdles & Growth (Challenges & Solutions)
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

## Key Learnings
Returning to C# has been refreshing after my earlier alternative language project. The practical application within Unity expanded my understanding of the language a great deal.

More importantly, I've gained appreciation for how project requirements should shape development processes, not the other way around. Our team's adaptability in creating custom workflows specifically for Unity collaboration demonstrates this principle.

![image](https://github.com/user-attachments/assets/30294288-f681-4254-84b6-d696ee3386ce)

![image](https://github.com/user-attachments/assets/34def04f-06c0-402d-bcc6-34cfd49b1d89)

![image](https://github.com/user-attachments/assets/39ae1387-0ce0-4389-b64d-41cb4b39de74)

[Echoes of the Forgotten World Video Presentation](https://youtu.be/C_ioHrTwdL4)

[Echoes of the Forgotten World PowerPoint](CapstonePresentation.pdf)
