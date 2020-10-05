using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "New Level")]
public class Level : ScriptableObject
{

    [SerializeField] string levelName;
    [SerializeField] Sprite background;
    [SerializeField] int scoreToBeat;
    [SerializeField] float spawnInterval;
    [SerializeField] string nextLevel;
    [SerializeField] GameObject enemyPrefab;

    public string LevelName { get { return levelName; } }
    public Sprite Background { get { return background; } }
    public int ScoreToBeat { get { return scoreToBeat; } }
    public float SpawnInterval { get { return spawnInterval; } }
    public string NextLevel { get { return nextLevel; } }
    public GameObject EnemyPrefab {  get { return enemyPrefab; } }
}
