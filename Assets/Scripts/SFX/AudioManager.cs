using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer mixer;
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSfxVolume(float volume)
    {
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public float GetMusicVolume()
    {
        float value;
        if (mixer.GetFloat("Music", out value))
        {
            return Mathf.Pow(10, value / 20);
        }
        return 1f;
    }

    public float GetSfxVolume()
    {
        float value;
        if (mixer.GetFloat("SFX", out value))
        {
            return Mathf.Pow(10, value / 20);
        }
        return 1f;
    }
}
