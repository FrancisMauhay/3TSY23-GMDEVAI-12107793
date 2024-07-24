using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    private Transform playerTarget;
    public NavMeshAgent agent;
    private float timer;
    public bool playerDetected = false;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDetected)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
        else
        {
            Vector3 targetLook = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
            Vector3 direction = targetLook - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            if (Vector3.Distance(targetLook, transform.position) > 3)
            {
                agent.SetDestination(playerTarget.position);
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public void StartFollowing(Transform playerTransform)
    {
        playerTarget = playerTransform;
        playerDetected = true;
        agent.speed = movementSpeed;
    }

    public void StopFollowing()
    {
        playerTarget = null;
        playerDetected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone.");
            StartFollowing(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone.");
            StopFollowing();
        }
    }
}

