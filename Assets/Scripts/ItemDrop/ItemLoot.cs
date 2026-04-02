using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemLoot : MonoBehaviour
{
    public abstract void OnPickUp(GameObject player);
}
