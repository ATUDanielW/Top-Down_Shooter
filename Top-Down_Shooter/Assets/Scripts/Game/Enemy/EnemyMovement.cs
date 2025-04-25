using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    // Enemy movement speed
[SerializeField] private float _speed;

// Speed at which the enemy rotates towards a direction
[SerializeField] private float _rotationSpeed;

// Distance from screen edge to trigger direction change
[SerializeField] private float _screenBorder;

// Radius for obstacle detection
[SerializeField] private float _obstacleCheckCircleRadius;

// Distance to check for obstacles
[SerializeField] private float _obstacleCheckDistance;

// Layer mask for what counts as an obstacle
[SerializeField] private LayerMask _obstacleLayerMask;

// Rigidbody for physics movement
private Rigidbody2D _rigidbody;

// Reference to player awareness system
private PlayerAwarenessController _playerAwarenessController;

// Current direction the enemy is moving
private Vector2 _targetDirection;

// Time left before the enemy changes direction randomly
private float _changeDirectionCooldown;

// Reference to the main camera
private Camera _camera;

// Stores obstacle collision results
private RaycastHit2D[] _obstacleCollisions;

// Cooldown to prevent constant obstacle avoidance
private float _obstacleAvoidanceCooldown;

// Direction to move in to avoid an obstacle
private Vector2 _obstacleAvoidanceTargetDirection;

private void Awake()
{
    // Get required components and initialize values
    _rigidbody = GetComponent<Rigidbody2D>();
    _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    _targetDirection = transform.up; // Start moving in the direction the enemy is facing
    _camera = Camera.main;
    _obstacleCollisions = new RaycastHit2D[10];
}

void FixedUpdate()
{
    // Update the enemy's direction and movement every physics frame
    UpdateTargetDirection();
    RotateTowardsTarget();
    SetVelocity();
}

private void UpdateTargetDirection()
{
    // Change direction randomly
    HandleRandomDirectionChange();

    // Track player if nearby
    HandlePlayerTargeting();

    // Keep enemy on screen
    HandleEnemyOffScreen();

    // Avoid obstacles
    HandleObstacles();
}

private void HandleRandomDirectionChange()
{
    // Countdown timer before changing direction
    _changeDirectionCooldown -= Time.deltaTime;
    if (_changeDirectionCooldown <= 0)
    {
        // Pick a random new direction by rotating current direction
        float angleChange = UnityEngine.Random.Range(-90f, 90f);
        Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
        _targetDirection = rotation * _targetDirection;

        // Reset cooldown to a random time between 1-5 seconds
        _changeDirectionCooldown = UnityEngine.Random.Range(1f, 5f);
    }
}

private void HandlePlayerTargeting()
{
    // If enemy can see the player, move towards them
    if (_playerAwarenessController.AwareOfPlayer)
    {
        _targetDirection = _playerAwarenessController.DirectionToPlayer;
    }
}

private void HandleEnemyOffScreen()
{
    // Convert world position to screen position
    Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

    // Bounce back if too close to horizontal edges
    if ((screenPosition.x < _screenBorder && _targetDirection.x < 0) || 
        (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
    {
        _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
    }

    // Bounce back if too close to vertical edges
    if ((screenPosition.y < _screenBorder && _targetDirection.y < 0) || 
        (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
    {
        _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
    }
}

private void HandleObstacles()
{
    // Countdown for avoiding new obstacles
    _obstacleAvoidanceCooldown -= Time.deltaTime;

    // Filter to detect only obstacles in the given layer
    var contactFilter = new ContactFilter2D();
    contactFilter.SetLayerMask(_obstacleLayerMask);

    // Cast a circle to detect obstacles ahead
    int numberOfCollisions = Physics2D.CircleCast(
        transform.position,
        _obstacleCheckCircleRadius,
        transform.up,
        contactFilter,
        _obstacleCollisions,
        _obstacleCheckDistance
    );

    // Check each detected obstacle
    for (int index = 0; index < numberOfCollisions; index++)
    {
        var obstacleCollision = _obstacleCollisions[index];

        // Skip if we hit ourselves
        if (obstacleCollision.collider.gameObject == gameObject)
        {
            continue;
        }

        // Only react if cooldown is finished
        if (_obstacleAvoidanceCooldown <= 0)
        {
            _obstacleAvoidanceTargetDirection = obstacleCollision.normal;
            _obstacleAvoidanceCooldown = 0.5f; // Short delay before changing again
        }

        // Rotate away from the obstacle
        var targetRotation = Quaternion.LookRotation(transform.forward, _obstacleAvoidanceTargetDirection);
        var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _targetDirection = rotation * Vector2.up;
        break; // Stop after handling the first obstacle
    }
}

private void RotateTowardsTarget()
{
    // Rotate smoothly toward the target direction
    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

    _rigidbody.SetRotation(rotation);
}

private void SetVelocity()
{
    // Move the enemy forward in the direction it is facing
    _rigidbody.linearVelocity = transform.up * _speed;
}
}

