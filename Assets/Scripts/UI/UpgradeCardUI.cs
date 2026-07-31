using TMPro;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    public UpgradeSO data;

    [Header("UI References")]
    public TextMeshProUGUI titleText;      // Заголовок (upgradeName)
    public TextMeshProUGUI descriptionText; // Описание (cardText + дополнительная инфа)
    public Image image;                    // Иконка карточки
    public Button button;                  // Кнопка выбора

    public void Setup(UpgradeSO upgrade)
    {
        data = upgrade;

        // 1. Устанавливаем иконку
        if (image != null)
        {
            image.sprite = data.cardImage;
            image.enabled = (data.cardImage != null); // Скрываем Image, если картинки нет
        }

        // 2. Устанавливаем заголовок
        if (titleText != null)
        {
            titleText.text = !string.IsNullOrEmpty(data.upgradeName) ? data.upgradeName : "Upgrade";
        }

        // 3. Формируем подробный текст описания в зависимости от типа карточки
        if (descriptionText != null)
        {
            descriptionText.text = BuildCardDescription();
        }

        // 4. Настраиваем нажатие на кнопку
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Select);
        }
    }

    private string BuildCardDescription()
    {
        string baseText = data.cardText;

        switch (data.upgradeType)
        {
            case UpgradeType.PlayerStat:
                // Выводим базовый текст и добавку к характеристикам
                string sign = data.statModifier >= 0 ? "+" : "";

                if (data.statType == PlayerStatType.PercentArmor || data.statType == PlayerStatType.BlockChance)
                {
                    return $"{baseText}\n<color = #88FFFF>{data.statType}: {sign}{data.statModifier * 100}%</color>";
                }
                else if (data.statType == PlayerStatType.FlatArmor)
                {
                    return $"{baseText}\n<color = #88FFFF>Armor: {sign}{data.statModifier} damage block</color>";
                }
                return $"{baseText}\n<color=#88FF88>{data.statType}: {sign}{data.statModifier}</color>";
            case UpgradeType.NewWeapon:
                // Для нового оружия
                return $"{baseText}\n<color=#FFDD88>New Weapon Unlocked!</color>";

            case UpgradeType.UpgradeWeapon:
                // Для прямого увеличения урона оружия
                return $"{baseText}\n<color=#FF8888>Damage: +{data.damageIncrease}</color>";

            case UpgradeType.LevelUpWeapon:
                // Динамически получаем уровень оружия и текст следующего уровня
                if (WeaponManager.instance != null)
                {
                    PlayerRangedWeapon weapon = WeaponManager.instance.GetWeaponByName(data.targetWeaponName);

                    if (weapon != null && weapon.WeaponData != null)
                    {
                        int nextLevel = weapon.CurrLevel + 1;
                        var levelStats = weapon.WeaponData.levelStats.Find(s => s.level == nextLevel);

                        if (levelStats.level != 0 && !string.IsNullOrEmpty(levelStats.upgradeDescription))
                        {
                            return $"{baseText} (Lvl. {nextLevel})\n<size=85%><color=#FFDD88>{levelStats.upgradeDescription}</color></size>";
                        }
                        else
                        {
                            return $"{baseText}\n<size=85%><color=#FF55FF>Ready to Evolve!</color></size>";
                        }
                    }
                }
                return baseText;

            default:
                return baseText;
        }
    }

    void Select()
    {
        Debug.Log("Picked: " + (data != null ? data.upgradeName : "Unknown"));
        ApplyUpgrade();

        if (UIManager.Instance != null)
        {
            UIManager.Instance.CloseUpgradePanel();
        }
    }

    void ApplyUpgrade()
    {
        if (data == null) return;

        switch (data.upgradeType)
        {
            case UpgradeType.PlayerStat:
                ApplyPlayerStatUpgrade();
                break;

            case UpgradeType.NewWeapon:
                if (data.gamePrefab != null && WeaponManager.instance != null)
                {
                    WeaponManager.instance.EquipNewWeapon(data.gamePrefab);
                }
                break;

            case UpgradeType.UpgradeWeapon:
                if (WeaponManager.instance != null)
                {
                    WeaponManager.instance.UpgradeExistingWeapon(data.targetWeaponName, data.damageIncrease);
                }
                break;

            case UpgradeType.LevelUpWeapon:
                if (WeaponManager.instance != null)
                {
                    PlayerRangedWeapon weaponToLevel = WeaponManager.instance.GetWeaponByName(data.targetWeaponName);
                    if (weaponToLevel != null && PlayerInventory.instance != null)
                    {
                        PlayerInventory.instance.UpgradeWeaponCard(weaponToLevel);
                    }
                }
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
        if (ArmorManager.inInstance != null)
        {
            if (data.statType == PlayerStatType.FlatArmor)
            {
                ArmorManager.inInstance.AddFlatArmor(data.statModifier);
            }
            else if (data.statType == PlayerStatType.PercentArmor)
            {
                ArmorManager.inInstance.AddPercentArmor(data.statModifier);
            }
            else if (data.statType == PlayerStatType.BlockChance)
            {
                ArmorManager.inInstance.AddBlockChance(data.statModifier);
            }
        }

    }
}
