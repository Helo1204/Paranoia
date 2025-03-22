using UnityEngine;
using UnityEngine.AI;

public class move_deviant : MonoBehaviour
{
    GameObject player;

    Vector3 destinationPosition;
    [SerializeField] float distanceDetection = 10f;
    private NavMeshAgent agent;
    [SerializeField] float speed;
    [SerializeField] Animator animator;
    private float squareDistance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        destinationPosition = EnemySpawner.Main.PlayerSpawnTransform.position;
        destinationPosition.z = transform.position.z;

        player = DangerSystem.Main.gameObject;
        agent.speed = speed;
        squareDistance = Mathf.Pow(distanceDetection, 2);
        agent.SetDestination(destinationPosition);
    }

    private void Update()
    {
        float dist = Vector3.SqrMagnitude(player.transform.position - transform.position);
        if (dist < squareDistance)
        {
            agent.SetDestination(player.transform.position);
            agent.speed = speed + 4;
            animator.SetTrigger("run");
        }
        else
        {
            agent.SetDestination(destinationPosition);
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
