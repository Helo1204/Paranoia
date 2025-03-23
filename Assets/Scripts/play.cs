using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public GameObject InstructionsPanel;
    public GameObject MainMenu;

    public void OnPlay()
    {
        SceneManager.LoadScene("Main scene");
    }
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnInstructions()
    {
        ToggleMenu(false);
    }

    public void ToggleMenu(bool menuShown)
    {
        InstructionsPanel.SetActive(!menuShown);
        MainMenu.SetActive(menuShown);
    }
}
