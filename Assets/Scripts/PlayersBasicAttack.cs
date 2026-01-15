using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayersBasicAttack : MonoBehaviour
{
    public int damage;
    public float attackRange;

    public LayerMask enemyLayer;

    void Start()
    {
        
    }

    
    void Update()
    {
        EnemyReach();
    }
    public void Attack()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void EnemyReach()
    {
        List<Collider> collider = Physics.OverlapSphere(transform.position, attackRange, enemyLayer).ToList<Collider>(); 

        foreach (var reached in collider)
        {
            if (reached.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("In sphere");
                Attack();
            }
        }
            
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }
}
