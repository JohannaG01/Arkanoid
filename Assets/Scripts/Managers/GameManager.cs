using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{

    public GameState CurrentState { get; private set; }
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle paddle;

    void Start()
    {
        CurrentState = GameState.Ready;
        ball.PrepareForLaunch();
        ball.AttachToPaddle(paddle.transform);
    }

    void Update()
    {
        switch (CurrentState)
        {
            case GameState.Ready:
                StartGameIfMouseIsPressed();
                break;

            case GameState.Playing:
                break;

            case GameState.GameOver:
                break;

            case GameState.Win:
                break;
            //TODO create new state for CompletedLevel
        }
    }

    public void StartGameIfMouseIsPressed()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CurrentState = GameState.Playing;
            ball.Launch();
        }

    }
}
