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

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TaskOnClick()
    {
        //unselect the button
        if (EventSystem.current.currentSelectedGameObject == continueButton.gameObject) 
            EventSystem.current.SetSelectedGameObject(null);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausedUi.SetActive(false);
        Time.timeScale = 1;
    }


}
