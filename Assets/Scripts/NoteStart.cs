using System.Collections;
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
        PlayerMovement.instance.move = false;
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

        PlayerMovement.instance.move = true;
        Cursor.lockState = CursorLockMode.Locked;
        letter.SetActive(false);
        audioSource.Play();
        PlayerMovement.instance.transform.SetParent(trainMove.transform);
        UIManager.instance.ShowText(moveText);

        yield return new WaitForSeconds(1f);

        trainMove.move = true;
    }
}
