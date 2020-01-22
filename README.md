# Desert Storm
LAS3018 Unity Assignment

# Game Design

A 2.5D top-down shooter was designed and developed for the LAS3018 unit at the University of Malta. The set of requirement for the submission of this assignment were met as follows –

– Win and/or lose condition (i.e. a clear end game state)
The player can lose by having their health drained to zero. The player can win by reaching the end of the level. Both states are clearly shown by text on the user interface when conditions are satisfied.

– Multiple (i.e. 2 or more) enemies
Two enemies were designed for this assignment, Armored Personnel Carrier (denoted by APC) and Desert Tank (denoted by TankA).

Both enemies look at the player to give a sense of realism. The two enemies will chase the player when they are within reach. Furthermore, they have a probability of spawning a random pickup, which will be described later on.
APC has a higher area of reach, and will follow the player when they are inside the trigger. APC can damage the player by hitting them. Once hit, a delay was introduced until the next attack to balance the difficulty.

TankA has a smaller area of reach, however the attack is not based on contact. Instead, TankA has the ability to chase and fire projectiles when in reach. By design, no contact damage was implement to balance the increased amount of damage from the enemy bullets (denoted by enemyShell) and increased health to compensate for the smaller chase area.

– Multiple (i.e. 2 or more) weaponry/bullets
The player has one bullet type, a tank shell, which can be fired continuously. Another bullet was also created for the enemies to distinguish between the two. The enemyShell ignores collisions between other enemies.

– Multiple (i.e. 2 or more) power ups
There are two power ups in the game, health and circle of death. The health power up provides a boost increment of 20 to the player’s current health. If the health maxes out to 100, other health pickups will contribute to the total score. The second power up is the circle of death. On collection, the power up creates 8 bullets in a circle around the player, and will be launched in the direction of their rotation. This will give the player a boost in difficult situations, when surrounded by enemies.

– Audio
The audio designed for this game matches the visuals, in a way that the player can aurally perceive the actions of the game. Ex. Shooting and reloading; distinguishing between health and circle of death pickup; distinguish between damage dealt and received.

– User Interface
Two canvases were designed for this assignment, to segregate between head-up display and the main menu. Canvas child objects are manipulated accordingly when conditions are met and game states reached. Start and Quit button are present in the UI, together with a sound effects slider to change the volume level of effects. As a design choice, the music is controlled by the designer and cannot be changed by the end user.

# Game Development

The game was developed using the C# language and Visual Studio Community 2015 as an IDE. There are two singleton classes.

The Game Controller is responsible for global game control, such as win/lose states, menu as well as heads-up manipulation while the Audio Controller is responsible for all audio effects in the game. Using singleton classes facilitated the process for safely exposing certain methods and values to other classes for reading and setting operations.

During the development process, code repetition was avoided by creating methods that can be called when required. Code comments were added to make the code human readable.

# Final Remarks

Desert Storm is a small proof of concept, based on Unity Tanks! Project with respect to art. Credit goes to them.

Art assets will be changed later on to not infringe any copyright licenses. At its core, this was a game development challenge for me.
