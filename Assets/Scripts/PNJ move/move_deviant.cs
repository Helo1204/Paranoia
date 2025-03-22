using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class move_deviant : MonoBehaviour
{
    GameObject player;
    int longueurRue = 100;
    Vector3 destinationPosition;
    float distanceDetection = 15f;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float speed;
    private float squareDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destinationPosition = transform.position +new Vector3(1,0,0)*longueurRue;
        player = DangerSystem.Main.gameObject;
        agent.speed = speed;
        squareDistance = Mathf.Pow(distanceDetection, 2);
        agent.SetDestination(destinationPosition);
    }

    // Update is called once per frame
    void Update()

    {
        float dist = Vector3.SqrMagnitude(player.transform.position - transform.position);
        if (dist < squareDistance)
        {
            agent.SetDestination(player.transform.position);
            agent.speed = speed + 7;

        }
        else
        {
            agent.SetDestination(destinationPosition);
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Destroy(this.gameObject);
            }
        }


    }

}
