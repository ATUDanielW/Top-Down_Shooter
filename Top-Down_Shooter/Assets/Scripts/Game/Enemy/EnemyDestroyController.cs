using UnityEngine;

public class EnemyDestroyController : MonoBehaviour
{
    public void DestroyEnemy(float delay) // Destroys the enemy game object after a delay
    {
        Destroy(gameObject, delay);
    }
}
