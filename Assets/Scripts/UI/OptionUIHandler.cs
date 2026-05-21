using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class OptionUIHandler : MonoBehaviour
{
    public Button backButton;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public UIFader faderImage;

    public void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        musicVolumeSlider.value = AudioManager.Instance.GetMusicVolume();
        sfxVolumeSlider.value = AudioManager.Instance.GetSfxVolume();
        
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    public async void OnBackButtonClicked()
    {
        UISfxController.Instance.PlayButtonClick();
        Task fadeTask = faderImage.FadeOut();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        asyncLoad.allowSceneActivation = false;

        await fadeTask;

        while (!asyncLoad.isDone && asyncLoad.progress < 0.9f)
        {
            await Task.Yield();
        }

        asyncLoad.allowSceneActivation = true;
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSfxVolumeChanged(float value)
    {
        AudioManager.Instance.SetSfxVolume(value);
    }
}