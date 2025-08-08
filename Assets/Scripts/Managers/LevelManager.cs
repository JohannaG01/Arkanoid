using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    private Level currentLevel;

    void Awake()
    {   
        currentLevel = levels[0];
        GameManager.OnGameReady += OnGameReady;
    }

    void OnDestroy()
    {
        GameManager.OnGameReady -= OnGameReady;
    }

    private void OnGameReady()
    {
        LoadLevel();
    }
    
    private void LoadLevel()
    {
        foreach (var brick in currentLevel.Bricks)
        {
            Vector3 position = new Vector3(brick.Position.x, brick.Position.y, 0f);
            GameObject brickObj = Instantiate(brick.BrickPrefab, position, Quaternion.identity, transform);

            var brickScript = brickObj.GetComponent<Brick>();
            brickScript.Setup(brick.HitPoints);
        }
    }
}