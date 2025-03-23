using UnityEngine;
using UnityEngine.AI;

public class move_hallu : MonoBehaviour
{
    [SerializeField] float speed;
    
    private Vector3 destinationPosition;
    private NavMeshAgent agent;
    private Person person;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        person = GetComponent<Person>();

        destinationPosition = EnemySpawner.Main.PlayerSpawnTransform.position;
        destinationPosition.z = transform.position.z;

        agent.speed = speed;
        agent.SetDestination(destinationPosition);
    }

    private void Update()
    {
        if (person.CantInteract())
        {
            return;
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
