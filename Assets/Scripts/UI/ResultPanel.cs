using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class ResultPanel : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;
    public Button restartButton;
    public Button levelMenuButton;
    public UIFader fader;
    public void ShowResult(int score, bool isWin)
    {
        resultText.text = isWin ? "Mission Complete!" : "Mission Failed!";
        SetScore(score);
    }

    public void SetScore(int score)
    {
        int currentScore = 0;
        DOTween.To(() => currentScore, x => currentScore = x, score, .5f)
        .OnUpdate(() => scoreText.text = currentScore.ToString())
        .SetEase(Ease.OutCubic);
    }

    public async Task OnRestartButtonClicked()
    {
        Task fadeTask = fader.FadeOut();
        await fadeTask;
        GameController.Instance.Restart();
    }

    public async Task OnLevelMenuButtonClicked()
    {
        // Load level menu scene
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
}
