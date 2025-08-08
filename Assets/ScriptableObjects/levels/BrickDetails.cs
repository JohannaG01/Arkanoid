using UnityEngine;

[System.Serializable]
public class BrickDetails
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Vector2 position;
    [SerializeField] private int hitPoints;
    
     public GameObject BrickPrefab => brickPrefab;
    public Vector2 Position => position;
    public int HitPoints => hitPoints;
}
