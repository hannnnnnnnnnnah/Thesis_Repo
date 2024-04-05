using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject pauseScreen;
    public void BackToPause()
    {
        pauseScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }
}
