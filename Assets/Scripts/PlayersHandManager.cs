using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHandManager : PlayerWeapon
{

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        EnemyHealthManager enemyHealthManager = collision.gameObject.GetComponent<EnemyHealthManager>();

        if (enemyHealthManager != null)
            enemyHealthManager.TakeDamage(Damage);
    }

    public override void Attack()
    {
       
    }

    public override void Init()
    {

    }
}
