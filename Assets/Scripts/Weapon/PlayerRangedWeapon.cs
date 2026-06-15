using System.Collections;
using UnityEngine;

public class PlayerRangedWeapon : PlayerWeapon
{
    [Header("Ranged Weapon Settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float attackRange = 10f;

    private bool isCooldown = false;

    public override void Init()
    {
        isCooldown = false;
    }

    public override void Attack() { }

    private void Update()
    {
        Transform targetEnemy = FindClosestEnemy();

        if (targetEnemy != null)
        {
            RotateTowardsTarget(targetEnemy);

            if (!isCooldown)
            {
                StartCoroutine(FireRoutine());
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        isCooldown = true;

        Projectile bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        bullet.Init(Damage);

        yield return new WaitForSeconds(fireRate);

        isCooldown = false;
    }

    private Transform FindClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = collider.transform;
                }
            }
        }

        return closestEnemy;
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = lookRotation;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}