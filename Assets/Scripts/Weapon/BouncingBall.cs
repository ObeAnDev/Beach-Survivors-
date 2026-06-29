using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BouncingBall: MonoBehaviour
{
    [SerializeField] float forwardForce = 10f;
    [SerializeField] float upwardForce = 7f;
    [SerializeField] float lifeTime = 5f;

    private float damage;
    private Rigidbody rb;

    public void Init(float weaponDamage, Vector3 launchDirection)
    {
        damage = weaponDamage;
        rb = GetComponent<Rigidbody>();

        Vector3 pushVector = (launchDirection.normalized * forwardForce) + (Vector3.up * upwardForce);

        rb.AddForce(pushVector, ForceMode.Impulse);

        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealthManager enemyHealth = collision.gameObject.GetComponent<EnemyHealthManager>();

            if (enemyHealth != null) 
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
