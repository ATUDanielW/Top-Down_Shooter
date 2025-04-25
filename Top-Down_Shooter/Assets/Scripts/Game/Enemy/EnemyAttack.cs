using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
 [SerializeField] private float _damageAmount; // This variable sets how much damage will be dealt to the player on collision

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())// If the object collided with is the player
        {
            var HealthController = collision.gameObject.GetComponent<HealthController>(); // Get the player's HealthController component

            HealthController.TakeDamage(_damageAmount);// Apply damage to the player
        }
    }
}
