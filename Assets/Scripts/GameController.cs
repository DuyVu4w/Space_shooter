using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // spawn rock
    public float posX;
    public float posZ;
    public int maxAsteroid = 15;
    private int numberOfAsteroid;
    private float startWait = 4;
    private float waveWait = 3;

    // game control
    public bool isGameOver, restart;
    public int score = 0;
    public float speed;
    //UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    void Start()
    {
        numberOfAsteroid = 5;
        isGameOver = restart = false;
        StartCoroutine(Waves());
    }

    void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    IEnumerator Waves()
    {
        yield return new WaitForSeconds(startWait);
        
        while (!isGameOver)
        {
            for (int i = 0; i < numberOfAsteroid; i++)
            {
                yield return new WaitForSeconds(0.3f);
                GameObject temp = RockPooling.sharedInstance.GetRock();
                Rigidbody rb = temp.GetComponent<Rigidbody>();
                rb.linearVelocity = Vector3.zero; // Reset vận tốc (Reset velocity)
                rb.angularVelocity = Vector3.zero;
                temp.GetComponent<Rigidbody>().linearVelocity = Vector3.back * speed;
                temp.transform.position = new Vector3(Random.Range(-posX, posX), 0, posZ);
            }
            numberOfAsteroid++;

            numberOfAsteroid = Mathf.Min(numberOfAsteroid, maxAsteroid);
            yield return new WaitForSeconds(waveWait);
        }
        restart = true;
        restartText.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
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
