using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LineSpawn", menuName = "Level Design/Spawn Strategies/Line")]
public class LineSpawnStrategy : SpawnStrategy
{
    public float lineSpacing = 2f;

    public override IEnumerator Spawn(string poolName, int count, Transform spawnOrigin, float spawnInterval)
    {
        float speed = GameController.Instance.speed; 
        
        // Đợi 1 chút trước khi spawn đợt đầu tiên
        yield return new WaitForSeconds(spawnInterval);
        
        if (GameController.Instance.isGameOver) yield break;

        float startX = -((count - 1) * lineSpacing) / 2f;

        for (int i = 0; i < count; i++)
        {
            GameObject temp = null;
            if (poolName == "Rock") {
                temp = ObjectPooler.Instance.SpawnFromPool("Rock");
            }

            if (temp != null) {
                float posX = startX + (i * lineSpacing);
                temp.transform.position = new Vector3(posX, spawnOrigin.position.y, spawnOrigin.position.z);
                Rigidbody rb = temp.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero; 
                    rb.angularVelocity = Vector3.zero; 
                    rb.linearVelocity = Vector3.back * speed;
                }

                temp.SetActive(true);
            }
        }

    }
}