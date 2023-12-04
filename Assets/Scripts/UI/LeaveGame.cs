using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaveGame : MonoBehaviour
{
    public Button leaveButton;

    void Start()
    {
        leaveButton = GetComponent<Button>();
        leaveButton.onClick.AddListener(TaskOnClick);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void TaskOnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
