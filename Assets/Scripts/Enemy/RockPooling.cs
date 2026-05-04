using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPooling : Singleton<RockPooling>
{
    public GameObject[] rockPrefabs;
    public Queue<GameObject> pool = new Queue<GameObject>();
    public int amountToPool;

    void Start()
    {
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(rockPrefabs[i % rockPrefabs.Length],transform.position, Quaternion.identity, transform);
            temp.SetActive(false);
            pool.Enqueue(temp);
        }
    }

    public GameObject GetRock()
    {
        GameObject temp = pool.Dequeue();
        if (temp != null)
        {
            temp.SetActive(true);
        }
        return temp;
    }

    public void ReturnToPool(GameObject rock)
    {
        rock.SetActive(false);
        pool.Enqueue(rock);
    }
}
