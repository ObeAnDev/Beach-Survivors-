using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemLoot : MonoBehaviour
{
    [SerializeField] DropRateSO dropSO;
    public abstract void OnPickUp(GameObject player);
}
