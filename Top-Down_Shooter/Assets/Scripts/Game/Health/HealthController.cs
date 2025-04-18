using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
   [SerializeField]private float _currentHealth;
   [SerializeField]private float _maximumHealth;

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvincible {get; set;}

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChange;

    public void TakeDamage(float damageAmount)
    {
        if(_currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
             return;
        }

        _currentHealth -=damageAmount;

        OnHealthChange.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        //when this event happens player will die
        if(_currentHealth == 0)
        {
            OnDied.Invoke();
        }
        //adding event so that after player is damaged it will become invicible for a bit so that he wont loose all the health per frame
        else
        {
            OnDamaged.Invoke();
        }

    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;

        OnHealthChange.Invoke();
        
        if(_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }







}
