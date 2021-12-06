using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject gameOverScreen;
    public Text secondsSurvivedText;

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
        FindObjectOfType<PlayerController>().OnPlayerDeath += OnGameOver;   
             
        halfScreenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        spawnPosY = Camera.main.orthographicSize + 2f;
        // StartCoroutine(DifficultyIncrease());
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (!gameOver)
        {
            Spawn();
            SetDifficulty();
        }       
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
        return Mathf.Clamp01(Time.timeSinceLevelLoad/timeToMaxDifficulty);
    }

    void OnGameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        secondsSurvivedText.text = Mathf.Round(Time.timeSinceLevelLoad).ToString();       
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
