using UnityEngine;
using UnityEngine.SceneManagement;
using Shooter.Data;

public class LevelMenuHandler : MonoBehaviour
{
    public UIFader fader;

    void Start()
    {
        int currentLevel = PlayerPrefsSave.GetCurrentLevel();
        string sceneName = "Level" + currentLevel;
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Please check the scene name and ensure it is added to the build settings.");
        }
    }
}