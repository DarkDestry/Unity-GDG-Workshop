# Chapter 1 - Setting up your project

## Creating a project

To create a project, navigate to the projects tab on your unity hub and select NEW. After which you should see the following window

![Create New Project](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%201/CreateNewProject.png?raw=true)

Select the 2D project template, give the project a name and a clean directory for the project to be stored at.

After creating the project, you will be greeted with the editor.

![Initial Unity editor screen](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%201/InitialUnityScreen.png?raw=true)

## Importing Assets

For this workshop we have packaged all the assets you need for the game into one unity package. You can get the .unitypackage [here](https://github.com/DarkDestry/Unity-GDG-Workshop/releases/tag/1.0.1). Under Assets, Workshop-pack-2.unitypackage.

Open the .unitypackage file and you will see an import package window. Ensure all items are checked and click import. Wait for the import to finish and you should see the assets in your project explorer.

![Asset Import Dialog](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%201/ImportAssetPackage.png?raw=true)

![Project Explorer with Assets](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%201/ProjectExplorerAfterImport.png?raw=true)

## Changing to Vertical View

Select the Game View tab. On the default layout, you can find it above the scene view. You may notice it currently fits to your current view window. For this project, we are creating a game similar to asteroids. We will need to fix the game to a vertical layout. To do so, we need to fix the resolution to a 10:16 aspect ratio. First, change your game view resolution to a 10:16 aspect ratio.

Select the `Free Aspect` button and add a new vertical resolution. Give a name to the preset, select `Aspect Ratio` as the type and give it a width and hight of 10x16.

![Game View Aspect Ratio](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%201/GameViewAspectRatio.png?raw=true)

Next we need to change the default resolution when your game is launched. You can do that from the Project Settings. Navigate to `Edit > Project Settings`. From the new window that appeared, `Player > Resolution and Presentation`

We want the game to launch in windowed mode with a preset resolution. For a 10:16 resolution, here are some resolution settings for different monitors.

```
1280x720  - Width: 406, Height: 650
1366x768  - Width: 437, Height: 700
1920x1080 - Width: 625, Height: 1000
```

![Game View Aspect Ratio](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%201/ProjectSettingsResolution.png?raw=true)
