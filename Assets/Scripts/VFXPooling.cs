using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPooling : MonoBehaviour
{
    public Queue<GameObject> poolEnemy = new Queue<GameObject>();
    public Queue<GameObject> poolAsteroid = new Queue<GameObject>();
    public int amountToPool;
    // prefabs[0] = astaroid
    // prefabs[1] = player
    // prefabs[2] = enenmy
    public GameObject[] prefabs;


    public static VFXPooling sharedInstance;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            // vfx asteroid explosion
            AddObjectToPool(prefabs[0], poolAsteroid);

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

    [ContextMenu("Test Spawn Asteroid VFX")]
    public GameObject SpawnFromPool(GameObject prefab, Queue<GameObject> pool)
    {
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
        StartCoroutine(ReturnVFXToPool(asteroid, poolAsteroid));
    }

    public void ReturnEnemyVFXToPool(GameObject enemy)
    {
        StartCoroutine(ReturnVFXToPool(enemy, poolEnemy));
    }

    IEnumerator ReturnVFXToPool(GameObject objFx, Queue<GameObject> pool)
    {
        ParticleSystem fx = objFx.GetComponent<ParticleSystem>();
        if (fx != null) yield return new WaitWhile(() => fx.IsAlive(true));

        objFx.SetActive(false);
        pool.Enqueue(objFx);
    }
}
