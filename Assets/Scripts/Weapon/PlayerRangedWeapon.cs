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
            WaterProjectile bullet = Instantiate(weaponData.projectilePref, firePoint.position, firePoint.rotation).GetComponent<WaterProjectile>();

            // Небольшое смещение угла, если снарядов несколько (эффект дробовика/веера)
            float angleOffset = (i - (currProjCount - 1) / 2f) * 15f;

            Vector3 fireDirection = Quaternion.Euler(0, angleOffset, 0) * firePoint.forward;
            bullet.Launch(currDamage,currProjSpeed, currPierce, fireDirection, currRange);
            bullet.transform.rotation = Quaternion.LookRotation(fireDirection);
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