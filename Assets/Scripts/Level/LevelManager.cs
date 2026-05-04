using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public LevelData currentLevel;
    public Transform spawnOrigin;
    private int currentWave = 0;

    void Start()
    {
        if (currentLevel != null && currentLevel.waves.Length > 0)
        {
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {
        // Vòng lặp sẽ dừng nếu hết wave hoặc game over
        while (currentWave < currentLevel.waves.Length && !GameController.Instance.isGameOver)
        {
            WaveData wave = currentLevel.waves[currentWave];
            SpawnStrategy strategy = wave.spawnStrategy;
            
            if (strategy != null)
            {
                // Đợi cho Strategy thực hiện xong toàn bộ việc spawn của Wave này
                yield return StartCoroutine(strategy.Spawn(wave.poolName, wave.count, spawnOrigin, wave.SpawnInterval));
            }
            else
            {
                Debug.LogWarning("Wave " + currentWave + " chưa được gán SpawnStrategy!");
            }

            // Sau khi spawn xong đợt này, nghỉ 2 giây rồi mới sang đợt tiếp theo
            yield return new WaitForSeconds(2f);

            currentWave++;
        }
        
        if (!GameController.Instance.isGameOver) 
        {
            Debug.Log("Hoàn thành tất cả các wave của Level: " + currentLevel.levelName);
            // Có thể gọi GameController.Instance.LevelComplete() tại đây nếu bạn có hàm đó
        }
    }
}