using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnGameReady;
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle paddle;
    private GameState State;

    protected override void Awake()
    {
        base.Awake();
        Ball.OnBallLaunch += OnBallLaunch;
        LevelManager.OnLevelComplete += OnLevelComplete;
        LevelManager.OnGameWin += OnGameWin;
    }

    void OnDestroy()
    {
        Ball.OnBallLaunch -= OnBallLaunch;
        LevelManager.OnLevelComplete -= OnLevelComplete;
    }

    void Start()
    {
        UpdateGameState(GameState.Ready);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            case GameState.Ready:
                OnGameReady?.Invoke();
                break;

            case GameState.Playing:
                break;

            case GameState.GameOver:
                break;

            case GameState.Win:
                Debug.Log("You Win!");
                break;
        }


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
        UpdateGameState(GameState.Win);
    }
}
