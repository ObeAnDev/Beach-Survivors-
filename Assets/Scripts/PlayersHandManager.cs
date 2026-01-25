using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHandManager : MonoBehaviour 
{
    [SerializeField] int damage;
    public int Damage => damage;

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        EnemyHealthManager enemyHealthManager = collision.gameObject.GetComponent<EnemyHealthManager>();

        if (enemyHealthManager != null)
            enemyHealthManager.TakeDamage(damage);
    }
}
