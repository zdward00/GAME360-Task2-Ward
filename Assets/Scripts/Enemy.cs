using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health = 1;
    public float moveSpeed = 2f;
    [Header("AI")]
    public float detectionRange = 7f;
    private Transform player;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Find player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj) player = playerObj.transform;
    }
    private void Update()
    {
        ChasePlayer();
    }
    private void ChasePlayer()
    {
        if (player)
        {
            if (GameManager.Instance.score > 1000)
                moveSpeed = 3f;
            if (GameManager.Instance.score > 2000)
                moveSpeed = 4f;
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
                rb.AddForce(direction * moveSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // This is where Singleton shines!
        // Any enemy can easily notify the GameManager
        GameManager.Instance.EnemyKilled(); //update the score of the player
        Destroy(gameObject); // the enemy gets destroyed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            moveSpeed *= -1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Custom 2D circle for older Unity versions
        Gizmos.color = Color.red;
        int segments = 32;
        float angle = 0f;
        Vector3 lastPos = transform.position + new Vector3(detectionRange, 0, 0);
        for (int i = 1; i <= segments; i++)
        {
            angle = (i * 360f / segments) * Mathf.Deg2Rad;
            Vector3 newPos = transform.position + new Vector3(
            Mathf.Cos(angle) * detectionRange,
            Mathf.Sin(angle) * detectionRange,
            0
            );
            Gizmos.DrawLine(lastPos, newPos);
            lastPos = newPos;
        }
    }
}