using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public static MenuSystem Main;

    public Button ResumeButton;
    public Slider SensivitySlider;
    public Slider AudioSlider;
    public GameObject MenuPanel;
    public AudioListener AudioListener;
    public PlayerController PlayerController;
    public TMP_Text AudioText;
    public TMP_Text SensivityText;

    private Action closeCallback;
    public bool Visible;

    void Start()
    {
        if (Main)
        {
            Debug.LogError($"MenuSystem.Main already exists, deleting the current one on {name}");
            Destroy(this);
            return;
        }

        Main = this;

        ResumeButton.onClick.AddListener(Hide);
        SensivitySlider.onValueChanged.AddListener(ChangeSensivity);
        AudioSlider.onValueChanged.AddListener(ChangeAudio);

        Hide();
    }

    public void ChangeSensivity(float sensivity)
    {
        PlayerController.Sensivity = sensivity;
        SensivityText.text = sensivity.ToString("0.000");
    }

    public void ChangeAudio(float value)
    {
        AudioListener.volume = value;
        AudioText.text = value.ToString("0.0");
    }

    public void Hide()
    {
        MenuPanel.SetActive(false);
        Visible = false;
        closeCallback?.Invoke();
        closeCallback = null;
    }

    public void Show(Action callback)
    {
        MenuPanel.SetActive(true);
        Visible = true;
        closeCallback = callback;
    }
}
