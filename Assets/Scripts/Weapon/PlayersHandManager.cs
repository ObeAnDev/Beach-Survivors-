using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHandManager : PlayerWeapon
{
    [SerializeField] private float damageCooldown = 0.5f;
    private Dictionary<Collider, float> enemyCooldowns = new Dictionary<Collider, float>();

    public override void Init()
    {
        enemyCooldowns.Clear();
    }

    public override void Attack()
    {
        // В этой логике метод Attack может оставаться пустым, 
        // так как рука бьет пассивно через триггер.
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        if (enemyCooldowns.ContainsKey(other))
        {
            if (Time.time < enemyCooldowns[other])
            {
                Debug.Log($"[Hand] Враг {other.name} на кулдауне. Ждем.");
                return;
            }
        }

        EnemyHealthManager enemyHealthManager = other.GetComponent<EnemyHealthManager>();

        if (enemyHealthManager != null)
        {
            enemyHealthManager.TakeDamage(Damage);

            if (enemyCooldowns.ContainsKey(other))
                enemyCooldowns[other] = Time.time + damageCooldown;
            else
                enemyCooldowns.Add(other, Time.time + damageCooldown);

            Debug.Log($"[Hand] Урон {Damage} нанесен врагу {other.name}! Следующий удар через {damageCooldown} сек.");
        }
        else
        {
            Debug.LogError($"[Hand] У {other.name} стоит тег Enemy, но нет скрипта EnemyHealthManager!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemyCooldowns.ContainsKey(other))
        {
            enemyCooldowns.Remove(other);
            Debug.Log($"[Hand] Враг {other.name} вышел из зоны поражения. Кулдаун очищен.");
        }
    }
}
