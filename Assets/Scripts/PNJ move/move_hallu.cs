using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class move_hallu : MonoBehaviour
{
    [SerializeField] float speed;
    
    private Vector3 destinationFinal;
    private NavMeshAgent agent;
    private Person person;
    private bool wantTalk;
    private GameObject player;
    private Vector3 offset;


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


        if (agent.remainingDistance <= agent.stoppingDistance)
        {   

            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Destroy(gameObject);
            }
        }


}
