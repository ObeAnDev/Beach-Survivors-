using UnityEngine;
public enum UpgradeType
{
    PlayerStat,
    NewWeapon,
    UpgradeWeapon,
    LevelUpWeapon,
}
public enum PlayerStatType
{
    None,
    Speed,
    MaxHealth,
}


[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string cardText;
    public Sprite cardImage;

    public UpgradeType upgradeType;

    public PlayerStatType statType;
    public float statModifier;

    public GameObject gamePrefab;

    public string targetWeaponName;
    public float damageIncrease;
}
