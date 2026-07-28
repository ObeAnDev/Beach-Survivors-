using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    [SerializeField]private Transform weaponParent;
    [SerializeField]private List<PlayerWeapon> activeWeapons = new List<PlayerWeapon>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayerWeapon[] startingWeapons = GetComponentsInChildren<PlayerWeapon>();

        foreach(var weapon in startingWeapons)
        {
            AddWeaponToActiveList(weapon);
        }
    }
    void AddWeaponToActiveList(PlayerWeapon weapon) 
    {
        if (!activeWeapons.Contains(weapon))
        {
            activeWeapons.Add(weapon);
            weapon.Init();
        }
    }
    public void EquipNewWeapon(GameObject weaponPrefab)
    {
        GameObject newWeaponGo = Instantiate(weaponPrefab, weaponParent.position, weaponParent.rotation, weaponParent);
        PlayerWeapon newWeapon = newWeaponGo.GetComponent<PlayerWeapon>();

        if (newWeapon != null)
        {
            AddWeaponToActiveList(newWeapon);

            Debug.Log($"Equipped new weapon: {newWeaponGo.name}");
        }
    }
    public void UpgradeExistingWeapon(string weaponName, float damageBonus)
    {
        foreach (var weapon in activeWeapons)
        {
            // 1. Check if the element itself or its GameObject has been destroyed
            if (weapon == null || weapon.gameObject == null) continue;

            // 2. Check if weaponName is null to avoid string method exceptions
            if (string.IsNullOrEmpty(weaponName)) return;

            if (weapon.gameObject.name.Contains(weaponName) || weapon.GetType().Name == weaponName)
            {
                weapon.Damage += damageBonus;
                weapon.RefreshStats();
                return;
            }
        }

        Debug.LogWarning($"WeaponManager: Could not find weapon matching '{weaponName}' to upgrade.");
    }
    public PlayerRangedWeapon GetWeaponByName(string weaponName)
    {
        foreach (var weapon in activeWeapons)
        {
            if (weapon is PlayerRangedWeapon rangedWeapon &&(weapon.gameObject.name.Contains(weaponName) || weapon.GetType().Name == weaponName))
            {
                return rangedWeapon;
            }
        }
        return null;
    }
}