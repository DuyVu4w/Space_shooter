using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuHandle : MonoBehaviour
{
    public UIFader fader;
    public void LevelMenu()
    {
        StartCoroutine(LoadSceneWithFade("GameMap"));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator LoadSceneWithFade(string sceneName)
    {

        if (fader != null)
        {
            fader.FadeOut();
            yield return new WaitForSeconds(fader.fadeDuration);
        }
        SceneManager.LoadScene(sceneName);
    }
}
