# Chapter 5 - Asteroids (Enemies)

In this chapter we will be adding the asteroids that the player can shoot at.

## Creating the Asteroids

Similar to the player, you need to first create the bullet in the scene. Navigate to the `Sprites` volder in your project hierarchy. Our asteroids come in the form of a sprite sheet. We have sliced the sprite sheet for you into its component sprites. To access its individual sprites, click the arrow button next to the asteroid spritesheet and you should see 4 different individual asteroids.

![Asteroid asset sliced](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/AsteroidAssets.png?raw=true)

Select one of the asteroids and drag it into the scene or hierarchy. 

![Asteroid in Scene View](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/AsteroidInScene.png?raw=true)

Rename your Asteroid to `Asteroid` so that we can easily identify it in the hierarchy. 

## Adding Components

Like the player and bullet, this asteroid will requre physics simulation and a collder to interact with other objects. So lets add the required components for it. Since the asteroid is round here, we shall approximate its collider using a circle collider.

Select the bullet in the hierarchy, scroll to the bottom of the inspector and select `Add Component`. Add the `Rigidbody 2D` and `Circle Collider` component.

## Setting Triggers

Similar to the bullet and player, the asteroid need to trigger an event when the bullet or the player collide with it. Select the `Circle Collider` and set its `Is Trigger` property to true. 

## Creating a prefab

As we need to spawn multiple asteroids, we will need to create a prefab of it as well. Drag the asteroid object into the prefabs folder to create a prefab.

## Tagging objects

Up till this point, our object never really did anything when they overlap with other objects. Now that we need the bullet to destroy the asteroid, we need to have a way to identify the objects that we collide with.

To achieve this, we will use tags to tag our objects. Select the asteroid in your hierarchy. At the top of the Inspector Window, you should see a tag dropdown. Select the dropdown and click `Add Tag`.

![Add Tag](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/AddTag.png?raw=true)

You should be brought to the `Tags & Layers` inspector window. Under Tags, click the + icon on the bottom right and add the `Enemies` tag. We will not need a player tag as there is a built in player tag provided by Unity.

![Enemies Tag](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/EnemiesTag.png?raw=true)

## Scripting the Asteroid

Navigate back to the scripts folder that you created and create a new script called `Enemy`. 

=================== TODO ======================

