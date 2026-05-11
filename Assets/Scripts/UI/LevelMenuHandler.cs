using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Shooter.Data;


public class LevelMenuHandler : MonoBehaviour
{
    public UIFader fader;
    public DataManager dataManager;
    private int currentLevelIndex = 1;

    private void Start()
    {
        currentLevelIndex = PlayerPrefsSave.GetCurrentLevel();
        foreach (GameObject btn in GameObject.FindGameObjectsWithTag("LevelSelectButton"))
        {
            LevelSelectButton lvSelect = btn.GetComponent<LevelSelectButton>();
            lvSelect.SetLocked(lvSelect.levelIndex > currentLevelIndex);
        }

    }


    public async Task LoadLevel(LevelData levelData)
    {
        Task fadeTask = fader.FadeOut();
        dataManager.selectedLevel = levelData;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = false;
        await fadeTask;

        while (!asyncLoad.isDone && asyncLoad.progress < 0.9f)
        {
            await Task.Yield();
        }

        asyncLoad.allowSceneActivation = true;
    }
}