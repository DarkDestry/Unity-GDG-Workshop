# Chapter 3 - Player Movement

In this chapter we will be creating a movable player ship.

## Creating the Player

To start, you need a player ship in the game scene. Navigate to the `Sprites/Ships` folder in your project explorer. Select a ship and drag it into the Hierarchy.

To move it the player around in the scene view, you can use the red and green arrow gizmos. You can also move it on both axis by dragging the square between the two arrows.

![Player Ship in Scene View](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%203/ShipInScene.png?raw=true)

Rename your game object to Player so that we can identify it easily later. You can do that via `Right Click > Rename` or select the object in the hierarchy and rename it at the first line of the inspector window

## Adding Components

The player ship will require several components to work correctly. It will need a rigidbody components to tell unity that this object requires physics simulation. Additionally, it will require a collider to allow for physics objects to interact with it.

To add these components, simply select the game object in the hierarchy, under the inspector window, scroll to the very bottom and select `Add Component`. Search for the two items `Rigidbody 2D` and `Box Collider`.

![Adding Components](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%203/AddComponents.png?raw=true)

> There is no particular reason we used the box collider to approximate the player's collider. If you want, you can select the Circle Collider or the Polygon Collider to approximate the player collider.

## Scripting the Player

=================== TODO ======================
