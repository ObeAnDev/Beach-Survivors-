using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallWeapon : PlayerWeapon
{
    [SerializeField] BouncingBall ballPref;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 1.5f;

    private bool isCooldown = false;

    public override void Init()
    {
        isCooldown = false;
    }
    public override void Attack()
    {

    }
    private void Update()
    {
        if (!isCooldown)
        {
            StartCoroutine(ThrowRoutine());
        }
    }
    private IEnumerator ThrowRoutine()
    {
        isCooldown = true;

        if (ballPref != null && firePoint != null)
        {
            BouncingBall ball = Instantiate(ballPref, firePoint.position, Quaternion.identity);
            ball.Init(Damage, transform.forward);
        }
        yield return new WaitForSeconds(fireRate);
        isCooldown = false;
    }
}
