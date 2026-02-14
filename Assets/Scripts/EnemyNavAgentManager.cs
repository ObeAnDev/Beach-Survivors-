using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgentNavManager : MonoBehaviour
{
    [SerializeField] Transform targetPoint;
    NavMeshAgent agent;

    void Start()
    {
        targetPoint = GameObject.Find("Players Character (TEST)").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(targetPoint.position);
    }
}
