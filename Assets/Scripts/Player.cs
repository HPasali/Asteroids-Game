using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float thrustSpeed, turnSpeed;
    private float turnDirection;
    private Rigidbody2D playerRigidbody;
    private bool isThrusted;
    public Bullet bulletPrefab;
    public int life;
    public AudioClip shootingSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isThrusted = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Shoot();        
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = -1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = 1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        if(isThrusted)
        {
            playerRigidbody.AddForce(transform.up *  thrustSpeed);
        }
        if(turnDirection != 0.0f)
        {
            playerRigidbody.AddTorque(turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        audioSource.Play();
        bullet.Projectile(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
