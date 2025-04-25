using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
 [SerializeField] private float _currentHealth;
[SerializeField] private float _maximumHealth;

public float RemainingHealthPercentage => _currentHealth / _maximumHealth;

public bool IsInvincible { get; set; }

public UnityEvent OnDied;
public UnityEvent OnDamaged;
public UnityEvent OnHealthChange;

public void TakeDamage(float damageAmount)
{
    // Exit early if already dead or invincible
    if (_currentHealth == 0 || IsInvincible) return;

    _currentHealth -= damageAmount;
    OnHealthChange.Invoke();

    if (_currentHealth < 0)
        _currentHealth = 0;

    if (_currentHealth == 0)
    {
        // Trigger death event
        OnDied.Invoke();
    }
    else
    {
        // Trigger damaged, event start invincibility, flash effect
        OnDamaged.Invoke();
    }
}

public void AddHealth(float amountToAdd)
{
    // If already at max health, do nothing
    if (_currentHealth == _maximumHealth) return;

    _currentHealth += amountToAdd;
    OnHealthChange.Invoke();

    if (_currentHealth > _maximumHealth)
        _currentHealth = _maximumHealth;
}



}
