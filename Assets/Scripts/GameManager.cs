using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    [SerializeField]
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
    private float score;

    [Header("Component")]
    private ObjectPool enemyPool;

    void Awake()
    {
        enemyPool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        afterLastSpawnTime = 0;
        score = 0;
    }

    void Update()
    {
        afterLastSpawnTime += Time.deltaTime;
        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();

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
}