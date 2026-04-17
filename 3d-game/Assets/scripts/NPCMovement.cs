using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float waitTime = 2f;
    private float timer;
    [SerializeField] private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        timer = waitTime;

        // IMPORTANT: ensures NPC is on NavMesh
        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("NPC spawned off NavMesh!");
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            MoveToRandomPoint();
            timer = waitTime;
        }
    }

    void MoveToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}