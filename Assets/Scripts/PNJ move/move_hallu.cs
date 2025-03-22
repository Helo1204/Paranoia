using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class move_hallu : MonoBehaviour
{
    [SerializeField] float speed;
    int longueurRue = 50;
    Vector3 destinationPosition;
    [SerializeField] NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destinationPosition = transform.position +new Vector3(1,0,0)*longueurRue;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destinationPosition);
        if (transform.position.x>longueurRue)
        {
            Destroy(this.gameObject);
        }
    }

}
