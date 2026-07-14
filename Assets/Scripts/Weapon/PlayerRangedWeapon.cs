using System.Collections;
using UnityEngine;

public class PlayerRangedWeapon : PlayerWeapon
{
    [Header("Ranged Weapon Settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private LayerMask groundLayer; // Слой земли/пола, куда может указывать мышь

    private bool isCooldown = false;
    private Camera mainCamera;

    public override void Init()
    {
        isCooldown = false;
        mainCamera = Camera.main; // Кэшируем главную камеру для оптимизации
    }

    public override void Attack() { }

    private void Update()
    {
        // Поворачиваем оружие/игрока в сторону мышки
        RotateTowardsMouse();

        // Стреляем автоматически, если зажата левая кнопка мыши (или используй GetButtonDown)
        if (Input.GetButton("Fire1") && !isCooldown)
        {
            StartCoroutine(FireRoutine());
        }
    }

    private void RotateTowardsMouse()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                // Умножаем направление на -1, чтобы развернуть объект на 180 градусов по Y
                direction = -direction;

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation;
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        isCooldown = true;

        Projectile bullet = ProjectilePool.instance.Get();

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        bullet.Init(Damage);

        yield return new WaitForSeconds(fireRate);

        isCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}