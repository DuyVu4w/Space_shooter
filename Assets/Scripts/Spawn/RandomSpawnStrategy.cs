using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RandomSpawn", menuName = "Level Design/Spawn Strategies/Random")]
public class RandomSpawnStrategy : SpawnStrategy
{
    public float _rangeX = 6.5f;

    public override IEnumerator Spawn(string poolName, int count, Transform spawnOrigin, float spawnInterval)
    {
        // Lấy speed từ GameController
        float speed = GameController.Instance.speed; 
        
        for (int i = 0; i < count; i++)
        {
            // Dừng việc sinh quái nếu game đã over
            if (GameController.Instance.isGameOver)
            {
                yield break;
            }

            yield return new WaitForSeconds(spawnInterval);
            
            GameObject temp = null;
            if (poolName == "Rock") {
                temp = RockPooling.Instance.GetRock();
            }
            else if (poolName == "Meteor") {
                // temp = MeteorPooling.Instance.GetMeteor();
            }

            if (temp != null) {
                float randomX = Random.Range(-_rangeX, _rangeX);
                temp.transform.position = new Vector3(randomX, spawnOrigin.position.y, spawnOrigin.position.z);
                temp.SetActive(true);

                // Xử lý vật lý (Physics handling)
                Rigidbody rb = temp.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero; // Reset vận tốc (Reset velocity)
                    rb.angularVelocity = Vector3.zero; // Reset vận tốc góc (Reset angular velocity)
                    rb.linearVelocity = Vector3.back * speed;
                }
            }
        }
    }
}