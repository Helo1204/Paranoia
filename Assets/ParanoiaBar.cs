using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[ExecuteInEditMode()]
public class ParanoiaBar : MonoBehaviour
{
    public int maximum;
    public int minimum;
    public float current;
    public Image mask;
    //public Image fill;
    //public Color color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentFill();
    }

    void UpdateCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        
        //float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;

        //fill.color = color;
    }
    public float GetCurrentFill()
    {
        return current;
    }

    public void ResetParanoia()
    {
        current = 0f;
        
    }

    public void AddParanoia(float amount)
    {
        current = Mathf.Clamp(current + amount, minimum, maximum); //floor et ceilamount;
    }

    

}
