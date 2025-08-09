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
        if (Instance == this)
        {
            ReplayButton.OnReplayButtonPressed += OnReplayButtonPressed;
            PlayButton.OnPlayButtonPressed += OnPlayButtonPressed;
            GameManager.OnGameOver += OnGameOver;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

    }

    void OnDestroy()
    {
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
            OnGameSceneLoad?.Invoke();
        }

        currentScene = scene;
    }
}