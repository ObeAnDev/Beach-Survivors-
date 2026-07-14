using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    public Transform firePoint;

    [SerializeField] private Projectile projectilePref;
    [SerializeField] private int startPoolSize = 30;

    private Queue <Projectile> pool = new Queue<Projectile>();

    private void Awake()
    {
        instance = this;

        for (int i =0; i < startPoolSize; i++)
        {
            Projectile p = Instantiate(projectilePref, firePoint.position, firePoint.rotation);

            p.gameObject.SetActive(false);
            pool.Enqueue(p);
        }
    }
    public Projectile Get()
    {
        Projectile p;
        
        if (pool.Count > 0)
        {
            p = pool.Dequeue();
        }
        else
        {
            p = Instantiate(projectilePref, firePoint.position, firePoint.rotation);
        }
         
        p.gameObject.SetActive(true);

        return p;
    }
    public void ReturnToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        pool.Enqueue(projectile);
    }
}
