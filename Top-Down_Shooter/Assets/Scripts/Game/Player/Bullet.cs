using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Camera _camera;
    void Update()
    {
        DestoryWhenOffScreen();
    }

    void Awake()
    {
        _camera = Camera.main;
    }
    void OnTriggerEnter2D(Collider2D collision) //whenever collider collides with anything
    {
        //check if bullet collided with enemy, check by cheking enemy movmenet commponent
        if(collision.GetComponent<EnemyMovement>())
        {
            //if collided destroy components (Enemy and Bullet)
            Destroy (collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void DestoryWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        //if these are true bullet will be destoyed
        if (screenPosition.x <0 ||
            screenPosition .x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight)
            {
                Destroy(gameObject);
            }
    }
}
