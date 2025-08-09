using System;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public static event Action OnPlayButtonPressed;
    public void InvokeOnPlayButtonPressed()
    {
        OnPlayButtonPressed?.Invoke();
    }
}
