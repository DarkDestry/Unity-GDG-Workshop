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

![Is Trigger Collider](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/TriggerCollider.gif?raw=true)

## Creating a prefab

Next we need to make this bullet a prefab. Prefabs are as the name suggests, "Pre-fabricated" game objects. These are objects that we have setup with the appropriate components and will enable us to spawn multiple copies of the same object. Our player will need to spawn multiple bullets and thus will require a bullet prefab to spawn.

To make project directory neat, we shall create a new folder called `Prefabs` in the root directory of the project explorer. After you have created the folder, open it and drag your bullet from the hierarchy into the empty space in the project explorer.

![Bullet Prefab](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/BulletPrefab.png?raw=true)

The object in the hierarchy should have a blue name instead of black after creating the prefab to indicate it is a prefab-ed object.

## Modifying a prefab

If any change was made to the game object after adding creating the prefab, the changes are not applied to the prefab itself. This is to allow you to create multiple different versions of the same bullet.

In the event that you want to apply the changes to the prefab itself, select the object in the hierarchy, on the first row of the inspector window, select override and apply all. That should apply the new settings on the current object onto its linked prefab.

![Prefab Override](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%204/PrefabOverride.png?raw=true)

## Scripting the Bullet

Navigate back to the scripts folder that you created and create a new script called `Bullet`. This will contain the code that move the bullet and destroy the bullet when it hits something.

Our bullet behaviour is very straightforward. We will set the velocity of the attached rigidbody once in `Start()` and the bullet will move at a constant speed. We will also destroy the bullet after a couple of seconds, when we know that the bullet will be outside of the camera's view and out of play. We will use an empirical value of `10s` for now, but may come back to adjust this later.

```csharp
public float m_Speed = 15;

void Start()
{
    GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    Destroy(this.gameObject, 10); // Self destruct after 10 seconds
}
```


// ------------------------- DATASETUP: ADD GIF OF ONE BULLET MOVING ----------------------------//



We will come back to the bullet script after we setup enemies for the bullet to interact with. For now, we will make it such that our player can fire the bullets.


In the player script, create a new function `HandleWeapons()`. We will also call this function in `Update()`. We will also create a public member for us to assign the bullet prefab in the inspector.

```csharp

public class Player : MonoBehaviour
{
    ...
    public GameObject m_BulletPrefab;
    ...

    void Update()
    {
        HandleMovement();
        HandleWeapons();
    }

    ...

    void HandleWeapons()
    {
        if (m_BulletPrefab == null)
            return;

    }
}
```

Back in Unity, drag the bullet prefab into the newly created `Bullet Prefab` field in the player component.

// ----------- DATASETUP: ADD OF IMAGE OF ASSIGNING BULLET PREFAB ON PLAYER COMPONENT ---------------- //

Just like how we used the "Horizontal" and "Vertical" input axis to move the player, there is a similar "Fire1" input axis that is mapped to common "fire" buttons. We will use this to check if we should spawn bullets.


```csharp
void HandleWeapons()
{
    if (m_BulletPrefab == null)
        return;

    if (Input.GetAxis("Fire1") > 0)
    {
        Instantiate(m_BulletPrefab, transform.position, Quaternion.identity);
    }
}

```

The `Instantiate()` function will spawn/create the input gameobject at a target position and rotation. We wil spawn the bullet at the player position and give it no rotation (identity rotation).

> Rotations in Unity are described by Quaternions. Quaternions are a slightly more advanced mathematical concept that we will not be covering here. For more information on Quaternions, see https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html

There are no reason for us to only spawn one bullet in `HandleWeapons()` when the input key is pressed. You are free to spawn more at any location you like. We like to fire two bullets side by side so we spawned two instead.

```csharp
Instantiate(m_BulletPrefab, transform.position + new Vector2(-0.27f, 0.0f), Quaternion.identity);
Instantiate(m_BulletPrefab, transform.position + new Vector2(0.27f, 0.0f), Quaternion.identity);
```

If we run the game and press `left ctrl` on the keyboard, our player should now be rapid firing bullets. `left ctrl` is mapped to the `Fire1` axis by default.







// ----------- DATASETUP: ADD GIF OF RAPID FIRE BULLETS ---------------- //








We want to be able to control the speed at which bullets fire. At the moment, firing speed is dependent on the frequency of which `Update` is called. This means that the firing speed is framerate dependent - less bullets will fire on a slow computer than on a fast one. We want to be able to control the time between firing each bullet.

Create a property `m_TimeBetweenShots` to control the time between each shot, and a private member for us to keep track of the time when the bullet was last fired.

```csharp
public class Player : MonoBehaviour
{
    ...
    public float m_TimeBetweenShots = 0.2f;
    private float m_TimeOfLastShot = 0;
    ```
}

```

In the `HandleWeapons()` function, we will check if it has been long enough since the last time we fired a shot, and only fire when the time since last shot is greater than `m_TimeBetweenShots`.

```csharp
void HandleWeapons()
{
    if (m_BulletPrefab == null)
        return;

    if (Input.GetAxis("Fire1") > 0)
    {
        float timeSinceLastShot = Time.time - m_TimeOfLastShot;
        if (timeSinceLastShot > m_TimeBetweenShots)
        {
            Instantiate(m_BulletPrefab, transform.position + new Vector2(-0.27f, 0.0f), Quaternion.identity);
            Instantiate(m_BulletPrefab, transform.position + new Vector2(0.27f, 0.0f), Quaternion.identity);
            m_TimeOfLastShot = Time.time;
        }
    }
}

```

Our bullets should now be firing only every `m_TimeBetweenShots` seconds.




// ----------- DATASETUP: ADD GIF OF BULLETS FIRING CORRECTLY ---------------- //



