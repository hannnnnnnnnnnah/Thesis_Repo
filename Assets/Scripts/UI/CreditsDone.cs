using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsDone : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] GameObject creditButton;
    public void CreditsBack()
    {
        EventSystem.current.SetSelectedGameObject(null);
        titleScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }
}
