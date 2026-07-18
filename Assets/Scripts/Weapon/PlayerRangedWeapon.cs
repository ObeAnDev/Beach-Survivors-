using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;

public class PlayerRangedWeapon : PlayerWeapon
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask groundLayer;

    private int currLevel = 1;
    private bool isCooldown = false;
    private Camera mainCamera;

    private float currDamage;
    private float currFireRate;
    private float currRange;
    private float currProjSpeed;
    private int currPierce;
    private int currProjCount;

    public int CurrLevel => currLevel;
    public WeaponData WeaponData => weaponData;

    public override void Init()
    {
        isCooldown = false;
        mainCamera = Camera.main; // Кэшируем главную камеру для оптимизации
        UpdateCurrStats();
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
    public void UpdateCurrStats()
    {
        if (weaponData == null || weaponData.levelStats.Count == 0)
        {
            return;
        }

        WeaponLevelStats stats = weaponData.levelStats.Find(s => s.level == currLevel);

        if (stats.level == 0)
        {
            stats = weaponData.levelStats[weaponData.levelStats.Count - 1];
        }

        currDamage = stats.damage;
        currFireRate = stats.fireRate;
        currRange = stats.attackRange;
        currProjSpeed = stats.projectileSpeed;
        currPierce = stats.pierceCount;
        currProjCount = stats.projectileCount;

        if (PlayerInventory.instance != null)
        {
            currDamage *= PlayerInventory.instance.GetTotalDamageMultiplier();
            currFireRate *= (1f - PlayerInventory.instance.GetTotalCooldownReduction());
        }
    }
    public void LevelUp()
    {
        if (currLevel < weaponData.levelStats.Count)
        {
            currLevel++;
            UpdateCurrStats();
        }
        else
        {
            TryEvolve();
        }
    }
    private void TryEvolve()
    {
        if (weaponData.evolvedWeaponPref == null || weaponData.requiredAttachment == null)
        {
            return;
        }

        if (PlayerInventory.instance.HasAttachment(weaponData.requiredAttachment))
        {
            PlayerInventory.instance.EvolveWeapon(this, weaponData.evolvedWeaponPref);
        }
    }

    private void RotateTowardsMouse()
    {
        if (mainCamera == null)
        {
             return;
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

        // Стреляем количеством снарядов (currentProjCount) веером или по очереди
        for (int i = 0; i < currProjCount; i++)
        {
            Projectile bullet = ProjectilePool.instance.Get();
            bullet.transform.position = firePoint.position;

            // Небольшое смещение угла, если снарядов несколько (эффект дробовика/веера)
            float angleOffset = (i - (currProjCount - 1) / 2f) * 10f;
            Quaternion fireRotation = firePoint.rotation * Quaternion.Euler(0, angleOffset, 0);
            bullet.transform.rotation = fireRotation;

            // Инициализируем снаряд расширенными статами (урон, скорость, пробитие)
            // bullet.Init(currentDamage, currentProjSpeed, currentPierce); 
            bullet.Init(currDamage); // Твой базовый метод

            if (currProjCount > 1) yield return new WaitForSeconds(0.05f); // Короткая задержка между очередью
        }

        yield return new WaitForSeconds(Mathf.Max(0.1f, currFireRate));
        isCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, currRange > 0 ? currRange: currRange);
    }
}