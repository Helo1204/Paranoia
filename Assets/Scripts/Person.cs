using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    private DangerSystem dangerSystem;
    private PlayerController playerController;
    private new Renderer renderer;

    private MaterialPropertyBlock materialPropertyBlock;

    public Material dangerMaterial;

    public float StabDistance = 2f;
    public GameObject HoldingObject;
    public int DangerMaterialIndex;
    public bool IsMurder;
    public Vector3 RaycastOffset;
    public bool IsDead;
    
    private Animator animator;
    private bool isKilling;
    
    public void Start()
    {
        dangerSystem = DangerSystem.Main;
        playerController = dangerSystem.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        materialPropertyBlock = new();
        materialPropertyBlock.SetColor("_Color", Color.white);
        materialPropertyBlock.SetFloat("_Scale", 1.1f);
    }

    public void Update()
    {
        UpdateDangerValue(dangerSystem.CalculateDanger(transform.position));

        if (!IsMurder || CantInteract())
        {
            return;
        }


        bool foundTarget = Physics.Raycast(transform.position + RaycastOffset, transform.forward, StabDistance, 1 << LayerMask.NameToLayer("Player"));


        if (foundTarget)
        {
            StartCoroutine(StabPlayer());
        }
    }

    private void UpdateDangerValue(float danger)
    {
        materialPropertyBlock.SetColor("_Color", new(1f, 1f - danger, 0));
        renderer.SetPropertyBlock(materialPropertyBlock, DangerMaterialIndex);
    }

    private IEnumerator StabPlayer()
    {
        isKilling = true;
        Destroy(GetComponent<NavMeshAgent>());
        playerController.SetControlsEnabled(false);
        Vector3 direction = transform.position + new Vector3(0, 3f, 0) - playerController.CameraTransform.position;

        Vector3 newAngles = Quaternion.LookRotation(direction).eulerAngles;

        float timeBegin = Time.time;
        while (Time.time < timeBegin + 1f)
        {
            playerController.SlowlyTurnTo(newAngles, Time.deltaTime * 5);
            yield return null;
        }

        animator.SetTrigger("stab");
        yield return new WaitForSeconds(2f);
        LevelManager.Main.GameOver();
    }

    public bool CantInteract()
    {
        return IsDead || isKilling;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position + RaycastOffset, transform.forward);
    }
}
