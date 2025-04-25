using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Reference to the scene controller
[SerializeField] private SceneController _sceneController;

public void Play()
{
    // Load the game scene
    _sceneController.LoadScene("Game");
}

public void Exit()
{
    // Quit the application
    Application.Quit();
}
}
