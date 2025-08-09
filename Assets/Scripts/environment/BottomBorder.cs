using System;
using UnityEngine;


public class BottomBorder : MonoBehaviour
{
    public static event Action OnBallLost;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            InvokeOnBallLost();
        }
    }

    private void InvokeOnBallLost()
    {
        OnBallLost?.Invoke();
    }
}
