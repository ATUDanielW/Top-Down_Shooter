using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  // Duration of the fade effect when switching scenes
[SerializeField] private float _sceneFadeDuration;

// Reference to the SceneFade component
private SceneFade _sceneFade;

private void Awake()
{
    // Get the SceneFade component from children
    _sceneFade = GetComponentInChildren<SceneFade>();
}

private IEnumerator Start()
{
    // Fade in the scene when the script starts
    yield return _sceneFade.FadeInCoroutine(_sceneFadeDuration);
}

public void LoadScene(string sceneName)
{
    // Start the coroutine to fade out and load a new scene
    StartCoroutine(LoadSceneCoroutine(sceneName));
}

private IEnumerator LoadSceneCoroutine(string sceneName)
{
    // Fade out, then load the specified scene
    yield return _sceneFade.FadeOutCoroutine(_sceneFadeDuration);
    yield return SceneManager.LoadSceneAsync(sceneName);
}
}
