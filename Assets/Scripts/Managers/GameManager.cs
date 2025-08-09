using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnGameReady;
    public static event Action OnGameOver;
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle paddle;
    private GameState? State;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Debug.Log($"Avoiding awake of duplicate instance of {typeof(GameManager).Name} on {gameObject.name}");
            Destroy(gameObject);
            return;
        }
        Ball.OnBallLaunch += OnBallLaunch;
        LevelManager.OnLevelComplete += OnLevelComplete;
        LevelManager.OnGameWin += OnGameWin;
        BottomBorder.OnBallLost += OnBallLost;
        UnitySceneManager.OnGameSceneLoad += OnGameSceneLoad;
    }

    void OnDestroy()
    {
        Ball.OnBallLaunch -= OnBallLaunch;
        LevelManager.OnLevelComplete -= OnLevelComplete;
        BottomBorder.OnBallLost -= OnBallLost;
        UnitySceneManager.OnGameSceneLoad -= OnGameSceneLoad;
    }

    private void UpdateGameState(GameState newState)
    {
        Debug.Log($"Cambiando estado del juego de {State} a {newState}");
        if (State == newState) return;

        State = newState;

        switch (State)
        {
            case GameState.Ready:
                OnGameReady?.Invoke();
                break;

            case GameState.Playing:
                break;

            case GameState.GameOver:
                OnGameOver?.Invoke();
                break;
        }
    }

    private void OnGameSceneLoad()
    {
        UpdateGameState(GameState.Ready);
    }

    private void OnBallLaunch()
    {
        UpdateGameState(GameState.Playing);
    }

    private void OnLevelComplete()
    {
        UpdateGameState(GameState.Ready);
    }

    private void OnGameWin()
    {
        UpdateGameState(GameState.GameOver);
    }

    private void OnBallLost()
    {
        UpdateGameState(GameState.GameOver);
    }
}
