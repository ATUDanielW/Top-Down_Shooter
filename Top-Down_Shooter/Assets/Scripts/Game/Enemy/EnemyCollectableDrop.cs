using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{
    [SerializeField] private float _chanceOfCollectableDrop;// This determines how likely it is that an enemy will drop a collectable 

    private CollectableSpawner _collectableSpawner;// Reference to the component that spawns collectables

    private void Awake()
    {
        _collectableSpawner = FindAnyObjectByType<CollectableSpawner>();// Find and store the first CollectableSpawner in the scene
    }

    public void RandomlyDropCollectable()
    {
        float random = Random.Range(0f, 1f);// Generate a random number between 0 and 1

        if (_chanceOfCollectableDrop >= random)// If the random number is within the chance range, spawn a collectable at this position
        {
            _collectableSpawner.SpawnCollectable(transform.position);
        }
    }


}
