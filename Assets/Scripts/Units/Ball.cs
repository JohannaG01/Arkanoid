using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    public static event Action OnBallLaunch;
    [SerializeField] private float initialVelocityX;
    [SerializeField] private float initialVelocityY;
    [SerializeField] private float maxBounceAngleSpeedX;
    private Rigidbody2D ballRigidbody;
    private bool isBallLaunched;
    private Vector3 initialPosition;

    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        GameManager.OnGameReady += OnGameReady;
        Paddle.OnPaddleMovedX += OnPaddleMovedX;
    }

    void OnDestroy()
    {
        GameManager.OnGameReady -= OnGameReady;
        Paddle.OnPaddleMovedX -= OnPaddleMovedX;
    }

    void FixedUpdate()
    {
        ballRigidbody.linearVelocity = ballRigidbody.linearVelocity.normalized * initialVelocityY;
    }

    void Update()
    {
        HandleBallBeforeLaunch();
    }

    private void OnGameReady()
    {
        ResetBallPosition();
    }

    private void OnPaddleMovedX(float paddleX)
    {
        UpdatePositionToPaddleX(paddleX);
    }

    private void HandleBallBeforeLaunch()
    {
        if (!isBallLaunched)
        {
            LaunchBallIfMouseIsPressed();
        }
    }

    private void ResetBallPosition()
    {
        isBallLaunched = false;
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        ballRigidbody.linearVelocity = Vector2.zero;
        transform.position = initialPosition;
    }

    private void UpdatePositionToPaddleX(float paddleX)
    {
        if (!isBallLaunched)
        {
            transform.position = new Vector3(paddleX, transform.position.y, transform.position.z);
        }
    }

    private void LaunchBallIfMouseIsPressed()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
            ballRigidbody.linearVelocity = new Vector2(initialVelocityX, initialVelocityY);
            isBallLaunched = true;

            OnBallLaunch?.Invoke();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            BounceOnPaddle(collision);
        }
    }

    private void BounceOnPaddle(Collision2D collision)
    {
        Vector3 paddlePosition = collision.collider.bounds.center;
        float paddleWidth = collision.collider.bounds.size.x / 2;

        float hitPoint = collision.contacts[0].point.x;
        float difference = hitPoint - paddlePosition.x;
        float normalizedDifference = difference / paddleWidth;

        Vector2 newVelocity = new Vector2(normalizedDifference * maxBounceAngleSpeedX, Mathf.Abs(ballRigidbody.linearVelocity.y));
        ballRigidbody.linearVelocity = newVelocity;
    }

}
