using UnityEngine;
using UnityEngine.AI;

public class move_hallu : MonoBehaviour
{
    [SerializeField] float speed;
    
    Vector3 destinationPosition;
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        destinationPosition = EnemySpawner.Main.PlayerSpawnTransform.position;
        destinationPosition.z = transform.position.z;

        agent.speed = speed;
        agent.SetDestination(destinationPosition);
    }

    void Update()
    {
        agent.SetDestination(destinationPosition);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    

}
