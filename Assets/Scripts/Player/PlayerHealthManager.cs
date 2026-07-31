using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;

    [SerializeField] float health = 100;

    public float HealthPercent => health / maxHealth;
    public float Health => health;



    public void TakeDamage(float damage, GameObject attacker)
    {
        if (damage <= 0)
            return;

        if (ArmorManager.inInstance != null)
            damage = ArmorManager.inInstance.CalculateIncomingDamage(damage,attacker);

        if (damage <= 0)
            return;

        health -= damage;

        if (health < 0)
            health = 0;

        PlayerEventBus.OnHealthChanged?.Invoke(HealthPercent);

        if (health <= 0)
            Die();
    }

    void Die()
    {
        PlayerEventBus.OnPlayerDeath?.Invoke();

        Destroy(gameObject);
    }
}
