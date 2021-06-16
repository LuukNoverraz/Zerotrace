using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class GuardController : MonoBehaviour 
{
    public NavMeshAgent agent;
    public Transform[] points;
    public bool chasing = false;
    public float lookRadius = 1f;
    Transform target;
    private int destPoint = 0;


    void Start ()
    {
        target = PlayerManager.instance.player.transform;
        GoToNextPoint();
    }


    void GoToNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
        {
            return;
        }
        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update ()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !chasing)
        {
            GoToNextPoint();
        }

        float distance = Vector3.Distance(target.position, transform.position);

        // if (distance >= lookRadius)
        // {
        //     chasing = false;
        // }

        if (distance <= lookRadius)
        {
            Debug.Log("Shkoinky");
            chasing = true;
        }

        if (chasing)
        {
            Debug.Log("Look Radius : " + lookRadius);
            Debug.Log("FOUND");
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                // Attack
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}