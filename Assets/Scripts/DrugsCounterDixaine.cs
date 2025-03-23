using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Unity.VisualScripting;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DrugsCounterDixaine : MonoBehaviour
{
    private int Counter = 3;
    private int Max = 99;
    private int Dix;
    private int[] tableau;
    [SerializeField] List<Sprite> nombres;
    private Image image;

    public GameObject player;
    private PlayerController player_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dix = 0;
        image = GetComponent<Image>();
        player_script = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Counter = player_script.DrugsAmmount;
        Counter = Mathf.Clamp(Counter, 0, Max);
        Dix = Dixaine(Counter);
        image.sprite = nombres[Dix];
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

    private int Dixaine(int Counter)
    {
        return (int)Mathf.Floor(Counter / 10f) % 10;
    }

}
