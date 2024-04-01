using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button startButton;
    public GameObject titleScreen;
    public GameObject warningScreen;

    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(TaskOnClick);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void TaskOnClick()
    {
        warningScreen.SetActive(true);
        titleScreen.SetActive(false);
    }
}
