using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StabSystem : MonoBehaviour
{
    public static StabSystem Main;
    
    private PlayerController playerController;

    public float StabDistance = 5f;
    public Transform CameraTransform;
    public Animator animator;
    public Image CursorImage;

    private float SqrStabDistance;

    private float latestStabTime;
    public float StabInterval = 1f;

    [SerializeField] AudioClip stabSound;
    [SerializeField] AudioClip stabHitSound;
    public void Awake()
    {
        if (Main)
        {
            Debug.LogError($"StabSystem.Main already exists, deleting the current one on {name}");
            Destroy(this);
            return;
        }

        Main = this;
        playerController = GetComponent<PlayerController>();

        SqrStabDistance = Mathf.Pow(StabDistance, 2);
    }

    public bool IsInStabRange(Transform otherTransform)
    {
        return Vector3.SqrMagnitude(otherTransform.position - transform.position) < SqrStabDistance;
    }

    public void Update()
    {
        if (!playerController.ControlsEnabled)
        {
            return;
        }

        bool foundTarget = Physics.Raycast(CameraTransform.position, CameraTransform.forward, out RaycastHit hit, StabDistance, 1 << LayerMask.NameToLayer("Enemy"), QueryTriggerInteraction.Collide);
        CursorImage.color = foundTarget ? Color.red : Color.white;

        if (Input.GetMouseButtonDown(0) && Time.time > latestStabTime + StabInterval)
        {
            latestStabTime = Time.time;
            animator.SetTrigger("Stab");
            PlayStabSound(foundTarget);
            if (foundTarget)
            {
                StabTarget(hit.transform, hit.point, hit.normal);
            }
        }
    }

    public void StabTarget(Transform transform, Vector3 point, Vector3 normal)
    {
        transform.GetComponent<Person>().IsDead = true;
        transform.GetComponent<Animator>().SetTrigger("die");
        transform.GetComponent<CapsuleCollider>().isTrigger = true;
        if (0.1>Random.Range(0f, 1.0f))
        {
            playerController.AddDrug(1);
        }
        Destroy(transform.GetComponent<NavMeshAgent>());

        ParticleSystem particleSystem = transform.GetComponent<ParticleSystem>();
        particleSystem.Play();

        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.position = transform.InverseTransformPoint(point);
        
        Vector3 localNormal = transform.InverseTransformDirection(normal);
        Quaternion rotation = Quaternion.LookRotation(localNormal);
        shapeModule.rotation = rotation.eulerAngles;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(CameraTransform.position, CameraTransform.forward * StabDistance);
    }

    void PlayStabSound(bool foundtarget)
    {
        AudioSource laudiosource = gameObject.AddComponent<AudioSource>();
        if (foundtarget)
        { laudiosource.clip = stabHitSound; }
        else { laudiosource.clip = stabSound; }
        laudiosource.Play();
        Destroy(laudiosource, laudiosource.clip.length);
    }
}
