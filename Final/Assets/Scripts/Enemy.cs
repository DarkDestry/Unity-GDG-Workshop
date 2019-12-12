using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, 20)]
    public float speed = 1;

    public int points = 100;

    public GameObject explosion;

    public Health health;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(45, 180);

        health = GetComponent<Health>();

        Destroy(gameObject, 20);
    }

    public void TakeDamage(int damage)
    {
        if (health == null)
        {
            Die();
        }

        health.TakeDamage(damage);

        if (health.IsDead())
        {
            Die();
        }
    }

    public void Die(bool addScore = true)
    {
        Destroy(this.gameObject);
        GameObject spawnedExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        spawnedExplosion.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        spawnedExplosion.transform.localScale = Vector2.one;
        Destroy(spawnedExplosion, 5);

        if (addScore)
            GameManager.Instance.AddScore(points);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.Instance.health.TakeDamage(10);
            Die(false);
        }
    }
}
