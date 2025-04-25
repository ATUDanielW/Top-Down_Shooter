using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;

public class NewMonoBehaviourScript : MonoBehaviour
{
 // Prefab of the enemy to spawn
[SerializeField] private GameObject _enemyPrefab;

// Minimum time between spawns
[SerializeField] private float _minimumSpawnTime;

// Maximum time between spawns
[SerializeField] private float _maximumSpawnTime;

// Time left until next spawn
private float _timeUnitilSpawn;

void Awake()
{
    // Sets the first spawn timer
    SetTimeUntilSpawn();
}

void Update()
{
    // Countdown until next spawn
    _timeUnitilSpawn -= Time.deltaTime;

    // Spawn enemy when timer reaches zero
    if(_timeUnitilSpawn <= 0)
    {
        Instantiate(_enemyPrefab, transform.position, quaternion.identity);
        SetTimeUntilSpawn();
    }
}

private void SetTimeUntilSpawn()
{
    // Randomizes time between next enemy spawns
    _timeUnitilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
}
}
