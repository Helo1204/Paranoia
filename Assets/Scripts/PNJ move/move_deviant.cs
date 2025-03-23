using UnityEngine;
using UnityEngine.AI;

public class move_deviant : MonoBehaviour
{
    private GameObject player;
    private Vector3 destinationPosition;
    private NavMeshAgent agent;
    private Person person;

    [SerializeField] float distanceDetection = 10f;
    [SerializeField] float speed;
    [SerializeField] Animator animator;
    [SerializeField] int acceleration = 4;
    private float squareDistance;

    [SerializeField] AudioClip spotCry;
    public bool playerSpotted = false;
    public bool hasScreamed = false;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        person = GetComponent<Person>();

        destinationPosition = EnemySpawner.Main.PlayerSpawnTransform.position;
        destinationPosition.z = transform.position.z;

        player = DangerSystem.Main.gameObject;
        agent.speed = speed;
        squareDistance = Mathf.Pow(distanceDetection, 2);
        agent.SetDestination(destinationPosition);
    }

    private void Update()
    {
        if (person.CantInteract())
        {
            return;
        }

        float dist = Vector3.SqrMagnitude(player.transform.position - transform.position);
        if (dist < squareDistance)
        {
            playerSpotted = true;
            agent.SetDestination(player.transform.position);
            agent.speed = speed + acceleration;
            animator.SetTrigger("run");
        }
        else
        {
            agent.SetDestination(destinationPosition);
        }

        if (transform.position.x + LevelManager.Main.DestroyDistance < player.transform.position.x)
        {
            Destroy(gameObject);
        }

        if (!hasScreamed && playerSpotted)
        {
            AudioSource laudioSource = gameObject.AddComponent<AudioSource>();
            laudioSource.clip = spotCry;
            laudioSource.Play();
            Destroy(laudioSource, laudioSource.clip.length);
            hasScreamed = true; 
        }
    }
}
