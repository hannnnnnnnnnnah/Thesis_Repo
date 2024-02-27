using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
    }
}
