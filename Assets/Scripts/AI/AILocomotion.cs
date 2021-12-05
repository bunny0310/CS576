using System;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        try
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
