using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // list of panels
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject quitPanel;
    public GameObject controlPanel;
    public GameObject audioPanel;
    public GameObject pauseMenuPanel;

    [Header("Scene")]
    public string loadScene;

    public static bool GameIsPaused = false;

    // hides the panels when clicking between buttons
    public void HideAllPanels()
    {
        if (mainMenuPanel) mainMenuPanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (quitPanel) quitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        HideAllPanels();
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(loadScene);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void openMainMenuPanel()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void openQuitPanel()
    {
        HideAllPanels();
        quitPanel.SetActive(true);
    }

    public void openSettingsPanel()
    {
        HideAllPanels();
        settingsPanel.SetActive(true);
    }

    public void openControlPanel()
    {
        audioPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void openAudioPanel()
    {
        controlPanel.SetActive(false);
        audioPanel.SetActive(true);
    }

}
