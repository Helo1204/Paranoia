using UnityEngine;
using UnityEngine.AI;

public class move_hallu : MonoBehaviour
{
    [SerializeField] float speed;
    
    private Vector3 destinationFinal;
    private NavMeshAgent agent;
    private Person person;
    private GameObject player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        person = GetComponent<Person>();
        player = DangerSystem.Main.gameObject;

        destinationFinal = EnemySpawner.Main.PlayerSpawnTransform.position;
        destinationFinal.z = transform.position.z;

        agent.speed = speed;
        agent.SetDestination(destinationFinal);
    }

    private void Update()
    {
        if (person.CantInteract())
        {
            return;
        }
        agent.SetDestination(destinationFinal);

        if (transform.position.x + LevelManager.Main.DestroyDistance < player.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
