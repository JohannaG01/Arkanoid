using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UnitySceneManager : Singleton<UnitySceneManager>
{
    public static event Action OnGameSceneLoad;
    private Scene currentScene;
    protected override void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Debug.Log($"Avoiding awake of duplicate instance of {typeof(UnitySceneManager).Name} on {gameObject.name}");
            Destroy(gameObject);
            return;
        }
        ReplayButton.OnReplayButtonPressed += OnReplayButtonPressed;
        PlayButton.OnPlayButtonPressed += OnPlayButtonPressed;
        GameManager.OnGameOver += OnGameOver;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        Debug.Log("SceneManager OnDestroy, desuscribiendo de sceneLoaded");
        ReplayButton.OnReplayButtonPressed -= OnReplayButtonPressed;
        PlayButton.OnPlayButtonPressed -= OnPlayButtonPressed;
        GameManager.OnGameOver -= OnGameOver;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnPlayButtonPressed()
    {
        LoadGameScene();
    }

    private void OnReplayButtonPressed()
    {
        LoadMenuScene();
    }

    private void OnGameOver()
    {
        LoadGameOverScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene" && scene != currentScene)
        {
            Debug.Log($"Escena de juego cargada, invocando evento. {Instance.GetHashCode()}");
            OnGameSceneLoad?.Invoke();
        }

        currentScene = scene;
    }
}