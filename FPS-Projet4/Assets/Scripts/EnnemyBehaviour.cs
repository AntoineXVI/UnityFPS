using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject following;

    private void followPlayer()
    {
        agent.SetDestination(following.transform.position);
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
