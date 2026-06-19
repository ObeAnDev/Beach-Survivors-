using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    private Transform weaponParent;
    private List<PlayerWeapon> activeWeapons = new List<PlayerWeapon>();
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
            if (weapon.gameObject.name.Contains(weaponName) || weapon.GetType().Name == weaponName)
            {
                weapon.Damage += damageBonus;

                return;
            }
        }
    }
}
