using System.Collections;
using UnityEngine;

public class NoteStart : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator body, cam;
    [SerializeField] GameObject newCam, controls;

    string moveText = "Use WASD to move";

    bool letterRead = false;


    private void Start()
    {
        PlayerMovement.instance.move = false;
        PlayerMovement.instance.mainCamera.enabled = false;

        trainMove.move = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !letterRead)
        {
            LetterRead();
        }
    }

    public void LetterRead()
    {
        letterRead = true;
        body.SetBool("Hands", true);
        audioSource.Play();
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
