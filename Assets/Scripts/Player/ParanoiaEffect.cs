using UnityEngine;

public class ParanoiaEffect : MonoBehaviour
{
    public ParanoiaBar ParanoiaBar;
    public float ParanoiaAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParanoiaAmount = ParanoiaBar.GetCurrentFill();
    }
}
