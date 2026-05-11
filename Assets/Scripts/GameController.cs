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

    public GameObject resultPanel;


    void Start()
    {
        isGameOver = restart = false;
        // Logic Spawn đã được chuyển sang cho LevelManager và SpawnStrategy
    }

    void Update()
    {

    }

    public void GameOver()
    {

        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        resultPanel.SetActive(true);
        resultPanel.GetComponent<Animator>().SetTrigger("Show");
        scoreText.gameObject.SetActive(false);

        // Cho phép người chơi khởi động lại game sau khi thua
        restart = true;

        ResultPanel resultPanelScript = resultPanel.GetComponent<ResultPanel>();
        if (resultPanelScript != null)
        {
            resultPanelScript.ShowResult(score, false);
        }

    }

    public void LevelComplete()
    {
        isGameOver = true;
        gameOverText.text = "Level Complete!";
        gameOverText.gameObject.SetActive(true);
        resultPanel.SetActive(true);
        resultPanel.GetComponent<Animator>().SetTrigger("Show");
        scoreText.gameObject.SetActive(false);

        // Cho phép người chơi khởi động lại game sau khi hoàn thành level
        restart = true;

        ResultPanel resultPanelScript = resultPanel.GetComponent<ResultPanel>();
        if (resultPanelScript != null)
        {
            resultPanelScript.ShowResult(score, true);
        }
    }

    public void Restart()
    {
        resultPanel.GetComponent<Animator>().SetTrigger("Close");
        
        StartCoroutine(WaitToRestart(.75f)); // 
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    IEnumerator WaitToRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
