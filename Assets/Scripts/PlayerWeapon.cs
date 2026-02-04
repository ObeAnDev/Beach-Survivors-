using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    //public float damage;
    public float range;
    public float speed;
    public float attackRate;

    public abstract void Attack();

    public abstract void Init();
}