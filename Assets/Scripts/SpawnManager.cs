using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public BoxCollider spawnZone;
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
        Bounds bounds = spawnZone.bounds;

        Vector3 spawnPoint = new Vector3
        (
            Random.Range(bounds.min.x, bounds.max.x),
            0,
            Random.Range(bounds.min.z, bounds.max.z)
        );

        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnEnemy();
        }
    }
}
