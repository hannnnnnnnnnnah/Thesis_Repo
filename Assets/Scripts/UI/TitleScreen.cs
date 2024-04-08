using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject twScreen;
    [SerializeField] GameObject controlsScreen;
    [SerializeField] GameObject creditsScreen;

    public void QuitGameTT()
    {
        Application.Quit();
    }
    public void StartGameTT()
    {
        titleScreen.SetActive(false);
        twScreen.SetActive(true);
    }
    public void ControlsButton()
    {
        titleScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }
    public void CreditsButton()
    {
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }
    public void ControlsDone()
    {
        controlsScreen.SetActive(false);
        titleScreen.SetActive(true);
    }
    public void CreditsDone()
    {
        creditsScreen.SetActive(false);
        titleScreen.SetActive(true);
    }
}
