using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public bool isGameOver = false;

    public bool isStealthTime = false;

    public int score = 0;

    //UI
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI MICText;

    //Sounds
    private AudioSource gameAudio;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GameObject.Find("Game Manager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            GameOver();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameAudio.PlayOneShot(clickSound);
            PauseGame();
        }

        if (isGameActive)
        {
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if (score == 0)
        {
            MICText.text = "Aw c'mon! You didn't even try!";
        }
        else if (score <= 5)
        {
            MICText.text = "Nice round! But you can do better than that!";
        }
        else if (score <= 10)
        {
            MICText.text = "You're on fire! Keep it up!";
        }
        else if (score <= 15)
        {
            MICText.text = "Wow! You got moves!";
        }
        else
        {
            MICText.text = "That's Insane! You're in the zone!";
        }
        gameOverScreen.SetActive(true);
        isGameOver = true;
    }

    public void RestartGame()
    {
        gameAudio.PlayOneShot(clickSound);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitToMenu()
    {
        gameAudio.PlayOneShot(clickSound);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        gameAudio.PlayOneShot(clickSound);
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gameAudio.PlayOneShot(clickSound);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
