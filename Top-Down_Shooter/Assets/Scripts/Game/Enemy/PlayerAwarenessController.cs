using UnityEngine;
public class PlayerAwarenessController : MonoBehaviour
{
  // Whether the enemy is currently aware of the player
public bool AwareOfPlayer { get; private set; }

// The direction from enemy to player
public Vector2 DirectionToPlayer { get; private set; }

// Maximum distance at which enemy becomes aware of player
[SerializeField]
private float _playerAwarenessDistance;

// Reference to the player's transform
private Transform _player;

private void Awake()
{
    // Finds the player's transform in the scene
    _player = FindAnyObjectByType<PlayerMovement>().transform;
}

void Update()
{
    // Calculate vector from enemy to player
    Vector2 enemyToPlayerVector = _player.position - transform.position;

    // Normalize it to get direction
    DirectionToPlayer = enemyToPlayerVector.normalized;

    // Check if player is within awareness distance
    if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
    {
        AwareOfPlayer = true;
    }
    else
    {
        AwareOfPlayer = false;
    }
}

}