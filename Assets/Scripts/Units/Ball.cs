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

    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody2D>(); 

        GameManager.OnGameReady += OnGameReady;
        Paddle.OnPaddleMovedX += OnPaddleMovedX;
    }

    void OnDestroy()
    {
        GameManager.OnGameReady -= OnGameReady;
        Paddle.OnPaddleMovedX -= OnPaddleMovedX;
    }

    void Update()
    {
        HandleBallBeforeLaunch();
    } 

    private void OnGameReady()
    {
        isBallLaunched = false;
        PrepareForLaunch();
    }

    private void OnPaddleMovedX(float paddleX)
    {
        if (!isBallLaunched)
        {
            transform.position = new Vector3(paddleX, transform.position.y, transform.position.z);
        }
        
    }

    private void PrepareForLaunch()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        ballRigidbody.linearVelocity = Vector2.zero;
    }

    private void HandleBallBeforeLaunch()
    {
        if (!isBallLaunched)
        {
            LaunchBallIfMouseIsPressed();
        }
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
