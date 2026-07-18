using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponLevelStats 
{
    public int level;
    [TextArea] public string upgradeDescription;
    public float damage;
    public float fireRate;
    public float attackRange;
    public float projectileSpeed;
    public int pierceCount;
    public int projectileCount;
}

[CreateAssetMenu(fileName = "NewBeachWeapon", menuName = "BeachSurvivors/Weapon Data")]

public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;
    public Projectile projectilePref;

    public bool isEvolvedForm;
    public WeaponData evolvedWeaponPref;
    public ItemData requiredAttachment;

    public List<WeaponLevelStats> levelStats = new List<WeaponLevelStats>();
}
