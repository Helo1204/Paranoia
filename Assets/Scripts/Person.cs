using System.Collections;
using UnityEngine;

public class Person : MonoBehaviour
{
    private DangerSystem dangerSystem;
    private new Renderer renderer;

    protected MaterialPropertyBlock materialPropertyBlock;

    public float StabDistance = 2f;
    public GameObject HoldingObject;
    public int DangerMaterialIndex;
    public bool IsMurder;
    [SerializeField] Animator animator;

    public void Start()
    {
        dangerSystem = DangerSystem.Main;
        renderer = GetComponent<Renderer>();

        materialPropertyBlock = new();
    }

    public void Update()
    {
        UpdateDangerValue(dangerSystem.CalculateDanger(transform.position));

        if (!IsMurder)
        {
            return;
        }

        bool foundTarget = Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, StabDistance, 1 << LayerMask.NameToLayer("Player"));
        Debug.Log(foundTarget);
        if (foundTarget)
        {
            animator.SetTrigger("stab");
           StartCoroutine( StabPlayer());
        }
    }

    private void UpdateDangerValue(float danger)
    {
        materialPropertyBlock.SetColor("_Color", new(1f, 1f - danger, 0));
        renderer.SetPropertyBlock(materialPropertyBlock, DangerMaterialIndex);
    }

    private  IEnumerator StabPlayer()
    {
        yield return new WaitForSeconds(2);
        LevelManager.Main.GameOver();
    }
}
