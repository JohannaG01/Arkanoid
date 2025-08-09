using System;
using UnityEngine;
public class Brick : MonoBehaviour
{
    public static event Action OnBrickDestroyed;
    private int hitPoints;

    public void Setup(int hitPoints)
    {
        this.hitPoints = hitPoints;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        hitPoints--;
        DestroyIfNoHitPointsLeft();
    }

    private void DestroyIfNoHitPointsLeft()
    {
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            OnBrickDestroyed?.Invoke();
        }
    }
}