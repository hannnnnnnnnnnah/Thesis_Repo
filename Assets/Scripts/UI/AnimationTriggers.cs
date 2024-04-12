using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTriggers : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void ButtonHoverSound()
    {
        SoundManager.PlaySound("hit");
    }
    void LightBreakSound()
    {
        SoundManager.PlaySound("light");
    }
    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
