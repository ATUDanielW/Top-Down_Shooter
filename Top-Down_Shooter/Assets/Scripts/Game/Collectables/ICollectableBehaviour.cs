using UnityEngine;

public interface ICollectableBehaviour
{
     // Interface method to define what happens when the item is collected
    void OnCollected(GameObject player);
    
}
