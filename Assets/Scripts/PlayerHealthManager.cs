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

    public void TakeDamage(float _damage)
    {
        if (_damage <= 0) return;

        health -= _damage;

        if (health < 0)
            health = 0;
        PlayerEventBus.OnHealthChanged.Invoke(HealthPercent);

        if (health == 0)
            Die();
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
