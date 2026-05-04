using UnityEngine;
using System.Collections;

public abstract class SpawnStrategy : ScriptableObject
{
    public abstract IEnumerator Spawn(string poolName, int count, Transform spawnOrigin, float spawnInterval);
}