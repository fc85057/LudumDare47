using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static event Action OnScoreReached = delegate { };
    public static event Action OnLevelLoaded = delegate { };
    public static event Action OnFinalPortalEntered = delegate { };

    [SerializeField] Level[] levelScriptableObjects;
    Level currentLevel;
    [SerializeField] Portal[] portals;
    [SerializeField] int maxEnemies = 8;

    Dictionary<string, Level> levels;
    List<GameObject> enemies;
    float currentSpawnInterval;

    SpriteRenderer sr;

    void Start()
    {

        Portal.OnPlayerEntered += LoadNewLevel;
        Enemy.OnEnemyDie += RemoveEnemy;

        sr = GetComponent<SpriteRenderer>();

        GameManager.OnScoreChanged += ScoreCheck;

        levels = new Dictionary<string, Level>();
        foreach (Level level in levelScriptableObjects)
        {
            levels.Add(level.LevelName, level);
        }

        enemies = new List<GameObject>();

        LoadNewLevel("Prehistoric");

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int numberOfEnemies = UnityEngine.Random.Range(0, 3);
            for (int i = 0; i <= numberOfEnemies; i++)
            {
                if (currentLevel.EnemyPrefab != null && enemies.Count < maxEnemies)
                {
                    GameObject newEnemy = Instantiate(currentLevel.EnemyPrefab, 
                        new Vector2(UnityEngine.Random.Range(0, 25), 0), Quaternion.identity);
                    enemies.Add(newEnemy);
                }
                
            }
            if (currentSpawnInterval > 1.5f)
            {
                currentSpawnInterval -= .02f;
            }
            yield return new WaitForSeconds(currentSpawnInterval);
        }
        
    }

    void RemoveEnemy(GameObject enemyToRemove)
    {
        enemies.Remove(enemyToRemove);
    }

    void ScoreCheck(int currentScore)
    {
        if (currentScore > currentLevel.ScoreToBeat)
        {
            OnScoreReached();
        }
    }

    void LoadNewLevel(string newLevel)
    {
        currentLevel = levels[newLevel];
        sr.sprite = currentLevel.Background;
        OnLevelLoaded();
    }

    void LoadNewLevel()
    {

        if (currentLevel.NextLevel == "Outro")
        {
            OnFinalPortalEntered();
            return;
        }

        currentLevel = levels[currentLevel.NextLevel];
        sr.sprite = currentLevel.Background;
        StopAllCoroutines();
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
        OnLevelLoaded();
        currentSpawnInterval = currentLevel.SpawnInterval;
        StartCoroutine(SpawnEnemies());
    }

}
