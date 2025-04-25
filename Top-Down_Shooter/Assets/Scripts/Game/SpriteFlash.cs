using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
 // Reference to the sprite renderer used for flashing
private SpriteRenderer _spriteRenderer;

private void Awake()
{
    // Gets the sprite renderer from child object
    _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
}

public void StartFlash(float flashDuration, Color flashColor, int numberOfFlashes)
{
    // Starts the flash coroutine
    StartCoroutine(FlashCoroutine(flashDuration, flashColor, numberOfFlashes));
}

public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numberOfFlashes)
{
    // Store the original color of the sprite
    Color startColor = _spriteRenderer.color;
    float elapsedFlashTime = 0;
    float elapsedFlashPercentage = 0;

    // Loop until the flash duration ends
    while (elapsedFlashTime < flashDuration)
    {
        elapsedFlashTime += Time.deltaTime;
        elapsedFlashPercentage = elapsedFlashTime / flashDuration;

        if (elapsedFlashPercentage > 1)
        {
            elapsedFlashPercentage = 1;
        }

        // Creates a flashing effect using PingPong
        float pingPongPercentage = Mathf.PingPong(elapsedFlashPercentage * 2 * numberOfFlashes, 1);
        _spriteRenderer.color = Color.Lerp(startColor, flashColor, pingPongPercentage);

        yield return null;
    }
}

}
