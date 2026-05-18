using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    [SerializeField] GameObject[] whatInst;
    [SerializeField] Transform whereInst;
    [SerializeField] Vector3 offset;

    [SerializeField] float health;
    public float Health => health;
    public void TakeDamage(float _damage)
    {
        if (_damage <= 0) return; 

        health -= _damage;

        if (health < 0)
            health = 0;

        if (health == 0)
            Die();
    }
    void Die()
    {
        DropOn();

        Destroy(gameObject);
        spawnManager.RemoveOne();
    }
    public void DropOn()
    {
        GameObject instantiatedObject = Instantiate(whatInst[Random.Range(0, whatInst.Length)], whereInst.position + offset, whereInst.rotation);
    }
}
