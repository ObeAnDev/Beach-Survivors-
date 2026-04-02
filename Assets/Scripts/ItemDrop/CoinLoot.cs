using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLoot : ItemLoot
{
    [SerializeField] int coin;
    public int Coin => coin;
    public override void OnPickUp(GameObject player)
    {
        player.GetComponent<LvlSystemController>().addCoin(coin);

        Destroy(gameObject);
    }
}
