using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
  // Score value given when this enemy is killed
[SerializeField]
private int _killScore;

// Reference to the score manager
private ScoreController _scoreController;

private void Awake()
{
    // Finds the ScoreController in the scene
    _scoreController = FindAnyObjectByType<ScoreController>();
}

public void AllocateScore()
{
    // Adds the kill score to the player's score
    _scoreController.AddScore(_killScore);
}
}
