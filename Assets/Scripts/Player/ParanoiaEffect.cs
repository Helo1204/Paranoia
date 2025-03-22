using UnityEngine;

public class ParanoiaEffect : MonoBehaviour
{
    public ParanoiaBar ParanoiaBar;
    float ParanoiaAmount;
    public Camera PlayerCamera;
    float InitialFOV = 90f;
    public float MaxFOV = 100f;
    public float MinFOV = 60f;

    float MaxParanoia;
    float MinParanoia;

    float Max_Fog = 0.07f;
    float Min_Fog = 0.015f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MinParanoia = ParanoiaBar.minimum;
        MaxParanoia = ParanoiaBar.maximum;
        EnableFog();

    }

    // Update is called once per frame
    void Update()
    {
        ParanoiaAmount = ParanoiaBar.GetCurrentFill();

        PlayerCamera.fieldOfView = FOVfunction(ParanoiaAmount);
        SetFog(FogFunction(ParanoiaAmount));
    }

    void ResetFOV()
    {
        PlayerCamera.fieldOfView = InitialFOV;
    }

    private float FOVfunction(float ParanoiaAmount)
    {
        float percentile = (ParanoiaAmount - MinParanoia) / (MaxParanoia-MinParanoia);
        return (percentile*MinFOV)+((1-percentile)*MaxFOV);
    }

    void EnableFog()
    {
        RenderSettings.fog = true;
    }
    void ResetFog()
    {
        RenderSettings.fogDensity = Min_Fog;
    }

    private float FogFunction(float ParanoiaAmount)
    {
        float percentile = (ParanoiaAmount - MinParanoia) / (MaxParanoia - MinParanoia);
        return (percentile * Max_Fog) + ((1 - percentile) * Min_Fog);
    }    
    void SetFog(float fogAmount)
    {
        RenderSettings.fogDensity = fogAmount;
        
    }



}
