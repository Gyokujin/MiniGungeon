using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Score
{
    public static float score;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Player")]
    [SerializeField]
    private GameObject player;

    [Header("Spawn")]
    [SerializeField]
    private float spawnTerm = 5;
    [SerializeField]
    private float fasterSpawnTerm = 0.05f;
    [SerializeField]
    private float minSpawnTerm = 1;
    private float afterLastSpawnTime;
    [SerializeField]
    private float maxXPos = 11.5f;
    [SerializeField]
    private float minXPos = -9.5f;
    [SerializeField]
    private float maxYPos = 4.7f;
    [SerializeField]
    private float minYPos = -4.85f;

    [Header("System")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Component")]
    private ObjectPool enemyPool;

    void Awake()
    {
        enemyPool = GetComponent<ObjectPool>();
        instance = this;
    }

    void Start()
    {
        afterLastSpawnTime = 0;
        Score.score = 0;
    }

    void Update()
    {
        afterLastSpawnTime += Time.deltaTime;
        Score.score += Time.deltaTime;
        scoreText.text = ((int)Score.score).ToString();

        if (afterLastSpawnTime > spawnTerm)
        {
            afterLastSpawnTime -= spawnTerm;
            SpawnEnemy();
            spawnTerm -= fasterSpawnTerm;

            if (spawnTerm < minSpawnTerm)
            {
                spawnTerm = minSpawnTerm;
            }
        }
    }

    void SpawnEnemy()
    {
        float xPos = Random.Range(minXPos, maxXPos);
        float yPos = Random.Range(minYPos, maxYPos);

        GameObject newEnemy = enemyPool.Get();
        newEnemy.transform.position = new Vector3(xPos, yPos, 0);
        newEnemy.GetComponent<EnemyController>().Spawn(player);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}