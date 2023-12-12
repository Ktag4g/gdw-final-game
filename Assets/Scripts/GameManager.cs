using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public bool GameOver = false;

    public bool isStealthTime = false;

    public int score = 0;

    //UI
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            gameOverText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        if (isGameActive)
        {
            UpdateScore();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
