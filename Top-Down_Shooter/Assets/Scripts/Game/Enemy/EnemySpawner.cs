using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;

public class NewMonoBehaviourScript : MonoBehaviour
{
  [SerializeField] private GameObject _enemyPrefab;

  [SerializeField] private float _minimumSpawnTime;

  [SerializeField] private float _maximumSpawnTime;

  private float _timeUnitilSpawn;


    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        //This will reduce the time until spawn by the amount of time has passed this frame once reaches zero we want to spawn enemy
        _timeUnitilSpawn -= Time.deltaTime;

        if(_timeUnitilSpawn <= 0)
        {
            Instantiate(_enemyPrefab, transform.position, quaternion.identity);
            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
  {
    _timeUnitilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
  }
}
