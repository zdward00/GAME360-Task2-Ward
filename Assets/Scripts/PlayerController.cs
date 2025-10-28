using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerState currentState;
    [Header("Movement")]
    public float moveSpeed = 5f;
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    [Header("Audio")]
    public AudioClip ShootSound; //this is where you put your mp3/wav files
    public AudioClip CoinSound;
    private AudioSource audioSource;//Unity componenet
    private Rigidbody2D rb;

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Configure AudioSource for sound effects
        audioSource.playOnAwake = false;
        audioSource.volume = 0.7f; // Adjust volume as needed

       currentState = new IdleState();
       currentState.EnterState(this);
    }
    public void Update()
    {
        //HandleMovement();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.velocity = movement * moveSpeed;
        HandleShooting();
        currentState.UpdateState(this);
    }
   public void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.velocity = movement * moveSpeed;
    }
    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }
    private void FireBullet()
    {
        if (GameManager.Instance.score > 499 && GameManager.Instance.score < 1000)
            fireRate = 0.3f;
        if (GameManager.Instance.score > 1000)
            fireRate = 0.1f;
        if (projectilePrefab && firePoint)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
        audioSource.PlayOneShot(ShootSound);
        // Play shoot sound effect
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Player hit by enemy - lose a life
            GameManager.Instance.LoseLife();
        }

       

    }

    public void ChangeState(PlayerState newState)
    {
        // Exit old state
        currentState.ExitState(this);

        // Switch to new state
        currentState = newState;

        // Enter new state
        currentState.EnterState(this);
    }
}
