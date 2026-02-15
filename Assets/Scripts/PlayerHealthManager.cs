using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float HealthPercent => health/maxHealth;

    [SerializeField] float health;
    public float Health => health;

    public HealthBar healthBar;

    public void TakeDamage(float _damage)
    {
        if (_damage <= 0) return;

        health -= _damage;
        healthBar.HealthUpdate(HealthPercent);

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
