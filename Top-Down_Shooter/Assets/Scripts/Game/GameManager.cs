using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  // Time to wait before transitioning to main menu after player death
[SerializeField] private float _timeToWaitBeforeExit;

// Reference to the scene controller
[SerializeField] private SceneController _sceneController;

public void OnPlayerDied()
{
    // Waits for delay before ending the game
    Invoke(nameof(EndGame), _timeToWaitBeforeExit);
}

private void EndGame()
{
    // Loads the Main Menu scene
    _sceneController.LoadScene("Main Menu");
}
}
