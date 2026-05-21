using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SquareSpawn", menuName = "Level Design/Spawn Strategies/Square")]
public class SquareSpawnStrategy : SpawnStrategy
{
    public float gridLength = 10f;

    // Hướng di chuyển vào màn hình
    public Vector3 enterDirection = Vector3.back;

    // Thời gian di chuyển ban đầu
    public float initialMoveDuration = 1.5f;
    
    // Thời gian đứng yên
    public float stopDuration = 2f;
    
    // Gia tốc khi di chuyển lại
    public float acceleration = 10f;

    public override IEnumerator Spawn(string poolName, int count, Transform spawnOrigin, float spawnInterval)
    {
        // tính gird size dựa trên số lượng enemy
        int gridSize = Mathf.Max(1, Mathf.CeilToInt(Mathf.Sqrt(count)));
        
        float spacing = gridSize > 1 ? gridLength / (gridSize - 1) : 0f;   

        // căn giữa
        float centerOffset = gridLength / 2f;

        float speed = 10f;
        int spawnedCount = 0;
        
        List<GameObject> spawnedEnemies = new List<GameObject>();

        for (int x = 0; x < gridSize; x++) 
        {
            for(int y = 0; y < gridSize; y++)
            {
                // dừng nếu đã đủ số lượng hoặc game over
                if (spawnedCount >= count || GameController.Instance.isGameOver)
                {
                    goto WAIT_FOR_ENEMIES;
                }

                float posX = (x * spacing) - centerOffset;
                float posZ = (y * spacing) - centerOffset;

                Vector3 spawnPos = spawnOrigin.position + new Vector3(posX, 0, posZ + 10);

                GameObject temp = ObjectPooler.Instance.SpawnFromPool(poolName);
                if(temp != null)
                {
                    temp.transform.position = spawnPos;
                    // temp.transform.rotation = Quaternion.identity;
                    
                    temp.SetActive(true);

                    Rigidbody rb = temp.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero;
                        GameController.Instance.StartCoroutine(SquareMovementRoutine(temp, rb, speed));
                    }

                    spawnedEnemies.Add(temp);
                    spawnedCount++;
                }
            }
        }


    // label
    WAIT_FOR_ENEMIES:
        // Đợi đến khi tất cả các enemy bị tiêu diệt hoặc rời khỏi màn hình
        while (!GameController.Instance.isGameOver)
        {
            bool allDead = true;
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if (spawnedEnemies[i] != null && spawnedEnemies[i].activeInHierarchy)
                {
                    allDead = false;
                    break; // Vẫn còn enemy sống
                }
            }

            if (allDead)
            {
                break; // Tất cả đã chết hoặc biến mất
            }

            yield return null; // Đợi frame tiếp theo
        }
    }

    private IEnumerator SquareMovementRoutine(GameObject obj, Rigidbody rb, float baseSpeed)
    {
        Debug.Log("obj: " + obj.name);
        if (rb == null) {Debug.Log("rb is null"); yield break;}
        Debug.Log("SquareMovementRoutine");
        Vector3 moveDir = enterDirection.normalized;

        // 1. Di chuyển ban đầu vào khung hình
        rb.linearVelocity = moveDir * baseSpeed;
        
        float timer = 0f;
        while (timer < initialMoveDuration)
        {
            if (obj == null || !obj.activeInHierarchy) yield break;
            timer += Time.deltaTime;
            yield return null;
        }

        if (obj == null || !obj.activeInHierarchy) yield break;

        // đứng yên
        rb.linearVelocity = Vector3.zero;
        
        timer = 0f;
        while (timer < stopDuration)
        {
            if (obj == null || !obj.activeInHierarchy) yield break;
            timer += Time.deltaTime;
            yield return null;
        }

        if (obj == null || !obj.activeInHierarchy) yield break;

        // di chuyển tiếp 
        float currentSpeed = baseSpeed;
        var waitForFixedUpdate = new WaitForFixedUpdate();

        while (obj != null && obj.activeInHierarchy)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime; 
            rb.linearVelocity = moveDir * currentSpeed;
            yield return waitForFixedUpdate; 
        }
    }
}