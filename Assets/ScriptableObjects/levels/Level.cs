using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    private BrickDetails[] bricks;

    [SerializeField, HideInInspector]
    private int breakableBricksLength;

    public BrickDetails[] Bricks => bricks;

    public int BreakableBricksLength => breakableBricksLength;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        breakableBricksLength = bricks.Count(b =>
            b.BrickPrefab.GetComponent<Brick>() != null
        );
    }
    #endif
}
