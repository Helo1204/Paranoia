using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject hallucination;
    [SerializeField] GameObject deviant;
    float roadRadius;
    float enemyRatio; //entre 0 et 1

    Action currentState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
