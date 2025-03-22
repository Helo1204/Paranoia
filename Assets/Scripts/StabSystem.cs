using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StabSystem : MonoBehaviour
{
    public static StabSystem Main;
    
    public float StabDistance = 5f;
    public Transform CameraTransform;
    public Animator animator;
    public Image CursorImage;

    private float SqrStabDistance;

    private float latestStabTime;
    public const float STAB_INTERVAL = 1f;

    public void Awake()
    {
        if (Main)
        {
            Debug.LogError($"StabSystem.Main already exists, deleting the current one on {name}");
            Destroy(this);
            return;
        }

        Main = this;

        SqrStabDistance = Mathf.Pow(StabDistance, 2);
    }

    public bool IsInStabRange(Transform otherTransform)
    {
        return Vector3.SqrMagnitude(otherTransform.position - transform.position) < SqrStabDistance;
    }

    public void Update()
    {
        bool foundTarget = Physics.Raycast(CameraTransform.position, CameraTransform.forward, out RaycastHit hit, StabDistance, 1 << LayerMask.NameToLayer("Ennemy"));
        CursorImage.color = foundTarget ? Color.red : Color.white;

        if (Input.GetMouseButtonDown(0) && Time.time > latestStabTime + STAB_INTERVAL)
        {
            latestStabTime = Time.time;
            animator.SetTrigger("Stab");
            if (foundTarget)
            {
                StabTarget(hit.transform, hit.point, hit.normal);
            }
        }
    }

    public void StabTarget(Transform transform, Vector3 point, Vector3 normal)
    {
        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        ParticleSystem particleSystem = transform.GetComponent<ParticleSystem>();
        particleSystem.Play();

        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.position = transform.InverseTransformPoint(point);
    }
}
