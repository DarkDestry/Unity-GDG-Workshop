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

Change the gravity scale of the rigidbody to 0 to disable gravity.

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

Select your asteroid and Tag it as an enemy. Dont forget to apply the prefab changes via the override menu after you changed the tag.

Similarly, tag the Player gameobject as `Player`.

## Scripting the Asteroid

Navigate back to the scripts folder that you created and create a new script called `Enemy`. 

Enemies will need to be able to move. We will do this exactly like how we moved the bullet in the last chapter, except in the opposite direction.

```csharp
public class Enemy : MonoBehaviour
{
    [Range(0, 20)]
    public float m_Speed = 20;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -m_Speed);
        Destroy(this.gameObject, 20);
    }
}
```

We will also give the the asteriod some random rotation to make it appear more natural.

```csharp
void Start()
{
    GetComponent<Rigidbody2D>().velocity = new Vector2(0, -m_Speed);
    GetComponent<Rigidbody2D>().angularVelocity = Random.Range(45, 180);
    Destroy(this.gameObject, 20);
}
```

> `Random.Range(a, b)` returns a random value in the range [a, b). `angularVelocity` is in degrees / second.

The asteriod should now fall towards the bottom of the screen.

![Asteroid Falling](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/AsteroidFalling.gif?raw=true)



The asteriod should be able to be destroyed. This may happen when it is shot, or when it collides with the player. We will create a helper function `Die()` in the enemy class. We will simply kill the object for now, but can come back to deal with spawning explosions and adding of score later.


```csharp
public class Enemy : MonoBehaviour
{
    ...

    public void Die()
    {
        Destroy(this.gameObject);
    }
}

```

At the moment, the `Die()` function is not being called by us. We will call it when the enemy collides with the player. We can do this easily with one of Unity's event callback function, `OnTriggerEnter2D()`. This function must take in a `Collider2D` object. When Unity' calls this callback function, it will assign the `Collider2D` parameter with the object that we've collided with.

```csharp
public class Enemy : MonoBehaviour
{
    ...
    private void OnTriggerEnter2D(Collider2D other)
    {

    }
}
```

Inside this function, we will first check if the object that we collided with is the player. Recall that we tagged the player as "Player" earlier. 


```csharp
public class Enemy : MonoBehaviour
{
    ...
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Die();
        }
    }
}
```

Run the game and fly into an asteriod. The asteriod gameobject should be destroyed. Of course, this should damage the player as well. We will deal that this in moment. Before that, we should also make sure that the bullet can destroy the enemy. We will do this in `Bullet.cs`:

```csharp
public class Bullet : MonoBehaviour
{
    ...

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e)
        {
            e.Die();
            Destroy(this.gameObject);
        }
    }
}

```

We checked if the collision object is an enemy using a different method to illustrate the different ways to achieve similar results. In this case, we can safely assume that an object is an enemy if it contains the `Enemy.cs` component.

The bullet should now be able to destroy asteriods. 

## Damaging the Player

To simplify adding a player "health", as well as for easy extensions in the future, we will create a new component `Health.cs`.

The health component stores the current and max health as well as some helper functions.

```csharp
using UnityEngine;

public class Health : MonoBehaviour
{
    public int m_MaxHealth = 100;
    public int m_CurrentHealth = 100;

    public void TakeDamage(int damageAmt)
    {
        m_CurrentHealth -= damageAmt;
    }

    public bool IsDead()
    {
        return m_CurrentHealth <= 0;
    }

    // This will be convenient for setting UI elements in the future
    public float GetPercentageHealth()
    {
        return (float)m_CurrentHealth / m_MaxHealth;
    }
}

```

Attach the health component to the Player gameobject, and create a reference to it in the `Player` class.

![Health Component](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/HealthComponent.png?raw=true)

```csharp

public class Player : MonoBehaviour
{
    ...

    public Health m_Health;

    ...

    void Start()
    {
        ...
        m_Health = GetComponent<Health>();
    }
}

```

