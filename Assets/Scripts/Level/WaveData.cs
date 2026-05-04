using UnityEngine;

[System.Serializable]
public class WaveData
{
    public string poolName;
    public int count;
    public float SpawnInterval;
    
    [Header("Spawn Behaviour")]
    public SpawnStrategy spawnStrategy;
}