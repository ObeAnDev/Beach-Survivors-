using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance { get; private set; }

    [Header("Текущий арсенал")]
    public List<PlayerRangedWeapon> activeWeapons = new List<PlayerRangedWeapon>();
    public List<ItemData> activeAttachments = new List<ItemData>();

    private void Awake()
    {
        instance = this;
    }

    // Метод для карточки повышения уровня оружия
    public void UpgradeWeaponCard(PlayerRangedWeapon weapon)
    {
        if (activeWeapons.Contains(weapon))
        {
            weapon.LevelUp();
        }
    }

    // Метод для карточки добавления/улучшения обвеса
    public void AddAttachmentCard(ItemData newItem)
    {
        activeAttachments.Add(newItem);
        // Пересчитываем статы всего оружия, так как обвес обновился
        foreach (var weapon in activeWeapons)
        {
            weapon.UpdateCurrStats();
        }
    }

    public bool HasAttachment(ItemData item)
    {
        return activeAttachments.Contains(item);
    }

    // Считаем общие модификаторы от пляжных вещей
    public float GetTotalDamageMultiplier()
    {
        float multiplier = 1f;
        foreach (var item in activeAttachments) multiplier += (item.damageMultiplier - 1f);
        return multiplier;
    }

    public float GetTotalCooldownReduction()
    {
        float reduction = 0f;
        foreach (var item in activeAttachments) reduction += item.coolDownReduction;
        return Mathf.Clamp(reduction, 0f, 0.6f); // Кап КД в 60%
    }

    public void EvolveWeapon(PlayerRangedWeapon oldWeapon, WeaponData evolvedData)
    {
        activeWeapons.Remove(oldWeapon);

        // PlayerRangedWeapon newWeapon = Instantiate(evolvedWeaponPrefab, transform);
        // activeWeapons.Add(newWeapon);
        // newWeapon.Init();

        Destroy(oldWeapon.gameObject);
    }
}