using UnityEngine;

public class Bullet : MonoBehaviour
{
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
}
