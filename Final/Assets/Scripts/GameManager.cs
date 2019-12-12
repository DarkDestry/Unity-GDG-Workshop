using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }

    public void AddScore(int amt)
    {
        score += amt;
    }

    public int GetScore()
    {
        return score;
    }
}
