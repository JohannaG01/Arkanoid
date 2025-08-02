using UnityEngine;

public static class CameraUtils
{
    public static float GetCameraHeight()
    {
        return Camera.main.orthographicSize * 2f;
    }

    public static float GetCameraWidth()
    {
        return GetCameraHeight() * Camera.main.aspect;
    }
}