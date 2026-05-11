using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class UIFader : MonoBehaviour
{
    private Image fadeImage;
    public float fadeDuration = 1f;

    void Awake()
    {
        gameObject.SetActive(true);
        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
    }
    async void Start()
    {
        await FadeIn();
    }

    public async Task FadeIn()
    {
        fadeImage.raycastTarget = false;

        await fadeImage.DOFade(0f, fadeDuration)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject)
            .AsyncWaitForCompletion();
    }

    public async Task FadeOut()
    {
        fadeImage.raycastTarget = true;

        await fadeImage.DOFade(1f, fadeDuration)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject)
            .AsyncWaitForCompletion();
    }
}
