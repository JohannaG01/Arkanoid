using System;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    public static event Action OnReplayButtonPressed;
    public void InvokeOnReplayButtonPressed()
    {
        OnReplayButtonPressed?.Invoke();
    }
}
