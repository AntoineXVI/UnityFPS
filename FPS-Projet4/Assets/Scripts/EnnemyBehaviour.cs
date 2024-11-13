using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject following;
    private float stopDistance = 5f;

    private void followPlayer()
    {
        float distance = Vector3.Distance(agent.transform.position, following.transform.position);
        if (distance > stopDistance)
        {
            agent.SetDestination(following.transform.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        following = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }
}