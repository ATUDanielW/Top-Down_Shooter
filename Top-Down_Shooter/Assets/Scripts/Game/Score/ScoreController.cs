using UnityEngine;
using UnityEngine.Events;


public class ScoreController : MonoBehaviour
{
  public UnityEvent OnScoreChanged;
public int Score { get; private set; }

public void AddScore(int amount)
{
    // Increase score and trigger update
    Score += amount;
    OnScoreChanged.Invoke();
}
}
