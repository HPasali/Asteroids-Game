using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Projectile(Vector2 force)
    {
        rb2D.AddForce(force * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
