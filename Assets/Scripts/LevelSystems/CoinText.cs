using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    private void Awake()
    {
        PlayerEventBus.OnCoinChange += coinUpdate;
    }
    public void coinUpdate(int coin)
    {
        coinText.text = coin.ToString(); 
    }
}
