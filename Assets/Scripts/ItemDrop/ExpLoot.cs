using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpLoot : ItemLoot
{
    [SerializeField] float exp;
    public float Exp => exp;

    public override void OnPickUp(GameObject player)
    {
        player.GetComponent<LvlSystemController>().AddExp(exp);

        Destroy(gameObject);
    }
}
