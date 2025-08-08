using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public static event Action<float> OnPaddleMovedX;
    private SpriteRenderer spriteRenderer;
    private int lastScreenWidth;
    private int lastScreenHeight;
    private float limitX;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;

        DefineLimitX();
    }

    void Update()
    {
        RedefineLimitXAtResolutionChanges();
        MovePositionX();
    }

    private void DefineLimitX()
    {
        float width = CameraUtils.GetCameraWidth();
        float paddleWidth = spriteRenderer.bounds.size.x;
        limitX = (width / 2) - (paddleWidth / 2);
    }

    private void RedefineLimitXAtResolutionChanges()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            DefineLimitX();
        }
    }

    private void MovePositionX()
    {
        Vector3 mousePosition = CameraUtils.MainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 paddlePosition = transform.position;

        float clampedX = Mathf.Clamp(mousePosition.x, -limitX, limitX);
        paddlePosition.x = clampedX;
        transform.position = paddlePosition;
        OnPaddleMovedX?.Invoke(clampedX);
    }


}
