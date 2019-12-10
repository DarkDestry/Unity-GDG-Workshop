using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    public GameObject deathExplosion;

    public void TakeDamage(int damageAmt)
    {
        currentHealth -= damageAmt;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public float GetPercentageHealth()
    {
        return (float)currentHealth / maxHealth;
    }
}
