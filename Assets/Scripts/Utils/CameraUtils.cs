using UnityEngine;

public static class CameraUtils
{
    public static Camera MainCamera => _cachedCamera != null ? _cachedCamera : (_cachedCamera = Camera.main);
    private static Camera _cachedCamera;
    public static float GetCameraHeight()
    {
        return MainCamera.orthographicSize * 2f;
    }

    public static float GetCameraWidth()
    {
        return GetCameraHeight() * MainCamera.aspect;
    }
}