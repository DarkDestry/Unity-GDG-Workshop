# Chapter 4 - Bullets

In this chapter we will be adding the laser bullet that the player will use to shoot the asteroids.

## Creating the bullet

Similar to the player, you need to first create the bullet in the scene. Navigate to the `Sprites/Bullets` volder in your project hierarchy. Select a bullet and drag it into the Hierarchy.

![Bullet in Scene View](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/LaserBulletInScene.png?raw=true)

Rename your laser to `Bullet` so that we can easily identify it in the hierarchy. You can do that via `Right Click > Rename` or select the object in the hierarchy and rename it at the first line of the inspector window

## Adding Components

Like the player, this bullet will requre physics simulation and a collder to interact with other objects. So lets add the required components for it. 

Select the bullet in the hierarchy, scroll to the bottom of the inspector and select `Add Component`. Add the `Rigidbody 2D` and `Box Collider` component.

![Bullet Inspector](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/BulletInspector.png?raw=true)

## Adding constraints

At this point you can press play in the editor and try interacting with the bullet with your ship. You may notice some perculiar behaviour with both your ship and the bullet

![Unconstrained rigidbody](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/Constrains.gif?raw=true)

This is due to an unconstrained rigidbody. By default unity's rigidbody assumes that your physics object can rotate like a ball. This is an unwanted behaviour in our game. To restrict the physics simulation to not rotate the object, we need to set a rotational constraint on our rigidbody component. Do this for both the bullet and the player.

![Rigidbody Constraints](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/RigidbodyConstraints.png?raw=true)

> You may notice two other constraints there for X and Y axis. Those are positional constrains and will allow you to tell the physics engine that this bullet can only move vertically or horizontally.

After setting those property, run the game again and try interacting with the bullet again, both the bullet and the player should not rotate anymore.

![Constrained rigidbody](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/ConstrainedRigidbody.gif?raw=true)

## Setting Triggers

You may have noticed that in the previous section, our bullet bounced off the ship. This is yet another unwanted behaviour. Bullets should not bounce off of our ships but should instead be either destroyed or pass through our ships.

To do this we need to set our bullet colliders as triggers. When other colliders intersect with this trigger collider, they will trigger a function in any scripts attached to the game object. We can then script the behaviour of the bullet in code. 

Select the bullet and set the `Is Trigger` property for the collider. Do the same for the player as well as it will need to interact with the asteroid in the future.

![Collider Is Trigger Property](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/ColliderTrigger.png?raw=true)

If you play the game now and try interacting with the bullet, you should notice that the bullet passes through the player ship. The bullet is not doing anything now as we have not scripted the bullet yet.

![Is Trigger Collider](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/ConstrainedRigidbody.gif?raw=true)

## Creating a prefab

Next we need to make this bullet a prefab. Prefabs are as the name suggests, "Pre-fabricated" game objects. These are objects that we have setup with the appropriate components and will enable us to spawn multiple copies of the same object. Our player will need to spawn multiple bullets and thus will require a bullet prefab to spawn.

To make project directory neat, we shall create a new folder called `Prefabs` in the root directory of the project explorer. After you have created the folder, open it and drag your bullet from the hierarchy into the empty space in the project explorer.

![Bullet Prefab](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/BulletPrefab.png?raw=true)

## Scripting the Bullet

Navigate back to the scripts folder that you created and create a new script called `Bullet`. This will contain the code that move the bullet and destroy the bullet when it hits something.

=================== TODO ======================
