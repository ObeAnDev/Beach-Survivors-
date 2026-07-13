using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 3f;

    public float damage;
    private Coroutine lifeRoutine;


    public void Init(float weaponDamage)
    {
        damage = weaponDamage;

        if (lifeRoutine != null)
        {
            StopCoroutine(lifeRoutine); 
        }

        lifeRoutine = StartCoroutine(LifeTimer());
    }
    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHealthManager enemyHealth = other.GetComponent<EnemyHealthManager>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        ReturnToPool();
    }
    private void ReturnToPool()
    {
        if (lifeRoutine != null)
        {
            StopCoroutine(lifeRoutine);
        }
        ProjectilePool.instance.ReturnToPool(this);
    }
}
