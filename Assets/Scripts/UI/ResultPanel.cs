using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;
    public Button restartButton;
    public Button levelMenuButton;

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

    public void OnRestartButtonClicked()
    {
        GameController.Instance.Restart();
    }

    public void OnLevelMenuButtonClicked()
    {
        // Load level menu scene
        SceneManager.LoadScene("LevelMenu");
    }
}
