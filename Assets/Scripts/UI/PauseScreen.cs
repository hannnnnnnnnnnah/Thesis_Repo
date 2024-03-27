using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
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
}
