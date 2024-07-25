2.5D Sprite Controller
Austin Zimmer
Contact Email: phatrobit@gmail.com
Discord: https://discord.gg/qkrV9jf

Thank you for your purchase! Feel free to contact me or join my Discord server if you have any trouble or feedback.

Notes:
- Take a look at the Example scene (Assets/PhatRobit/2.5D Sprite Controller/Examples/Scenes/Example.unity) to see how a basic setup of the scripts work
- The Examples directory is not required for this package to work properly
- You may use your own solution for animating the sprites of your characters

Quick Start Guide:
1. Add a CameraView Component to your Main Camera
2. Add a SpriteView Component to the parent object of your character
3. Create a child gameobject on your character that will act as the sprite parent
4. Assign the sprite parent transform to the Sprite Parent variable on the SpriteView component
5. Place your character art as children of the sprite parent gameobject

Optional steps for animation using Unity's built in solution:
1. Add a SpriteAnimation Component to the parent object of your character
2. Assign the Animator of your character art to the Animator variable on the SpriteAnimation Component
3. Ensure that your Animator Controller has a Direction float parameter
4. Use the Direction parameter on your Animator Controller to determine which animation to play (take a look at my example controller to see how it works)
	- 0 = Up
	- 1 = UpRight
	- 2 = Right
	- 3 = DownRight
	- 4 = Down
	- 5 = DownLeft
	- 6 = Left
	- 7 = UpLeft