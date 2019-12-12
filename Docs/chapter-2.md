# Chapter 2 - Background

In this chapter we will be creating a background for the game

## Creating the background

The game needs an interesting background to place the player in the world. To add a background to the game we simply need to find the image and drag it into the scene view.

Navigate to `Sprites/Background` and pick a background for your game. Click and drag it to the Hierarchy to add it.

On your scene view and game view, you should be able to see the background that you have just added.

![Background Added to Game](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/AddedBackground.png?raw=true)

## Making the background scroll

### Material Setup

Our game depicts a spaceship traveling through space. Instead of moving the player ship, camera, background, and (possibly) many other objects in the engine, we will instead create the illusion of movement by "scrolling" the background image. This is a design decision that will reduce the complexity of scripting other game components in the future. 

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

### Writing the code

When a C# script is created in Unity, a template will be used and the following code will be generated.


```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
```

Because our `BackgroundScroller` extends `MonoBehaviour`, we can now attach the script as a component on our Backgroud gameobject. Drag the script onto the Background object just like you did the material before.

To scroll the background, we will first need to create a reference to the `SpriteRenderer` component attached alongside with our `BackgroundScroller` component. 

<details style="background: lightgray"><summary >The SpriteRenderer Component</summary>
<p>
The Sprite Renderer component was attached automatically by Unity when we dragged the background asset into the scene. This component handles drawing the background image to the screen, and contains the material that we dragged into the gameobject earlier.
</p>
</details>

We will first create a public `float` member to easily change the scroll speed in the editor.

```csharp
public float m_ScrollSpeed;
```

In the inspector, we should see a new field named "Scroll Speed"

![A new field to set scroll speed](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%202/BackgroundScrollSpeedInspector.png?raw=true)

Create a SpriteRenderer member in the class.

```csharp
private SpriteRenderer m_SpriteRenderer;
```

In the `Start` function, assign the `m_SpriteRenderer` reference.

```csharp
void Start()
{
    m_SpriteRenderer = GetComponent<SpriteRenderer>();
}
```

<details style="background: lightgray"><summary >The GetComponent Function</summary>
<p>
GetComponent is a templated function that returns the first component of the specified type in the current gameobject. 
</p>
</details>

Next, inorder to scroll the background image, we will set the built-in `offset` property of the material. This property "offsets" the texture by some amount in both the x and y axis. If the offset is increased over time, we will achieve the effect of scrolling the image.

In the update function, we will assign an offset based on time elapsed through the `Material.SetTextureOffset()` function.

```csharp
void Start()
{
    spriteRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, Time.time * m_ScrollSpeed));
}
```

"_MainTex" is a keyword that specifies the offset operation on the the primary texture in the material.
The second parameter is a 2 component vector that specifies the amount of offset in each axis. Since we want to scroll the image from top to bottom, but not scroll from left to right, we will leave the x component as 0. 

For the y component of the offset vector, we will use `Time.time` to drive the offset amount. `Time.Time` will be updated by Unity every frame, and will increase over time.

Run the game and the background should start scrolling. If not, check the `ScrollSpeed` property in the `BackgroundScroller` component (in the inspector window, not in code. Inspector settings will overwrite the value in code).
