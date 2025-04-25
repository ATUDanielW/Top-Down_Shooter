using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
 // Image used for fading in/out scene transitions
private Image _sceneFadeImage;

private void Awake()
{
    // Get the Image component on this object
    _sceneFadeImage = GetComponent<Image>();
}

public IEnumerator FadeInCoroutine(float duration)
{
    // Fade from opaque to transparent
    Color startColor = new Color(_sceneFadeImage.color.r, _sceneFadeImage.color.g, _sceneFadeImage.color.b, 1);
    Color targetColor = new Color(_sceneFadeImage.color.r, _sceneFadeImage.color.g, _sceneFadeImage.color.b, 0);

    yield return FadeCoroutine(startColor, targetColor, duration);

    // Disable the object after fade-in
    gameObject.SetActive(false);
}

public IEnumerator FadeOutCoroutine(float duration)
{
    // Fade from transparent to opaque
    Color startColor = new Color(_sceneFadeImage.color.r, _sceneFadeImage.color.g, _sceneFadeImage.color.b, 0);
    Color targetColor = new Color(_sceneFadeImage.color.r, _sceneFadeImage.color.g, _sceneFadeImage.color.b, 1);

    // Ensure the fade image is visible
    gameObject.SetActive(true);
    yield return FadeCoroutine(startColor, targetColor, duration);
}

private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
{
    float elapsedTime = 0;
    float elapsedPercentage = 0;

    // Gradually interpolate between start and target colors
    while (elapsedPercentage < 1)
    {
        elapsedPercentage = elapsedTime / duration;
        _sceneFadeImage.color = Color.Lerp(startColor, targetColor, elapsedPercentage);

        yield return null;
        elapsedTime += Time.deltaTime;
    }
}

}
