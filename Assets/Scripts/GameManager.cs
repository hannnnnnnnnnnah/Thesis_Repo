using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject settingsScreen;
    void Update()
    {
        if (pauseScreen.activeSelf == false && settingsScreen.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
