using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPooling : Singleton<VFXPooling>
{
    public Queue<GameObject> poolEnemy = new Queue<GameObject>();
    public Queue<GameObject> _poolRock  = new Queue<GameObject>();
    public int amountToPool;

    // prefabs[0] = astaroid
    // prefabs[1] = player
    // prefabs[2] = enenmy
    public GameObject[] prefabs;

    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            // vfx asteroid explosion
            AddObjectToPool(prefabs[0], _poolRock);

            // vfx enemy explosion
            AddObjectToPool(prefabs[2], poolEnemy);
        }
    }

    [ContextMenu("Test Spawn Asteroid VFX")]
    public GameObject AddObjectToPool(GameObject prefab, Queue<GameObject> pool)
    {
        GameObject temp = Instantiate(prefab, transform);
        temp.SetActive(false);
        pool.Enqueue(temp);
        return temp;
    }
    

    [ContextMenu("Test Spawn Rock VFX")]
    public GameObject SpawnFromPool(string tag)
    {
        if (tag == "Rock" && _poolRock.Count > 0)
        {
            var temp = SpawnVFXFromPool(_poolRock);
            ReturnAsteroidVFXToPool(temp);
            return temp;
            
        }
        else if (tag == "Enemy" && poolEnemy.Count > 0)
        {
            var temp = SpawnVFXFromPool(poolEnemy);
            ReturnEnemyVFXToPool(temp);
            return temp;
        }
        return null;
    }

    public GameObject SpawnVFXFromPool(Queue<GameObject> pool)
    {
        if (pool.Count == 0) return null;

        GameObject temp = pool.Dequeue();
        if (temp)
        {
            temp.SetActive(true);
        }

        // reset particle system (reset trangh thái của particle system để nó có trở lại từ đầu)
        var ps = temp.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Clear();
            ps.Play();
        }
        
        // play audio source if exists (có particle system để phát âm thanh nổ)
        var audio = temp.GetComponent<AudioSource>();
        if (audio != null) audio.Play();
        return temp;
    }
    public void ReturnAsteroidVFXToPool(GameObject asteroid)
    {
        StartCoroutine(ReturnVFXToPool(asteroid, _poolRock));
    }

    public void ReturnEnemyVFXToPool(GameObject enemy)
    {
        StartCoroutine(ReturnVFXToPool(enemy, poolEnemy));
    }

    IEnumerator ReturnVFXToPool(GameObject objFx, Queue<GameObject> pool)
    {
        ParticleSystem fx = objFx.GetComponent<ParticleSystem>();
        if (fx != null) yield return new WaitWhile(() => fx.IsAlive(true));
        else yield return new WaitForSeconds(1f); // Nếu không có ParticleSystem, chờ 1 giây trước khi trả về pool
        
        objFx.SetActive(false);
        pool.Enqueue(objFx);
    }
}
