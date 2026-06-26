using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField] float range;
    public float Range => range;

    [SerializeField] float speed;
    public float Speed => speed;

    [SerializeField] float attackRate;
    public float AttackRate => attackRate;

    public abstract void Attack();

    public abstract void Init();

}
