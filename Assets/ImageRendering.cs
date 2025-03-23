using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ImageRendering : MonoBehaviour
{
    public GameObject player;
    private PlayerController player_script;
    [SerializeField] List<Sprite> nombres;
    private Image image;
    private int Counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        player_script = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Counter = player_script.InnocentKilled;
        image.sprite = nombres[Counter];
    }
}
