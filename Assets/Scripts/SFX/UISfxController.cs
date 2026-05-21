using UnityEngine;
using System.Collections;

public class UISfxController : Singleton<UISfxController>
{
    public AudioSource audioSource;
    public AudioClip btnClick;
    public AudioClip glickSfx;
    public AudioClip levelCompleteSfx;
    public AudioClip gameOverSfx;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(btnClick);
    }

    public void PlayGlick()
    {
        audioSource.PlayOneShot(glickSfx);
    }

    public void PlayLevelComplete()
    {
        audioSource.PlayOneShot(levelCompleteSfx);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSfx);
    }
}