In the player's `Update()` function, we will check if the player's health is ever below 0. If so, we will kill the player.

```csharp
void Update()
{
    ...
    if (m_Health.IsDead())
    {
        Die();
    }
}

...

void Die()
{
    Destroy(gameObject);
}

```

At this point, ideally we should be able to call `Player.health.TakeDamage(amt)` from the enemy (and in the future from lots of other places). We will make do this by creating a static reference to the player, in the player class. This is known as a "Singleton Pattern". We can do this because we know there will only ever be one player object.

Create a static Player reference in the Player class, and asign it in `Start()`

```csharp
public class Player : MonoBehaviour
{
    public static Player Instance;

    ...

    void Start()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;

        ...
    }

}

```

We will destroy any other instances of players each time we create a new `Player`. Once again, we can do this because we know there should only be one instance of `Player` at any point in time.

Finally, in `Enemy.OnTriggerEnter2D()`, we can decrement the player's health. We will hardcode the damage value for now. Feel free to parameterize it as we did for `m_Speed` and various other properties.

```csharp
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.tag == "Player")
    {
        Die();
        Player.Instance.m_Health.TakeDamage(10);
    }
}
```

## Spawning Asteriods

To spawn asteriods, we will create a new gameobject called EnemySpawner and position it at the top of the scene just outside the camera bounds. We will use the gameobject to position where the asteriods are spawned.

![Enemy Spawner](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/spawner.png?raw=true)




Create a `EnemySpawner.cs` component and attach it to the EnemySpawner game object.

In the new script, we will create an array of enemy gameobject prefabs so that we can randomly spawn one from the array each time.

```csharp
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] m_Enemies;
}
```


To have a variety of asteroids to spawn, we need to create more asteroids for the spawner to instantiate. To do this simply, drag the asteroid prefab into the scene, and change the sprite for the object. Drag the asteroid game object back into the prefab menu to create a new prefab.

To change the sprite quicky, you can select the ball icon next to the sprite field and selecting a sprite from the window that pops up. You can close the select window after selecting a new sprite.


![Quick Sprite Selection](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/AsteroidsChangeSprite.png?raw=true)

Repeat the steps for all 3 other sprites and you should have the following result in your prefabs folder


![Asteroid Prefabs](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%205/AsteroidPrefabs.png?raw=true)




We will create some properties for defining the spawn rates as well as private members to keep track of the time since last spawning an asteriod. This is very similar to how we controlled the spawn speed of bullets in Chapter 4.


```csharp
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] m_Enemies;

    [Range(0, 10)]
    public float m_SpawnRate = 1.2f;

    private float m_TimeSinceLastSpawn;

    void Update()
    {
        if (Time.time - m_TimeSinceLastSpawn > m_SpawnRate)
        {
            m_TimeSinceLastSpawn = Time.time;

            SpawnAtRandomPosition();
        }
    }
}

```


We will create a `SpawnAtRandomPosition()` function to select a random prefab and a random position to spawn the asteriod.


```csharp
void SpawnAtRandomPosition() 
{
    int randomEnemyIndex = Random.Range(0, enemies.Length);
    Vector2 spawnPosition = transform.position;
    spawnPosition.x += Random.Range(-2.0f, 2.0f);
}

```

The (-2.0f, 2.0f) offset is purely empirical. This is roughly the the width of our screen.

At the end of `SpawnAtRandomPosition()`, we will instantiate the random asteriod.

```csharp
void SpawnAtRandomPosition() 
{
    int randomEnemyIndex = Random.Range(0, enemies.Length);
    Vector2 spawnPosition = transform.position;
    spawnPosition.x += Random.Range(-2.0f, 2.0f);
    Instantiate(enemites[randomEnemyIndex], spawnPosition, Quaternion.identity);
}

```

Asteriods should now spawn from the top of the screen and fall down towards the player.













