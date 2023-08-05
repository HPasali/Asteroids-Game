using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] asteroidSprites;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    public float size, minSize, maxSize, speed, maxLifeTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        transform.localScale = Vector3.one * size;
        rb2d.mass = size;

    }

    public void SetTrajectory(Vector2 direction)
    {
        rb2d.AddForce(direction * speed);
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            if((size / 2) > minSize)
            {
                for (int i = 0; i < 2; i++)
                {
                    CreateSplit();
                }
                Destroy(gameObject);

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void CreateSplit()
    {
        Vector2 spawnLocation = transform.position;
        spawnLocation += Random.insideUnitCircle * 0.5f;
        Asteroid half = Instantiate(this, spawnLocation, this.transform.rotation);
        half.size = size / 2;
        half.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }

}
