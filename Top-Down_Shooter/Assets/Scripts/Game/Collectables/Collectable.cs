using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour _collectableBehaviour;

private void Awake()
{
    // Retrieve the collectable behavior implemented on this object (e.g., health or other powerups)
    _collectableBehaviour = GetComponent<ICollectableBehaviour>();
}

void OnTriggerEnter2D(Collider2D collision)
{
    // Check if the player collided with the collectable
    var player = collision.GetComponent<PlayerMovement>();

    if (player != null)
    {
        // Trigger collectable effect on player
        _collectableBehaviour.OnCollected(player.gameObject);
        // Destroy collectable after being picked up
        Destroy(gameObject);
    }
}
}
