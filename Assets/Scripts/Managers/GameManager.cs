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
    }

    void OnDestroy()
    {
        Ball.OnBallLaunch -= OnBallLaunch;
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
                break;
        }


    }
    
    private void OnBallLaunch()
    {
        UpdateGameState(GameState.Playing);
    }
}
