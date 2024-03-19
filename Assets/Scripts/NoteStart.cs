using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteStart : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject letter;

    string moveText = "Use WASD to move";

    bool letterRead = false;


    private void Start()
    {
        PlayerMovement.instance.GetComponent<CharacterController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !letterRead)
        {
            StartCoroutine(LetterRead());
        }
    }

    IEnumerator LetterRead()
    {
        letterRead = true;

        PlayerMovement.instance.rotate = true;
        PlayerMovement.instance.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        letter.SetActive(false);
        PlayerMovement.instance.transform.SetParent(trainMove.transform);

        yield return new WaitForSeconds(1f);

        trainMove.move = true;
        audioSource.Play();
        UIManager.instance.ShowText(moveText);
    }
}
