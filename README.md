# Pathing Is Key

This is the GitHub Repository where we will upload our project of the Interactive Systems course, called Pathing Is Key.
This project consists in a collaborative videogame where two children, between 8 and 12 years old, have to cooperate to build a stone path until a ruby reward by avoiding different obstacles.

# Team members

Arnau Camarero

Javier Echavarri

Cristina Mendoza

# Initial Implementations

- Different tags for different elements.
- Movement is only allowed on the Stone and When the player tries to leave the controller won't let it
- Mechanics to pick the bucket and the pickaxe based on collisions.
- Mechanics for the buckets to pick either lava or water.
- Mechanics for the pickaxe to destroy stone when tracker goes down.
- Sounds for the bucket picking water or the pickaxe breaking stone have also been added.

# Mid Implementations

- Ambiental Sound.
- Make that when the ruby is picked up, it disappears.
- Sound for the ruby when is picked up.
- Pickaxe points at player's direction.
- Mechanics for the creation of a obsidian or stone platform.
- Created an advanced path to reach the stone (not optimal).
- Increased the speed of the players for non-tracking (speed up the process).
- Added new prefabs and a platform generator to create, destroy and regenerate the platforms

# Advanced implementations

- Addition of rivers with currents where if a block of obsidian is placed the river is blocked and
  if stone is placed then the block is carried away with the current.
- Addition of obstacles such as trees and mountains where the players can't place a platform
- Mechanic where the bucket can pick up lava or water placed on a platform
- Fixed bug that duplicated platforms when one was destroyed
- Updated lava audio
- Addition of decoration such as flowers, trees, rocks, and a mountain.
- Modification of ruby, added rotation and increased the scale
- Instead of destroy objects we disable 'if (SetActive(false)) -> Disable them
- Created a New función FillPlatforms
- Added An id per platform to identify them
