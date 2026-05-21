using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuHandle : MonoBehaviour
{
    public UIFader fader;
    public async void LevelMenu()
    {
        UISfxController.Instance.PlayButtonClick();

        Task fadeTask = fader.FadeOut();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelMenu");
        asyncLoad.allowSceneActivation = false;
        await fadeTask;
        
        while (!asyncLoad.isDone && asyncLoad.progress < 0.9f)
        {
            await Task.Yield();
        }

        asyncLoad.allowSceneActivation = true;
    }

    public async void OptionMenu()
    {
        UISfxController.Instance.PlayButtonClick();

        Task fadeTask = fader.FadeOut();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("OptionScene");
        asyncLoad.allowSceneActivation = false;

        await fadeTask;

        while (!asyncLoad.isDone && asyncLoad.progress < 0.9f)
        {
            await Task.Yield();
        }

        asyncLoad.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        UISfxController.Instance.PlayButtonClick();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // IEnumerator LoadSceneWithFade(string sceneName)
    // {

    //     if (fader != null)
    //     {
    //         fader.FadeOut();
    //         yield return new WaitForSeconds(fader.fadeDuration);
    //     }
    //     SceneManager.LoadScene(sceneName);
    // }
}
