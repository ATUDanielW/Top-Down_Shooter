using UnityEngine;

public class Bullet : MonoBehaviour
{
private Camera _camera;

void Update()
{
    // Check if bullet is off-screen and destroy if it is
    DestoryWhenOffScreen();
}

void Awake()
{
    _camera = Camera.main;
}

void OnTriggerEnter2D(Collider2D collision)
{
    // Check if bullet hit an enemy
    if (collision.GetComponent<EnemyMovement>())
    {
        // Deal damage to the enemy
        HealthController healthController = collision.GetComponent<HealthController>();
        healthController.TakeDamage(10);

        // Destroy bullet
        Destroy(gameObject);
    }
}

private void DestoryWhenOffScreen()
{
    // Convert bullet's position to screen space
    Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
    
      // Destroy bullet if it moves out of visible screen
        if (screenPosition.x < 0 ||
            screenPosition.x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);
        }
}
}
