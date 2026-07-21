using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    public UpgradeSO data;

    public TextMeshProUGUI text;
    public Image image;
    public Button button;

    public void Setup(UpgradeSO upgrade)
    {
        data = upgrade;

        /*if (data.upgradeType == UpgradeType.UpgradeWeapon)
        {
            PlayerRangedWeapon currWeapon = WeaponManager.instance.GetWeaponByName(data.targetWeaponName);

            if (currWeapon != null)
            {
                int nextLevel = currWeapon.CurrLevel + 1;

                var levelStats = currWeapon.WeaponData.levelStats.Find(s => s.level == nextLevel);
                string levelDescription = levelStats.level != 0? levelStats.upgradeDescription: "Weapon ready to evolve!";
                text.text = $"{data.cardText} (Lvl. {nextLevel}) \n<size = 80%><color = #FFDD88>{levelDescription}</color></size>";
            }
        }*/

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Select);
    }

    void Select()
    {
        Debug.Log("Picked: " + data.upgradeName);
        ApplyUpgrade();
        UIManager.Instance.CloseUpgradePanel();

        // тут добавишь баффы
    }
    void ApplyUpgrade()
    {
        switch (data.upgradeType) 
        {
            case UpgradeType.PlayerStat:
                ApplyPlayerStatUpgrade();
                break;

            case UpgradeType.NewWeapon:
                if (data.gamePrefab != null)
                {
                    WeaponManager.instance.EquipNewWeapon(data.gamePrefab);
                }
                break;
            case UpgradeType.UpgradeWeapon:
                WeaponManager.instance.UpgradeExistingWeapon(data.targetWeaponName, data.damageIncrease);
                break;
        }

    }
    void ApplyPlayerStatUpgrade()
    {
        PlayerControllerManager playerController = FindObjectOfType<PlayerControllerManager>();
        PlayerHealthManager playerHealth = FindObjectOfType<PlayerHealthManager>();

        if (data.statType == PlayerStatType.Speed && playerController != null)
        {
            playerController.Speed += data.statModifier;
        }
        if (data.statType == PlayerStatType.MaxHealth && playerHealth != null)
        {
            playerHealth.maxHealth += data.statModifier;
        }
    }
}
