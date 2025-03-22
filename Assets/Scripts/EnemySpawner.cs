using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject hallucination;
    [SerializeField] GameObject deviant;
    float roadRadius;
    float hallucinationRatio; //entre 0 et 1
    float affluence;
    float time;

    Action currentState;
    void Start()
    {
        currentState = Reload;
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Invoke();
    }
    
    void Spawn()
    {

        Vector3 lRandomPosition = new Vector3 (0,0, UnityEngine.Random.Range(-1f,1f) * roadRadius);
        float lRoll = UnityEngine.Random.Range(0f, 1f);
        if (lRoll < hallucinationRatio)
        {
            Instantiate(hallucination, lRandomPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(deviant, lRandomPosition, Quaternion.identity);
        }
        time = 0;
        currentState = Reload;
    }

    void Reload()
    {
        time += Time.deltaTime;
        if (time > affluence)
        {
            currentState = Spawn;
        }
    }
}
