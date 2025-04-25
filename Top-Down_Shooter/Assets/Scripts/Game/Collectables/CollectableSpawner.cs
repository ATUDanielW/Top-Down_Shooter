using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
[SerializeField] private List<GameObject> _collectablePrefabs;

public void SpawnCollectable(Vector2 position)
{
    // Randomly select a collectable prefab from the list
    int index = Random.Range(0, _collectablePrefabs.Count);
    var selectedCollectable = _collectablePrefabs[index];

    // Spawn the collectable at the specified position
    Instantiate(selectedCollectable, position, Quaternion.identity);
}
}
