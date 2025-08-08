using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
   [SerializeField]
    private BrickDetails[] bricks;

    public BrickDetails[] Bricks => bricks; 
}
