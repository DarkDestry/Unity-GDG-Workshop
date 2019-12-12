# Chapter 2 - Background

In this chapter we will be creating a background for the game

## Creating the background

The game needs an interesting background to place the player in the world. To add a background to the game we simply need to find the image and drag it into the scene view.

Navigate to `Sprites/Background` and pick a background for your game. Click and drag it to the Hierarchy to add it.

On your scene view and game view, you should be able to see the background that you have just added.

![Background Added to Game](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/AddedBackground.png?raw=true)

## Making the background scroll

### Material Setup

However, our game is about a ship traving through an asteroid field. Having a static background looks like the ship is not moving. To give an illusion of movement, we will make the background scroll.

To do this, we need to create ourselves a new material. In the `Sprites/Background` folder, right click the empty space in the project explorer and select `Create > Material`. You should see a new asset with a ball icon appear in your Project explorer. Give it a name.

![Background Added to Game](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/BackgroundMaterial.png?raw=true)

Drag the material onto the background you added in the hierarchy.

![Background Added to Game](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/ApplyMaterial.png?raw=true)

Next, we want to modify the material such that it can scroll. To do this, we shall select the simplest material with an offset variable that we can change.

Select the material in the project explorer. On your inspector window, change the shader from `Standard` to `Unlit/Texture`. You should now see a field labeled offset.

![Background Added to Game](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/UnlitTextureShader.png?raw=true)

Try doing a left click and drag over either the X or Y offset fields. You should now see your background scrolling. If for some reason your image doesnt scroll correctly, refer to the quick note on Image Wrap Mode below

<details style="background: lightgray"><summary >Quick Note on Image Wrap Mode</summary>
<p>

#### If your image looks like this

![Image Wrap mode Clamp](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/ImageWrapClamp.png?raw=true)

This means that the image wrap mode is set to clamp. There are multiple different modes of wrapping images past its edge.

-   Clamp - Repeat the last pixel on the edge infinitely
-   Repeat - Tile the image
-   Mirror Once - Mirrors the image past the edge and clamp after.
-   Mirror - Mirrors the image indefinately
-   Per-axis - Set the above mode on a per-axis basis

Select the background image in your project explorer. On the inspector window, find the option for Wrap Mode. Set the mode to Repeat and it should tile correctly

![Image Inspector](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/ImageInspector.png?raw=true)

</p>
</details>

### Scripting the offset scroll

While you can scroll the background manually, we want it to scroll automatically. We shall accomplish this by writing a script that automatically increase that value.

First, we shall create a script. Go back to your root directory of your Project Explorer. Create a folder called Scripts by right clicking the empty space and select `Create > Folder`. Create a script in that folder by right clicking the empty space and select `Create > C# Script`. Give it the name of `BackgroundScroller`. Do take note that if you want to rename the file, you have to change the class name in the script as well.

Drag the script onto the Background object just like you did the material before.

### Writing the code
