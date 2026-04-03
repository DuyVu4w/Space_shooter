using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling sharedInstance;
    public GameObject bulletPrefab;
    public List<GameObject> pool;
    public int amountToPool;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        pool = new List<GameObject>();
        GameObject temp;

        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(bulletPrefab, transform);
            temp.SetActive(false);
            pool.Add(temp);
        }
    }

    public GameObject GetBulletsObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pool[i].activeInHierarchy)
            {

                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }

    public void ReturnToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
