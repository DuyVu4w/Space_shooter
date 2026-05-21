using UnityEngine;

public class GameSceneBtn : MonoBehaviour
{
    public ResultPanel resultPanel;

    public async void OnBackToMenuClicked()
    {
        await resultPanel.OnLevelMenuButtonClicked();    
    }
    
    public async void OnRestartClicked()
    {
        await resultPanel.OnRestartButtonClicked();
    }

}
