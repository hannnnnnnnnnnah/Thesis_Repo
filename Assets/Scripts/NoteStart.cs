using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteStart : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;
    [SerializeField] AudioSource audioSource;

    string moveText = "Use WASD to move";


    private void Start()
    {
        PlayerMovement.instance.GetComponent<CharacterController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerMovement.instance.rotate = true;
            PlayerMovement.instance.GetComponent<CharacterController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);

            trainMove.move = true;
            audioSource.Play();
            UIManager.instance.ShowText(moveText);
        }
    }
}
