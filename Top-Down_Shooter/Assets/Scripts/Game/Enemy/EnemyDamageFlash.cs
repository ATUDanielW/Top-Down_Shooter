using UnityEngine;

public class EnemyDamageFlash : MonoBehaviour
{
    [SerializeField] private float _flashDuration;// Duration of the flash effect
    [SerializeField] private Color _flashColor;// Color of the flash effect
    [SerializeField] private int _numberOfFlashes;// How many times to flash

    private SpriteFlash _spriteFlash;// Reference to the SpriteFlash component

    private void Awake()
    {
        _spriteFlash = GetComponent<SpriteFlash>();// Get the SpriteFlash component attached to this object
    }

    public void StartFlash()
    {
        _spriteFlash.StartFlash(_flashDuration, _flashColor, _numberOfFlashes);// Start the flashing effect with specified parameters
    }

}
