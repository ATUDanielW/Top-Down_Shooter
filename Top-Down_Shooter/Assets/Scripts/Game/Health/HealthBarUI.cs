using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
[SerializeField] private UnityEngine.UI.Image _healthBarForegroundImage;

public void UpdateHealthBar(HealthController healthController)
{
    // Set health bar fill amount based on current health percentage
    _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage;
}

}
