using UnityEngine;

namespace Shooter.Data
{
    public static class PlayerPrefsSave
    {
        public static int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1); // Default to level 1 if not set
        }

        public static void SetCurrentLevel(int level)
        {
            PlayerPrefs.SetInt("CurrentLevel", level);
            PlayerPrefs.Save();
        }
    }
}
