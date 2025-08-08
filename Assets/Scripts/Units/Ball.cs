using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    public static event Action OnBallLaunch;
    [SerializeField] private Paddle paddle;
    [SerializeField] private float initialVelocityX;
    [SerializeField] private float initialVelocityY;
    [SerializeField] private float maxBounceAngleSpeedX;
    private Vector3 paddleOffset;
    private Rigidbody2D ballRigidbody;
    private bool isBallLaunched;

    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        GameManager.OnGameReady += OnGameReady;
    }

    void OnDestroy()
    {
        GameManager.OnGameReady -= OnGameReady;
    }

    void Update()
    {
        HandleBallIsNotLaunched();
    } 

    private void OnGameReady()
    {
        PrepareForLaunch();
        AttachToPaddle();
        isBallLaunched = false;
    }

    private void PrepareForLaunch()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        ballRigidbody.linearVelocity = Vector2.zero;
    }

    private void AttachToPaddle()
    {
        paddleOffset = transform.position - paddle.transform.position;
        PositionOnPaddle();
    }

    private void HandleBallIsNotLaunched()
    {
        if (!isBallLaunched)
        {
            PositionOnPaddle();
            LaunchBallIfMouseIsPressed();
        }
    }

    private void PositionOnPaddle()
    {
        transform.position = paddle.transform.position + paddleOffset;
    }

    private void LaunchBallIfMouseIsPressed()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Launch();
            isBallLaunched = true;
            OnBallLaunch?.Invoke();
        }

    }

    private void Launch()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.linearVelocity = new Vector2(initialVelocityX, initialVelocityY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnPaddleCollisionEnter2D(collision);
    }

    private void OnPaddleCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
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

}
