using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyApplyDamage : MonoBehaviour
{
    [SerializeField] float damage;
    public float Damage => damage;

    public float attackRate;
    public float timer;

    private void OnCollisionStay (Collision collision)
    {
        timer += Time.deltaTime;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        PlayerHealthManager playerHealthManager = collision.gameObject.GetComponent<PlayerHealthManager>();
        
        if(playerHealthManager != null)
        {
            if (timer >= attackRate)
            {
                playerHealthManager.TakeDamage(Damage);
                timer = 0;
            }
        }
    }

}
