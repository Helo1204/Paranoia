using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class move_hallu : MonoBehaviour
{
    [SerializeField] float speed;
    
    private Vector3 destinationPosition;
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
        destinationPosition = destinationFinal;

        agent.speed = speed;
        agent.SetDestination(destinationFinal);
        if(Random.Range(0, 100) < LevelManager.Main.Difficulty * 10) { wantTalk = true; }

        offset = new Vector3(-2,0,0);
        destinationPosition = player.transform.position+offset ;
        if (wantTalk)
        {
            destinationPosition = player.transform.position + offset;
        }
    }
    private void Update()
    {
        
        if (person.CantInteract())
        {
            return;
        }
        agent.SetDestination(destinationPosition);
        if (wantTalk)
        {
            Debug.Log("talk");
            destinationPosition = player.transform.position + offset;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {   
            if(wantTalk)
            {
                StartCoroutine(Talk());
            }
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Destroy(gameObject);
            }
        }

    }
    private IEnumerator Talk()
    {
        yield return new WaitForSeconds(2);
        wantTalk= false;
        destinationPosition = destinationFinal;
    }
}
