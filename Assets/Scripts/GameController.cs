using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public static float obstacleSpeed;
    public Vector2 spawnIntervalMinMax;
    private float spawnInterval;
    private float difficulty;
    public float timeToMaxDifficulty = 30f;
    // public int difficultyIncreaseTimer;

    [SerializeField] bool gameOver;
    float timeSinceSpawn;
    [HideInInspector] public float halfScreenWidth;
    float spawnPosY;
    void Start()
    {
        difficulty = 0;
        
        halfScreenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        spawnPosY = Camera.main.orthographicSize + 2f;
        // StartCoroutine(DifficultyIncrease());
    }

    void Update()
    {
        Spawn();
        SetDifficulty();
    }

    private void Spawn()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > spawnInterval)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-halfScreenWidth, halfScreenWidth), spawnPosY);
            Instantiate(obstaclePrefab, randomPosition, obstaclePrefab.transform.rotation);
            timeSinceSpawn = 0;
        }
    }
    private void SetDifficulty()
    {
        difficulty = Mathf.Lerp(0, 20f, GetDifficultyPercent());
        obstacleSpeed = difficulty;
        spawnInterval = Mathf.Lerp(spawnIntervalMinMax.y, spawnIntervalMinMax.x, GetDifficultyPercent());
    }
    private float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.time/timeToMaxDifficulty);
    }

    // private IEnumerator DifficultyIncrease()
    // {
    //     while(!gameOver)
    //     {
    //         yield return new WaitForSeconds(difficultyIncreaseTimer);
    //         if (difficulty < 20)
    //         {
    //             difficulty++;
    //             obstacleSpeed = difficulty;
    //             if (spawnInterval > 0.5f)
    //             {
    //                 spawnInterval -= 0.1f;
    //             }
    //         }
    //     }
        
    // }
}
