using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class ScoreUI : MonoBehaviour
{
   private TMP_Text _scoreText;

private void Awake()
{
    // Get the TextMeshPro text component
    _scoreText = GetComponent<TMP_Text>();
}

public void UpdateScore(ScoreController scoreController)
{
    // Update the displayed score text
    _scoreText.text = $"Score: {scoreController.Score}";
}
}
