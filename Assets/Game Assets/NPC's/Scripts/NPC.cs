using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform Route;
    public bool Loop;
    public bool FollowRoute;
    NavMeshAgent agent;
    Animator animator;

    int CurruntDestinationIndex;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        CurruntDestinationIndex = -1;
    }

    void Update()
    {
        #region Animations Setup
        if (ReachedDestination())
        {
            if(animator.GetFloat("Locomotion") > 0.01f)
            {
                animator.SetFloat("Locomotion", Mathf.Lerp(animator.GetFloat("Locomotion"), 0, Time.deltaTime * 2));
            }
            else
            {
                animator.SetFloat("Locomotion", 0);
            }
        }
        else
        {
            if (animator.GetFloat("Locomotion") < 1)
            {
                animator.SetFloat("Locomotion", Mathf.Lerp(animator.GetFloat("Locomotion"), 1, Time.deltaTime * 2));
            }
            else
            {
                animator.SetFloat("Locomotion", 1);
            }
        }
        #endregion

        #region Route System
        if (Route != null && FollowRoute && Route.childCount > 0)
        {
            if (ReachedDestination())
            {
                if((CurruntDestinationIndex + 1) < Route.childCount)
                {
                    CurruntDestinationIndex++;
                    agent.SetDestination(Route.GetChild(CurruntDestinationIndex).transform.position);
                }
                else
                {
                    if (Loop)
                    {
                        CurruntDestinationIndex = 0;
                        agent.SetDestination(Route.GetChild(CurruntDestinationIndex).transform.position);
                    }
                    else
                    {
                        CurruntDestinationIndex = -1;
                        FollowRoute = false;
                    }
                }
            }
        }
        #endregion
    }

    public void ResetDestinationIndex() => CurruntDestinationIndex = -1;
    bool ReachedDestination() => agent.remainingDistance < agent.stoppingDistance + 0.5f;
}