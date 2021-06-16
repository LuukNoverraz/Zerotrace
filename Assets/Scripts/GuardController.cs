using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class GuardController : MonoBehaviour 
{
    public NavMeshAgent agent;
    public Transform[] points;
    public bool chasing = false;
    public float lookRadius = 1f;
    public Transform target;
    private int destPoint = 0;


    void Start ()
    {
        GoToNextPoint();
    }


    void GoToNextPoint()
    {
        // Set the agent to go to the currently selected destination
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update ()
    {
        // Patrol if the agent has gotten near to one of the patrol points and is not in chase mode
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !chasing)
        {
            Debug.Log("Patrolling");
            GoToNextPoint();
        }

        // Get distance from player to guard, changing the chasing boolean based on value compared to looking radius of guard
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            chasing = true;
        }

        if (chasing)
        {
            // Debug.Log("Chasing");
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                // Attack code can be put here, for now the guard only looks at the player
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        Debug.Log(chasing);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}