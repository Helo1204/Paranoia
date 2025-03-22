using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public ParanoiaBar ParanoiaBar;
    public float CrowdFactor = 0.5f;
    public float BaseFactor = 0.5f;
    public float Factor = 1f;
    public float Difficulty = 1;
    public float radius=10;
    [SerializeField] GameObject gameOver;
    GameObject player;

    public static LevelManager Main;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        if (Main)
        {
            Debug.LogError($"StabSystem.Main already exists, deleting the current one on {name}");
            Destroy(this);
            return;
        }

        Main = this;
    }
    void Start()
    {
        player = DangerSystem.Main.gameObject;
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFactor(CountEnnemy(player.transform.position, radius));
        ParanoiaBar.AddParanoia(Time.deltaTime*Factor*Difficulty);
    }

    public int CountEnnemy(Vector3 center, float radius)
    {
        return  Physics.OverlapSphere(center, radius, 1<<LayerMask.NameToLayer("Ennemy")).Length;
    }

    public void UpdateFactor(int numberEnnemy)
    {
        Factor = CrowdFactor*numberEnnemy + BaseFactor;
        //Debug.Log(numberEnnemy);
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        player.GetComponent<PlayerController>().SetControlsEnabled(false);
        Time.timeScale = 0;
    }
    public void restart()
    {
        SceneManager.LoadScene("Main scene");
        Time.timeScale = 1;
    }
    public void quit()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
