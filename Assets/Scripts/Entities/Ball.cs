using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialVelocityX;
    [SerializeField] private float initialVelocityY;
    [SerializeField] private float maxBounceAngleSpeedX;
    private Vector3 paddleOffset;
    private Transform paddleTransform;
    private Rigidbody2D ballRigidbody;

    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PositionOnPaddleIfGameIsReady();
    }

    public void PrepareForLaunch()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        ballRigidbody.linearVelocity = Vector2.zero;  
    }

    public void AttachToPaddle(Transform paddle)
    {
        paddleTransform = paddle;
        paddleOffset = transform.position - paddle.position;
        PositionOnPaddle();
    }

    public void Launch()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.linearVelocity = new Vector2(initialVelocityX, initialVelocityY);
    }

    private void PositionOnPaddleIfGameIsReady()
    {
        if (GameManager.Instance.CurrentState == GameState.Ready)
        {
            PositionOnPaddle();
        }
    }

    private void PositionOnPaddle()
    {
        transform.position = paddleTransform.position + paddleOffset;
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
