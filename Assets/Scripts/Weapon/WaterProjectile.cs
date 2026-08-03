using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : Projectile
{
    // ”брали damage и speed, так как они уже есть в базовом классе Projectile
    private int pierceLeft;
    private Vector3 moveDirection;

    public void Launch(float _damage, float _speed, int _pierceCount, Vector3 _direction, float _range)
    {
        // ѕрисваиваем значени€ переменным из базового класса Projectile
        this.damage = _damage;
        this.speed = _speed;

        this.pierceLeft = _pierceCount;
        this.moveDirection = _direction;

        Destroy(gameObject, _range / _speed);
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealthManager enemyHealth = other.GetComponent<EnemyHealthManager>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            //SpawnWaterSplashEffect();

            pierceLeft--;

            if (pierceLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
