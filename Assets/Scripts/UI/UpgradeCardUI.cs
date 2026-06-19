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

        text.text = data.cardText;
        image.sprite = data.cardImage;

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
