using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerRangeManager: MonoBehaviour
{
    [SerializeField] float attackRange;
    public float AttackRange => attackRange;

    public LayerMask enemyLayer;

    void Update()
    {
        EnemyReach();
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
            }
        }        
    }
}
