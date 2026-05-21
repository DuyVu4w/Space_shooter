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

        Debug.Log("Current LV: " + currentLevelIndex);
    }


    public async Task LoadLevel(LevelData levelData, int levelIndex)
    {
        UISfxController.Instance.PlayButtonClick();
        Task fadeTask = fader.FadeOut();
        dataManager.selectedLevel = levelData;
        dataManager.selectedLevelIndex = levelIndex;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = false;
        await fadeTask;

        while (!asyncLoad.isDone && asyncLoad.progress < 0.9f)
        {
            await Task.Yield();
        }

        asyncLoad.allowSceneActivation = true;
    }

    public async void LoadMainMenu()
    {
        UISfxController.Instance.PlayButtonClick();
        Task fadeTask = fader.FadeOut();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        asyncLoad.allowSceneActivation = false;

        await fadeTask;
        while (!asyncLoad.isDone && asyncLoad.progress < 0.9f)
        {
            await Task.Yield();
        }
        asyncLoad.allowSceneActivation = true;
    }
}