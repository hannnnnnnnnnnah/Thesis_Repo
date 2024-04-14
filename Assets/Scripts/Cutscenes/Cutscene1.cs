using System.Collections;
using UnityEngine;

public class Cutscene1 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    DoorHandler doorHandler;

    bool cutsceneTriggered = false;

    private void Start()
    {
        doorHandler = GetComponent<DoorHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            doorHandler.DisableDoors();

            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;

        GameManager.instance.dialogueFinished = false;
        GameManager.instance.SetDialogue = audioSource;
        PlayerMovement.instance.dr.StartDialogue("Line1");
        audioSource.Play();

        yield return new WaitForSeconds(10f);

        doorHandler.EnableDoors();

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        GameManager.instance.dialogueFinished = true;
    }
}
