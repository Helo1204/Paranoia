using UnityEngine;

public class Person : MonoBehaviour
{
    private DangerSystem dangerSystem;
    private new Renderer renderer;

    protected MaterialPropertyBlock materialPropertyBlock;

    public GameObject HoldingObject;
    public int DangerMaterialIndex;

    public void Start()
    {
        dangerSystem = DangerSystem.Main;
        renderer = GetComponent<Renderer>();

        materialPropertyBlock = new();
    }

    public void Update()
    {
        UpdateDangerValue(dangerSystem.CalculateDanger(transform.position));
    }

    private void UpdateDangerValue(float danger)
    {
        materialPropertyBlock.SetColor("_Color", new(1f, 1f - danger, 0));
        renderer.SetPropertyBlock(materialPropertyBlock, DangerMaterialIndex);
    }
}
