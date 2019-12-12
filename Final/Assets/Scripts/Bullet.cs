using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0, 20)]
    public float speed = 15;

    [Range(0, 20)]
    public int damage = 15;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e)
        {
            e.TakeDamage(damage);
            Destroy(gameObject);
            GameObject spawnedExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            spawnedExplosion.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            Destroy(spawnedExplosion, 5);
        }
    }
}
