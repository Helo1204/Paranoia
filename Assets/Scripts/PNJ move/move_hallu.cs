using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class move_hallu : MonoBehaviour
{
    [SerializeField] float speed;
    int longeurRue = 50;
    Vector3 destinationPosition;
    [SerializeField] NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destinationPosition = transform.position +new Vector3(1,0,0)*longeurRue;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destinationPosition);
        if(Vector3.Distance(destinationPosition,transform.position)<0.5f)
        {
            Destroy(this.gameObject);
        }
    }

}
