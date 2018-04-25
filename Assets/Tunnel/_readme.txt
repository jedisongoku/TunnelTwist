Thanks for your purchase.



To begin, open the scene «Tunnel».


Script descriptions:


- AddSpeed:
Class define in SpeedAddText.cs, use to change the speed in the game. 
	 - AnimateSphereTexture.cs:
Attached to the sphere. In charge to rotate the texture on the sphere.
 - AnimPanelInfoAtPlayerStart.cs:
Àttached to some element of UI_InfoInGame. In charge to animate ui element when player starts.
 - BackgroundAnim.cs:
In charge to animate UI element of UI_background. React with the music. 
 - BestScoreText.cs: 
Attached to some UI text elements in charge to display best score in the game.
 - ButtonLeaderboard.cs:
Attached to the leaderboard button. Open the leaderboard (iOS only)
 - ButtonMoreGames.cs:
Attached to the button more games
 - ButtonRate.cs:
Attached to the rate button
 - ButtonRestart.cs:
Attached to the restart button. Restart the scene by loading a new one
 - ButtonTransitionAnimation.cs:
In charge to anim UI buttons
 - CameraLogic.cs:
In charge to display the animation at start
 - ColorManager.cs:
In charge to change the color in the game
 - Constant.cs:
Some constants. Modify speedPow and speedPowDiv to change the difficulty in the game.
 - CubePlatform.cs:
Each platform have this script attached. In charge of the animation of the platform.
 - CurvedManager.cs:
In charge to curve the world.
 - EventManager.cs:
In charge to manage all the venet in the game.

- GameManager.cs:
In charge to spawn the platforms, to count the point, to change the speed.

- InputTouch.cs:
Detect touch, click and keybords inputs.
 -LastScoreText.cs:
Attached to ui text element in charge to display the last score in the game.
 - LeaderboardManager.cs
Static class in charge of the leaderboard.
 - MaterialManager.cs:
In charge to change the color in the game. 
 - MenuManager.cs:
In charge to display ui menu at start and at game over.
 - MonoBehaviorHelper.cs:
An helper to avoid some duplicate code.
 - MoveForward.cs:
In charge to move the player continuously. 

ObjectPool.cs - ObjectPoolClass.cs - ObjectPooling.cs - ObjectPoolingExtensions.cs:
In charge to pool items and platforms at level start and to spawn them when needed.
 - PlatformParent.cs:
The parent of all platforms. Have 8 childs. In charge to desactivate / animate his childs, and display items.
 - Player.cs:
In charge to move the player, detect if player is grounded or not. If not grounded => Game Over.
 - PlayerPrefsX.cs:
A player pref extension. Very usefull.

- PointTrigger.cs:
A trigger when the player touch a point item.

- ScoreText.cs:
Attached to UI element in charge to display the score during the game.
 - SlowMotionLogic.cs:
In charge to do some stuff (like decrese the timeScale..) when the player trigger the slow down bonus.
 - SoundManager.cs:
In charge to play the sound in the game.
 - SpeedAddText.cs:
Attached to the UI Element who is moving each time the player get 1 point. In charge of the animation.
 - SpeedText.cs:
Attached to the ui element who display the speed text in the game.

- TCurve.cs:
Class to help us to curve the world. 
 - TUNNELColor.cs:
Class to get a random flat color
 - Utils.cs:
Some usefull function. 