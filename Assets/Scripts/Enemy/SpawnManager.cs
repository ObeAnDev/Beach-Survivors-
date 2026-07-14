using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public EnemyHealthManager enemyHealthManager;
    public static SpawnManager instance;
    public float spawnRadius;
    public Transform player;

    public float spawnInterval;
    public int spawnAmountMax;
    public int spawnAmountCrr;

    public float timer;

    //public BoxCollider spawnZone;
    public GameObject enemyPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void SpawnEnemy()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * spawnRadius;
        float z = Mathf.Sin(angle) * spawnRadius;

        Vector3 spawnPos = new Vector3(player.position.x + x, 1f, player.position.z + z);

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemyHealthManager = enemyObj.GetComponent<EnemyHealthManager>();
        enemyHealthManager.spawnManager = this;
        enemyObj.GetComponent<Renderer>().material.color = Random.ColorHSV();


        spawnAmountCrr++;
    }
    public void RemoveOne()
    {
        spawnAmountCrr--;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            for (int i = spawnAmountCrr; i < spawnAmountMax; i++)
            {
                SpawnEnemy();
            }
            timer = 0f;
        }
    }
}
