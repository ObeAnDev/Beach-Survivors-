using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public float spawnRadius;
    public Transform player;


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

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnEnemy();
        }
    }
}
