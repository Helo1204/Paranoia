using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class move_deviant : MonoBehaviour
{
    GameObject player;
    int longeurRue = 50;
    Vector3 destinationPosition;
    float distanceDetection = 15f;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destinationPosition = transform.position +new Vector3(1,0,0)*longeurRue;
        player = DangerSystem.Main.gameObject;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < distanceDetection)
        {
            agent.SetDestination(player.transform.position);
            agent.speed = speed + 4;

        }
        else
        {
            agent.SetDestination(destinationPosition);
        }


        
        if(Vector3.Distance(destinationPosition,transform.position)<0.5f)
        {
            Destroy(this.gameObject);
        }

    }

}
