using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;

    [Range(0, 1)]
    public float movementSpeed = 0.5f;

    [Range(0, 1)]
    public float movementSpeedShift = 0.2f;

    [Range(0, 1)]
    public float fireSpeed = 0.2f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firePos1, firePos2;

    private Rigidbody2D rigidbody;

    private float timeSinceLastFire;

    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleWeapons();

        if (health.IsDead())
        {
            Die();
        }
    }

    void HandleMovement()
    {
        Vector2 position = rigidbody.position;
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeedShift : movementSpeed;

        position.x += Input.GetAxis("Horizontal") * moveSpeed;
        position.y += Input.GetAxis("Vertical") * moveSpeed;
        
        position.x = Mathf.Max(Mathf.Min(2.5f, position.x), -2.5f);
        position.y = Mathf.Max(Mathf.Min(4.0f, position.y), -4.0f);

        rigidbody.MovePosition(position);
    }

    void HandleWeapons()
    {
        if (bulletPrefab == null)
            return;

        if (Input.GetAxis("Fire1") > 0 &&  Time.time - timeSinceLastFire > (1 - fireSpeed))
        {
            Instantiate(bulletPrefab, firePos1.position, Quaternion.identity);
            Instantiate(bulletPrefab, firePos2.position, Quaternion.identity);
            timeSinceLastFire = Time.time;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
