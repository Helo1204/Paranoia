using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using UnityEngine.Rendering;

public class DrugsCounterUnit : MonoBehaviour
{
    private int Counter = 99;
    private int Max = 99;
    private int Unit;
    private int[] tableau;
    [SerializeField] List<Sprite> nombres;
    private Image image;

    public GameObject player;
    private PlayerController player_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Unit = 0;
        image = GetComponent<Image>();
        player_script = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Counter = player_script.DrugsAmount;
        Counter = Mathf.Clamp(Counter, 0, Max);
        Unit = Unitaine(Counter);
        image.sprite = nombres[Unit];

    }
    

    private int[] Sorting(int Counter)
    {
        int[] digits;
        int digits_length = 0;
        float n = Counter;
        while (n > 9)
        {
            n = Mathf.Floor(n / 10f);
            digits_length += 1;
        }

        digits = new int[digits_length];
        int m = Counter;
        for (int i = 0; i < digits_length; i++)
        {
            digits[i] = m%10;
            m /= 10;
            
        }
        
        return digits.Reverse().ToArray();
    }

    private int Unitaine(int Counter)
    {
        return (int)Mathf.Floor(Counter ) % 10;
    }

}
