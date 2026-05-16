using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    public UpgradeSO upgConfig;

    public TextMeshProUGUI cardText;
    public Image imageCard;

    public void InIt()
    {
        cardText.text = upgConfig.cardText;
        imageCard.sprite = upgConfig.cardImage;
    }
}
