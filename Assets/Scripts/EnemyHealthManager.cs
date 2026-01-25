using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] int health;
    public int Health => health;
    public void TakeDamage(int _damage)
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
        Destroy(gameObject);
    }
}
