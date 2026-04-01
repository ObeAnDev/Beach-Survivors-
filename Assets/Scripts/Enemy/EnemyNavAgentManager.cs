using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgentNavManager : MonoBehaviour
{
    [SerializeField] Transform targetPoint;
    public float speed;

    void Start()
    {
        targetPoint = GameObject.Find("Player Model").transform;
    }

    void Update()
    {

        Vector3 direction = (targetPoint.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

    }


    
}
