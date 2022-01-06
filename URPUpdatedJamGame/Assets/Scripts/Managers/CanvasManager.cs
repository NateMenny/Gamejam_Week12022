using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button quitButton;
    public Button settingsButton;
    public Button backButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    public Text livesText;
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;

    [Header("Images")]
    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(() => GameManager.instance.StartGame());

        if (quitButton)
            quitButton.onClick.AddListener(() => GameManager.instance.QuitGame());

        if (settingsButton)
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());

        if (backButton)
            backButton.onClick.AddListener(() => ShowMainMenu());

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(() => ReturnToGame());

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => GameManager.instance.ReturnToTitle());

    }

    public void SetLivesText(int livesValue)
    {

        //livesText.text = livesValue.ToString();
        /*
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < livesValue)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
        */
    }
    void ReturnToGame()
    {
        pauseMenu.SetActive(false);
    }

    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);


                if (pauseMenu.activeSelf)
                {
                    GameManager.instance.PauseGame();
                }
                else
                {
                    GameManager.instance.ResumeGame();
                }
            }
        }

        if (settingsMenu)
        {
            if (settingsMenu.activeSelf)
            {
                volSliderText.text = volSlider.value.ToString();
            }
        }
    }
}