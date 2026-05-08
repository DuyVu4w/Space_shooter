using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIFader : MonoBehaviour
{
    private Image fadeImage;
    public float fadeDuration = 1f;
    void Start()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        FadeIn();
    }

    public void FadeIn()
    {
        fadeImage.DOFade(0f, fadeDuration).SetEase(Ease.InQuad);
        fadeImage.raycastTarget = false; // Disable raycast target to allow interactions during fade-in
    }

    public void FadeOut()
    {
        fadeImage.raycastTarget = true; // Enable raycast target to block interactions during fade-out
        fadeImage.DOFade(1f, fadeDuration).SetEase(Ease.InQuad);
    }
}
