using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        //float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;

        //fill.color = color;
    }

    public void ResetParanoia()
    {
        current = 0f;
    }

    public void AddParanoia(float amount)
    {
        current += amount;
    }
}
