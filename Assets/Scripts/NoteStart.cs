using System.Collections;
using UnityEngine;

public class NoteStart : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator body, cam, UI;
    [SerializeField] GameObject newCam, controls;

    string moveText = "Use WASD to move";

    bool letterRead, triggerReady = false;


    private void Start()
    {
        PlayerMovement.instance.move = false;
        PlayerMovement.instance.mainCamera.enabled = false;

        UI.SetBool("PulseText", true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !letterRead)
        {
            LetterRead();
        }
        if(!trainMove.move && triggerReady)
        {
            PlayStand();
            triggerReady = false;
        }
    }

    public void LetterRead()
    {
        letterRead = true;
        body.SetBool("Hands", true);
        UI.SetBool("PulseText", false);
        audioSource.Play();
        StartCoroutine(DelayStand());
    }

    IEnumerator DelayStand()
    {
        yield return new WaitForSeconds(2f);
        trainMove.move = true;
        triggerReady = true;
    }

    public void PlayStand()
    {
        cam.SetBool("Stand", true);
    }

    public void StandUp()
    {
        newCam.SetActive(false);
        PlayerMovement.instance.mainCamera.enabled = true;
        PlayerMovement.instance.move = true;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.instance.ShowText(moveText);
    }
}
