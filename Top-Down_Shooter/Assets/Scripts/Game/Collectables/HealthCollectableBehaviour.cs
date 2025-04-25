using UnityEngine;

public class HealthCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
 [SerializeField] private float _healthAmount;

public void OnCollected(GameObject player)
{
    // When collected, add health to the player
    player.GetComponent<HealthController>().AddHealth(_healthAmount);
}
}
