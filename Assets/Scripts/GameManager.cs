using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject settingsScreen;

    public AudioSource SetDialogue;
    public bool dialogueFinished, escapeActive;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (pauseScreen.activeSelf == false && settingsScreen.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && escapeActive)
            {
                pauseScreen.SetActive(true);
                ManageDialogue(SetDialogue);
                Time.timeScale = 0;
            }
        }
    }

    public void ManageDialogue(AudioSource CurrentDialogue)
    {
        if (CurrentDialogue != null)
        {
            if (!CurrentDialogue.isPlaying && !dialogueFinished)
                CurrentDialogue.Play();
            else
                CurrentDialogue.Pause();
        }
        else
            return;
    }
}
