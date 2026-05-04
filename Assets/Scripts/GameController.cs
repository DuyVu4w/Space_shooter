using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    // game control
    public bool isGameOver, restart;
    public int score = 0;
    
    // speed dùng chung cho các script khác
    public float speed;

    //UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    void Start()
    {
        isGameOver = restart = false;
        // Logic Spawn đã được chuyển sang cho LevelManager và SpawnStrategy
    }

    void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        
        // Cho phép người chơi khởi động lại game sau khi thua
        restart = true;
        restartText.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
