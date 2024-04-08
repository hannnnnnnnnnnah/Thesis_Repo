using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject quitScreen;

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Continue()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        gameObject.SetActive(false);
    }

    public void Leave()
    {
        Application.Quit();
    }

    public void Return()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void Settings() 
    {
        settingsScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void Yes()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void No()
    {
        pauseScreen.SetActive(true);
        quitScreen.SetActive(false);
    }

    public void QuitGame()
    {
        quitScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }


}
