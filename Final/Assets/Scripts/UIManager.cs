using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text scoreText;
    public Image healthBar;

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
            scoreText.text = GameManager.Instance.GetScore().ToString();

        if (healthBar)
            healthBar.fillAmount = Player.Instance.health.GetPercentageHealth();
    }
}
