using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text infoText;
    public GameObject settingButtons;
    public Button playButton;

    void OnEnable()
    {
        infoText.enabled = true;
        settingButtons.SetActive(true);
    }

    void OnDisable()
    {
        infoText.enabled = false;
        settingButtons.SetActive(false);
    }

    public void DisableMenuButtons()
    {
        settingButtons.SetActive(false);
    }

    public void MainMenuStartAnimation()
    {
        infoText.text = "Level " + PlayerPrefs.GetInt("Level");
    }
}