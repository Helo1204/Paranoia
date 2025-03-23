using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements.Experimental;

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

    public AudioSource Src;
    public AudioClip SfxHeartBeat;
    private float LatestHeartBeat;
    private float HeartBeatFrequency = 1f;
    private float Pitch;
    public float MaxHeartBeat = 2f;
    public float MinHeartBeat = 1;
    public float HeartBeatFactor = 1f;
    public float HeartBeatDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MinParanoia = ParanoiaBar.minimum;
        MaxParanoia = ParanoiaBar.maximum;
        EnableFog();
        Pitch = Src.pitch;
        LatestHeartBeat = Time.time;
        StartCoroutine(TriggerHeartBeatCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        ParanoiaAmount = ParanoiaBar.GetCurrentFill();

        PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, FOVfunction(ParanoiaAmount), Time.deltaTime);
        //PlayerCamera.fieldOfView = FOVfunction(ParanoiaAmount);
        SetFog(Mathf.Lerp(RenderSettings.fogDensity, FogFunction(ParanoiaAmount), Time.deltaTime));

        HeartBeatFrequency = HeartBeatFunction(ParanoiaAmount);
        HeartBeatFactor = HeartBeatFrequency / MinHeartBeat;
        
        HeartBeatDelay = 1 / HeartBeatFrequency;
        //Debug.Log(HeartBeatFrequency);
        if (Time.time > LatestHeartBeat + HeartBeatDelay)
        {
            //Debug.Log("HeartBeating");
            LatestHeartBeat = Time.time;
            Pitch = HeartBeatFactor;
            //PlayHeartBeat();
        }
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

    public void PlayHeartBeat()
    {
        Src.clip = SfxHeartBeat;
        Src.volume = Pitch*0.2f;
        Src.pitch = Pitch*0.6f;
        Src.Play();
    }

    public IEnumerator TriggerHeartBeatCoroutine()
    {
        float battementDelay = 0.4f;
        float timeBegin;
        float FOV;
        while (true)
        {
            //Effet Visuel
            timeBegin = Time.time;
            FOV = PlayerCamera.fieldOfView;
            while (Time.time < timeBegin + battementDelay)
            {
                PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, FOV-20, Time.deltaTime);
                yield return null;
            }
            PlayHeartBeat();
            yield return new WaitForSeconds(battementDelay);
            //Effet Visuel
            timeBegin = Time.time;
            FOV = PlayerCamera.fieldOfView;
            while (Time.time < timeBegin + battementDelay)
            {
                PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, FOV , Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(HeartBeatDelay- battementDelay);

        }
    }

    public float HeartBeatFunction(float ParanoiaAmount)
    {
        float percentile = (ParanoiaAmount - MinParanoia) / (MaxParanoia - MinParanoia);
        return (percentile * MaxHeartBeat) + ((1 - percentile) * MinHeartBeat);
    }


}
