using TMPro;
using UnityEngine;
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

        UIManager.Instance.CloseUpgradePanel();

        // тут добавишь баффы
    }
}
