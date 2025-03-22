using UnityEngine;
using System.Collections;


public class LevelManager : MonoBehaviour
{
    public ParanoiaBar ParanoiaBar;
    public float CrowdFactor = 1f;
    public float BaseFactor = 1f;
    public float Factor = 1f;
    public float Difficulty = 1;
    public float radius=10;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = DangerSystem.Main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFactor(CountEnnemy(player.transform.position, radius));
        ParanoiaBar.AddParanoia(Time.deltaTime*Factor*Difficulty);
    }

    public int CountEnnemy(Vector3 center, float radius)
    {
        return  Physics.OverlapSphere(center, radius, LayerMask.NameToLayer("Ennemy") ).Length;
    }

    public void UpdateFactor(int numberEnnemy)
    {
        Factor = CrowdFactor*numberEnnemy + BaseFactor;
        //Debug.Log(numberEnnemy);
    }
}
