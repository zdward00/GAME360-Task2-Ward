using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifetime = 3f;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        // Destroy bullet after lifetime
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Bullet hit enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(1);
                GameManager.Instance.AddScore(100);
                Destroy(gameObject); // Destroy bullet
            }
        }
        // Destroy bullet if it hits walls or boundaries
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
