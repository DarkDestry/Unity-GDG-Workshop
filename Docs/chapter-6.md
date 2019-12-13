# Chapter 6 - Explosion

In this chapter we will be creating the explosion effect.

## Explosion animation

The explosion is an animated via a sequence of sprites. You can find the spritesheet for the animation in the `Sprites` folder. To animate the sprites, we will need a animation controller and an animation sequence. 

Create a new folder in the `Sprites` folder called `Explosion` and drag the explosion spritesheet in. Open the folder and create two new objects via the create menu `Animator Controller` and `Animation`. Name them `ExplosionController` and `Explosion` respectively.

![Explosion Folder in Assets](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/ExplosionFolder.png?raw=true)

## Animation and Animator Window

To edit animations, you need two new windows. The animation window and the animator window. you can open them under the window menu at the top menu bar.

![Animation windows](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/AnimationWindows.png?raw=true)

## Game Object setup

Drag the first sprite in the explosion animation into the hierarchy. This will be our placeholder object as we setup the animation. Rename the object to `Explosion` and add the `Animator` component onto the object.

Select our animatior controller that we just created and drag it into the controller field. 

![Attaching Animatior Controller](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/AnimationControllerAttaching.png?raw=true)

## Animator Controller Setup

The animator controller allows you to sequence multiple different animation and blend between them. However in our case, we only need to play one animation.

Double click the `ExplosionController` in your project explorer. The Animator window should pop up. drag your explosion animation intor the the Animator window. You should see a yellow tile with the label Explosion appear.

![Attaching Animation](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/AttachAnimation.png?raw=true)

## Animation setup

The animation you just attached to the controller is currently empty. We will need to add the sprite sequence to the animation.

To do this, open your Animation window. Select the explosion object in the hierarchy. If everthing was done correctly up to this point, your animaton window should look like this

![Animation Window](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/AnimationWindowEditting.png?raw=true)

Select `Add Property` and click `Sprite Renderer > Sprite`. This will allow you to change the sprite animation.

![Sprite Animation Track](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/SpriteAnimationTrack.png?raw=true)

![Track Added](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/TrackAdded.png?raw=true)

To start with a clean slate, select the two keyframes on the track and delete them.

![Deleting Existing Keyframes](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/DeletingExistingKeyframes.png?raw=true)

To add your animation frames, select all the frames by selecting the first and shift selecting the last item and drag it into the animation window. Move your keyframes such that the first frame start at the 0:00 line.

![Adding Keyframes](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/AddingKeyframes.png?raw=true)

At this point, if you click and drag through the timeline above, you should be able to see the animation

![Playing Animation](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/AnimationPlaying.gif?raw=true)

If the animation is playing to quickly for you, this is normal. Unity by default assumes your animation is at 60 frames per second. To change this, select the gear at the right of the timeline and click `Show Sample Rate`

![Show Sample Rate](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/ShowSampleRate.png?raw=true)

You should now see a samples field above the sprite track. A higher sample value means more frames are played per second. As our sprite sheet only has 12 frames. Setting it to 20 should give us a 0.6s explosion.

![Sample Field](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/SampleField.png?raw=true)

Your final explosion should look like this

![Final Explosion](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%206/FinalExplosion.gif?raw=true)

## Create a prefab

As this animation will be played whenever there is an explosion, create a prefab by dragging the explosion object into the prefabs folder.

## Spawning explosions 

On one hand we could write code for spawning explosions with a position, rotation and scale in every script that requires spawning explosions (enemy, bullet, player, etc), it would be much easier if we had a function taht we can call to spawn an explosion with a random rotation/scale at some location.

To do this, we will create a `GameManager` class, and attach this component to a new GameObject that we will also call "Game Manager".

We will also follow the singleton pattern for the game manager as we know that there should only ever be one game manager.

```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

}

```

We will create a field for the explosion prefab and assign it accordingly in the inspector.

```csharp
public class GameManager : MonoBehaviour
{
    public GameObject m_ExplosionPrefab;

    ...
}

```

We will also create a function to spawn an explosion with a random rotation at a certain position. The explosion scale we will leave as a parameter in case different scripts require different sized explosions.


```csharp
public void SpawnExplosionAt(Vector2 position, float scale)
{
    GameObject explosion = Instantiate(m_ExplosionPrefab, position, Quaternion.identity);

    // Rotate around z-axis by a random amount
    explosion.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));

    // Scale the explosion by the input parameter, with a little bit of randomness
    explosion.tranform.localScale = Vector2.one * scale * Random.Range(0.9f, 1.1f);

    // Destroy the explosion after the animation has finished playing
    Destroy(explosion, 5);
}
```

There are currently 3 places where we might want to spawn an explosion.

### Enemy.Die()

```csharp
public void Die()
{
    GameManager.Instance.SpawnExplosionAt(transform.position, 1);
    Destroy(this.gameObject);
}

```

### Bullet.OnTriggerEnter2D()


```csharp
private void OnTriggerEnter2D(Collider2D other)
{
    Enemy e = other.GetComponent<Enemy>();
    if (e)
    {
        e.Die();
        Destroy(this.gameObject);

        // Spawn explosion
        GameManager.Instance.SpawnExplosionAt(transform.position, 0.2f);
    }
}
```

### Player.Die()

```csharp
void Die()
{
    GameManager.Instance.SpawnExplosionAt(transform.position, 1.5f);
    Destroy(gameObject);
}
```

With all the `SpawnExplosionAt()` calls done, we should get explosions when our bullet hits an asteriod, when the asteriod is destroyed, and when the player is destroyed.
