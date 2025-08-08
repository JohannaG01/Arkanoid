using UnityEngine;
public class Brick : MonoBehaviour
{
    private int hitPoints;

    public void Setup(int hitPoints)
    {
        this.hitPoints = hitPoints;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HandleBallCollision();
        }
    }

    private void HandleBallCollision()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}