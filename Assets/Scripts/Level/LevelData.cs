using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level Design/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public float speed;
    // cách spawn các wave
    public WaveData[] waves;
}