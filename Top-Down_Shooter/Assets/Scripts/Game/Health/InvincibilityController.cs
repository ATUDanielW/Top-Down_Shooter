using System.Collections;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
private HealthController _healthController;
private SpriteFlash _spriteFlash;

private void Awake()
{
    // Cache required components
    _healthController = GetComponent<HealthController>();
    _spriteFlash = GetComponent<SpriteFlash>();
}

public void StartInvincibility(float invincibilityDuration, Color flashColor, int numberOfFlashes)
{
    // Begin coroutine to trigger invincibility
    StartCoroutine(InvincibilityCoroutine(invincibilityDuration, flashColor, numberOfFlashes));
}

private IEnumerator InvincibilityCoroutine(float invincibilityDuration, Color flashColor, int numberOfFlashes)
{
    _healthController.IsInvincible = true;
    // Trigger flash visual effect
    yield return _spriteFlash.FlashCoroutine(invincibilityDuration, flashColor, numberOfFlashes);
    _healthController.IsInvincible = false;
}

}
