using UnityEngine;
using UnityEngine.AI;

public class NPCFollower : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    public float detectionDistance = 6f;
    public float followDistance = 2f;

    public float wanderSpeed = 2.5f;
    public float followSpeed = 8f;

    private bool isFollowing = false;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent NOT FOUND");
        }

        // start in wander mode
        agent.speed = wanderSpeed;
    }

    void Update()
    {
        if (player == null || agent == null) return;

        float distance = Vector3.Distance(agent.transform.position, player.position);

        // 🔵 ENTER FOLLOW MODE
        if (!isFollowing && distance <= detectionDistance)
        {
            isFollowing = true;
            agent.speed = followSpeed;   // match player speed here
        }

        if (isFollowing)
        {
            if (distance > followDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
    }
}