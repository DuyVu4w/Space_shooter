using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using Shooter.Data;

public class LevelSelectButton : MonoBehaviour
{
    public int levelIndex;
    public LevelData levelData;
    public LevelMenuHandler menuHandler;

    public GameObject lockIcon, levelText;

    private Button button;
    public void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogWarning("No Button component found on " + gameObject.name);
        }
    }

    public async void OnButtonClicked()
    {
        if (levelData != null && menuHandler != null)
        {
            await menuHandler.LoadLevel(levelData);
        }
        else
        {
            Debug.LogWarning("LevelData or MenuHandler is not set for " + gameObject.name);
        }
    }

    public void SetLocked(bool isLocked)
    {
        if (isLocked)
        {
            if (lockIcon != null)
                lockIcon.SetActive(isLocked);
            if (levelText != null)
                levelText.SetActive(!isLocked);
            if (button != null)
                button.interactable = !isLocked;
        }
        else
        {
            levelText.GetComponent<TextMeshProUGUI>().text = levelIndex.ToString();
        }
    }
}
