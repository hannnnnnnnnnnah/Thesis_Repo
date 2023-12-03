using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContinueGame : MonoBehaviour
{
    public Button continueButton;
    public GameObject pausedUi;

    void Start()
    {
        continueButton = GetComponent<Button>();
        continueButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (EventSystem.current.currentSelectedGameObject == continueButton.gameObject) //unselect the button
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        pausedUi.SetActive(false);
        Time.timeScale = 1;
    }


}
