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

![Adding Components](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%203/AddComponent.png?raw=true)

> There is no particular reason we used the box collider to approximate the player's collider. If you want, you can select the Circle Collider or the Polygon Collider to approximate the player collider.

## Rigidbody settings

Currently if you play the game in the editor, you should see the player falling. That is due to gravity being enabled for the player. To disable gravity, select the player object in the hierarchy and under the rigidbody settings in the inspector window, set the gravity scale to 0

![Rigidbody Gravity Scale](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%203/RigidbodyGravityScale.png?raw=true)

## Scripting the Player Movement

In the `player` class, we will first handle player movement. Create the following public members:

```csharp
[Range(0, 1)]
public float m_MovementSpeed = 0.5f;

[Range(0, 1)]
public float m_MovementSpeedShift = 0.2f;
```
The `[Range(0, 1)]` property will turn the field generated into a slider. The values (0, 1) represents the min and max values of the slider respectively.

![Range(0,1) slider](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%203/RangeSlider.png?raw=true)

Next, we will create a `HandleMovement()` function. We will call this function in our `Update()`.

```csharp
void Update()
{
    HandleMovement();
}

void HandleMovement()
{

}
```

We will move the player through the `RigidBody` component that we added earlier. This is so that movements will not override the `RigidBody`'s physics computations.

We will get a reference to the `RigidBody` in `Start`.

```csharp
...
public class Player : MonoBehaviour
{
    ... 
    private RigidBody2D m_Rigidbody;
    ...

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    ...
}
```

> The 2D variant of `Rigidbody`, `Rigidbody2D`, works with 2D colliders.

Inside `HandleMovement()`, we will get the player's current position, and calculate a new position given some keypresses. While we could do manual checks for input keys, Unity have an Input system that maps multiple keys to the same "Axis". For instance, the axis "Horizontal", is written to by keys `A`, `D`, `Left Arrow`, `Right Arrow`, and left joystick of any controller attached. The value returned by the horizontal axis is a value between `-1` and `1`. For more information on input axis, check out https://docs.unity3d.com/ScriptReference/Input.GetAxis.html.

```csharp
void HandleMovement()
{
    Vector2 position = m_Rigidbody.position;
    float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeedShift : movementSpeed;

    position.x += Input.GetAxis("Horizontal") * moveSpeed;
    position.y += Input.GetAxis("Vertical") * moveSpeed;
    
    m_Rigidbody.MovePosition(position);
}
```

The function `Rigidbody.MovePosition()` will move the player to the newly calculated position. The player will now be able to move in play mode.






// ------------------------- DATASETUP: ADD GIF OF PLAYER MOVING ----------------------------//






You may have noticed that the player can move beyond the boundary of the game window. We would want to clamp the player's position to within the screen.

```csharp
void HandleMovement()
{
    Vector2 position = m_Rigidbody.position;
    float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeedShift : movementSpeed;

    position.x += Input.GetAxis("Horizontal") * moveSpeed;
    position.y += Input.GetAxis("Vertical") * moveSpeed;
    
    // Clamps the player position in each axis.
    position.x = Mathf.Max(Mathf.Min(2.5f, position.x), -2.5f);
    position.y = Mathf.Max(Mathf.Min(4.0f, position.y), -4.0f);

    m_Rigidbody.MovePosition(position);
}
```

The player should now be able to move but not escape the game window.




