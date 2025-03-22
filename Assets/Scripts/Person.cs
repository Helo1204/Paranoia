using UnityEngine;

public class Person : MonoBehaviour
{
    private DangerSystem dangerSystem;
    private Renderer renderer;
    private Shader shader;
    protected MaterialPropertyBlock materialPropertyBlock;

    public GameObject HoldingObject;
    public int DangerMaterialIndex;
    public float danger;

    public void Awake()
    {
        dangerSystem = DangerSystem.Main;
        renderer = GetComponent<Renderer>();
        shader = renderer.materials[DangerMaterialIndex].shader;
        //renderer.material.CloneViaFakeSerialization();

        materialPropertyBlock = new();
    }

    public void Update()
    {
        danger = dangerSystem.CalculateDanger(transform.position);
        UpdateDangerValue(danger);
    }

    private void UpdateDangerValue(float danger)
    {
        materialPropertyBlock.SetColor("Color", new(255, (1 - danger) * 255, 0));
        renderer.SetPropertyBlock(materialPropertyBlock, DangerMaterialIndex);
    }
}
