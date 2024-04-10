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
            PlayerMovement.instance.speed = 5f;

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        PlayerMovement.instance.dr.StartDialogue("Line1");

        audioSource.Play();
        yield return new WaitForSeconds(10f);

        doorHandler.EnableDoors();

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        PlayerMovement.instance.speed = 15f;
    }
}
