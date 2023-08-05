using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text lifeNumberText, scoreText;
    public GameObject gameOverScreen;
    public Player player;
    public ParticleSystem explosion;
    private int respawnTime = 3;
    private int collisionReturnTime = 3;
    private float score = 0;
    private bool isGameOver = false;
    private AudioSource audioSource;
    public AudioClip asteroidDestroySound, gameOverSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        scoreText.text = score.ToString();
        lifeNumberText.text = player.life.ToString();
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            RestartScene();
        }
    }

    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        player.life--;
        lifeNumberText.text = player.life.ToString();
        player.gameObject.SetActive(false);
        if(player.life <= 0)
        {
            GameOver();            
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
        
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        audioSource.clip = asteroidDestroySound;
        audioSource.Play();
        explosion.Play();
        if(asteroid.size < 0.75f)
        {
            score += 100.0f;
        }
        else if(asteroid.size < 1.2f)
        {
            score += 50.0f;
        }
        else
        {
            score += 25.0f;
        }
        scoreText.text = score.ToString();
    }

    public void Respawn()
    {
        player.gameObject.SetActive(true);
        player.gameObject.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("NoCollision");
        Invoke(nameof(TurnOnCollision), collisionReturnTime);
    }

    public void TurnOnCollision()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()
    {
        explosion.Stop();
        audioSource.clip = gameOverSound;
        audioSource.Play();
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartScene()
    {
        gameOverScreen.SetActive(false);
        isGameOver = false;
        score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
