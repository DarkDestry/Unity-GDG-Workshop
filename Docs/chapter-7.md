# Chapter 6 - UI

In this chapter we will be creating the UI for the game.

## Scene Setup

All UI elements in Unity need to be placed on a canvas. To create a canvas, right click on the empty space in the hierarchy and select `UI > Canvas`

![Create Canvas](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/CreateCanvas.png?raw=true)

Right click the new canvas object that is just created in the hierarchy and select `UI > Text` to add two Text elements. These 2 text elements will be our score text. Name these 2 elements `ScoreLabel` and `Score`

To make sure that the UI remains the same size regardless of monitor resolution we need to set the scale mode of the canvas to scale with screen size. Select the Canvas on the hierarchy and change the `UI Scale Mode` in the inspector to `Scale with screen size`.

![Canvas Scale](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/CanvasScale.png?raw=true)

Head over to the scene view. Note that you cannot see your UI elements. This is because the UI elements use a different transform called a `Rect Transform` that is used for UI elements. These transforms are much larger than your game scene to allow for non blurry UI elements. To view the UI elements, double click the canvas in the hierarchy.

![Canvas Focus](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/CanvasFocus.gif?raw=true)

## Score UI Setup

Our text will be placed against our current background and would not be visible if it is not white. To change the color, select the text element in the hierarchy and change the color element. Change the color to white.

![Color Selection](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/ColorSelection.png?raw=true)

![White Text](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/WhiteText.png?raw=true)

Currently, the placeholder text for the text element is `New Text`. We want the label to say `Score:` and the score itself to contain some placeholder number so that we can position them.

Select the `ScoreLabel` element and change the text in the text component to `Score:`

Similarly select the `Score` element and change the text to `00000000000` as a placeholder for some numbers.

![Score Text](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/ScoreText.png?raw=true)

Since our score will be placed in the bottom left corner of the screen, we want to anchor our UI element to the bottom left corner of the screen. Select the `ScoreLabel` and select the square icon at the top left of the the `Rect Transform` component. Left click on the bottom left icon in the middle 3x3 grid. Do the same for the `Score` text element

![Rect Transform Anchor](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/RectAnchor.png?raw=true)

We can now position our UI elements. Select the UI elements and move them to the bottom right.


![Moving UI](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/MovingUI.gif?raw=true)

## Healthbar UI Setup

Navigate to your Sprite folder in the project explorer. You should find the Healthbar UI element in there. First Create an Image UI element for the healthbar. Right click the canvas and select `UI > Image`. Rename the object `Healthbar`. On the inspector window of the Image UI object, there is an image component. drag the Healthbar image into the `Source Image` field of the component. Select `Preserve Aspect` to not stretch the image.

![ImageComponent](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/ImageComponent.png?raw=true)

Set the anchor to the top left and move the healthbar to the top left of the canvas

![Image Anchor](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/ImageAnchor.png?raw=true)

Your UI in the scene view should now look like this

![Final UI](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/FinalUI.png?raw=true)

We now need to make the healthbar reducable. To do this, we need to set the image type to be filled. This allows us to change a slider value to change how much the health value to be displayed is.

Select the Healthbar in the hierarchy and change the `Image Type` in the Image Component to `Filled`. Additionally you want it to fill horizontally from the left. Change the `Fill Method` to `Horizontal` and `Fill Origin` to `Left`.

![Image Fill Mode](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/ImageFillMode.png?raw=true)

Now drag the fill slider below and watch your healthbar decrease. Note that the `+` Icon for the healthbar dissappears as well when the fill value reaches 0. This is not what we want. To keep the healthbar icon there, first drag the fill slider such that only the icon is visible. 

![Image Fill Icon](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/FillIcon.png?raw=true)

Now duplicate the healthbar by selecting it in the hierarchy and pressing `Ctrl-D`. There should be now two healthbar objects. Rename one of them to `Healthbar Icon`.

This should be what your canvas looks like in the hierarchy.

![Canvas Hierarchy](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/CanvasHierarchy.png?raw=true)

You can now leave the `Healthbar Icon` alone and adjust the fill slider of the `Healthbar` object. Note that the Icon is still there because there is a second image showing just the icon.

![Final Healthbar](https://github.com/DarkDestry/Unity-GDG-Workshop/blob/master/Docs/Images/Chapter%207/FinalHealthbar.gif?raw=true)

## Storing a score for the game session

We do not yet have a way to store the total score the player has attained. We will store this value in the `GameManager` as a private member because we want to encapsulate the value to prevent ourselves from changing the value arbitrarily.

```csharp
public class GameManager : MonoBehaviour
{
    private int m_Score = 0;
    ...

    public void AddScore(int amt)
    {
        m_Score += amt;
    }

    public int GetScore()
    {
        return m_Score;
    }
}
```

We want to increment the score when an asteroid is destroyed by the player. To do this, call `AddScore()` from `Enemy.Die()`. However, we also call `Enemy.Die()` when the asteriod collides with the player. We certainly don't want to add score then. Since a lot of code has already been written, we will add a new boolean property to `Enemy.Die()` with a default value.

```csharp
// change
public void Die() {...}

// to 
public void Die(bool addScore = true) { ... }
```

Inside the `Die()` method,

```csharp
public void Die(bool addScore = true)
{
    ...

    if (addScore)
        GameManager.Instance.AddScore(100); // We will hard code this value for now
}
```

And we will need to fix the call to `Die` inside `Enemy.OnTriggerEnter2D()`


```csharp
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.tag == "Player")
    {
        Player.Instance.health.TakeDamage(10);
        Die(false); // Make sure addScore = false when the asteriod dies because collides with player
    }
}
```

The score property should now be updated in the `GameManager`. We will now proceed to update the UI text accordingly.

## Updating the UI components dynamically

Create a new script `UIManager.cs` and attach it to the `GameManager` object.

In this script, we will grab references to the individual UI elements (health bar, score text) and update them in realtime with values from the game manager.

We will first need to include `UnityEngine.UI` at the top of the file since Unity's UI classes are in their own namespace (packages, if you're from a Java background).

```csharp
using UnityEngine;
using UnityEngine.UI;

...
```

We will need to create reference to the health `Image` and the score `Text`.

```csharp
// UIManager Members
public Text m_ScoreText;
public Image m_HealthBar;
```

Assign the `Healthbar` and `Score` UI Objects that we created earlier into the fields in the inspector.

In the `Update()` function, we will update 1) the healthbar's fill amount, and 2) the score's text.

```csharp
void Update()
{
    if (m_ScoreText != null)
        m_ScoreText.text = GameManager.Instance.GetScore().ToString();

    if (m_HealthBar != null)
        m_HealthBar.fillAmount = Player.Instance.m_Health.GetPercentageHealth();
}

```

Start playing the game and the UI should be updated as you play!

Final code for UIManager should look something like this:


```csharp
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text m_ScoreText;
    public Image m_HealthBar;

    // Update is called once per frame
    void Update()
    {
        if (m_ScoreText != null)
            m_ScoreText.text = GameManager.Instance.GetScore().ToString();

        if (healthBar)
            m_HealthBar.fillAmount = Player.Instance.health.GetPercentageHealth();
    }
}
```






























