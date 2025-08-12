using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action OnLevelComplete;
    public static event Action OnGameWin;
    [SerializeField] private Level[] levels;
    private Level currentLevel;
    private int currentLevelIndex = 0;
    private int bricksLeft;

    void Awake()
    {
        GameManager.OnGameReady += OnGameReady;
        Brick.OnBrickDestroyed += OnBrickDestroyed;
    }

    void OnDestroy()
    {
        GameManager.OnGameReady -= OnGameReady;
        Brick.OnBrickDestroyed -= OnBrickDestroyed;
    }

    private void OnGameReady()
    {
        if (currentLevelIndex >= levels.Length)
        {
            OnGameWin?.Invoke();
        }
        else
        {
            SelectLevel();
            LoadLevel();
        }
    }

    private void OnBrickDestroyed()
    {
        bricksLeft--;
        InvokeOnLevelCompleteIfNoBricksLeft();
    }

    private void InvokeOnLevelCompleteIfNoBricksLeft()
    {
        if (bricksLeft <= 0)
        {
            OnLevelComplete?.Invoke();
        }
    }

    private void SelectLevel()
    {
        currentLevel = levels[currentLevelIndex];
        bricksLeft = currentLevel.BreakableBricksLength;
        currentLevelIndex++;
    }

    private void LoadLevel()
    {
        foreach (var brickData in currentLevel.Bricks)
        {
            GameObject brick = CreateBrick(brickData);
            SetupBrick(brick, brickData);
        }
    }

    private GameObject CreateBrick(BrickDetails brickData)
    {
        Vector3 position = new Vector3(brickData.Position.x, brickData.Position.y, 0f);
        return Instantiate(brickData.BrickPrefab, position, Quaternion.identity, transform);
    }

    private void SetupBrick(GameObject brickObj, BrickDetails brickData)
    {
        var brickScript = brickObj.GetComponent<Brick>();

        if (brickScript != null)
        {
            brickScript.Setup(brickData.HitPoints);
        }
    }

}