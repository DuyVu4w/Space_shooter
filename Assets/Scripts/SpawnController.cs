using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    public GameObject[] rockPrefab;
    public GameObject[] powerUpPrefab;
    public GameObject enemyPrefab;


    // spawn Random Rock
    public void SpawnRock(Transform pos)
    {
        int index = Random.Range(0, rockPrefab.Length);
        Instantiate(rockPrefab[index], pos.position, Quaternion.identity);
    }

    public void SpawnPowerUp(Transform pos)
    {
        int index = Random.Range(0, powerUpPrefab.Length);
        Instantiate(powerUpPrefab[index], pos.position, Quaternion.identity);
    }

    public void SpawnEnemy(Transform pos)
    {
        Instantiate(enemyPrefab, pos.position, Quaternion.identity);
    }
}
