using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyApplyDamage : MonoBehaviour
{
    [SerializeField] float damage;
    public float Damage => damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        PlayerHealthManager playerHealthManager = collision.gameObject.GetComponent<PlayerHealthManager>();

        while (playerHealthManager != null)
        {
            playerHealthManager.TakeDamage(Damage);
        }
    }
}
