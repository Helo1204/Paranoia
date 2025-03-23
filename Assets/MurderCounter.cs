using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MurdererCounter : MonoBehaviour
{
    public GameObject player;
    private TMP_Text text;
    private PlayerController player_script;
    private int Counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_script = player.GetComponent<PlayerController>();
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Counter = player_script.MurdererKilled;
        text.text = "Murderer Killed : " + Counter;
    }
}
