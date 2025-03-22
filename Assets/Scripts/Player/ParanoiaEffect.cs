using UnityEngine;

public class ParanoiaEffect : MonoBehaviour
{
    public ParanoiaBar ParanoiaBar;
    float ParanoiaAmount;
    public Camera PlayerCamera;
    float InitialFOV = 90f;
    public float MaxFOV = 100f;
    public float MinFOV = 70f;

    float MaxParanoia;
    float MinParanoia;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MinParanoia = ParanoiaBar.minimum;
        MaxParanoia = ParanoiaBar.maximum;

    }

    // Update is called once per frame
    void Update()
    {
        ParanoiaAmount = ParanoiaBar.GetCurrentFill();
        
        PlayerCamera.fieldOfView = FOVfunction(ParanoiaAmount);
    }

    void ResetFOV()
    {
        PlayerCamera.fieldOfView = InitialFOV;
    }

    private float FOVfunction(float ParanoiaAmount)
    {
        float percentile = ParanoiaAmount / (MaxParanoia-MinParanoia);
        return (percentile*MinFOV)+((1-percentile)*MaxFOV);
    }
}
