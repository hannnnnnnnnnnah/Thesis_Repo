using System.Collections;
using UnityEngine;

public class Cutscene3 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] TrainRide trainRide;
    [SerializeField] GameObject NewSpawn;

    DoorHandler doorHandler;
    TrainMove trainMove;

    bool cutsceneTriggered = false;

    private void Start()
    {
        doorHandler = GetComponent<DoorHandler>();  
        trainMove = GetComponent<TrainMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            doorHandler.DisableDoors();
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        trainMove.move = true;
        cutsceneTriggered = true;
        GameManager.instance.dialogueFinished = false;
        GameManager.instance.SetDialogue = audioSource;
        audioSource.Play();
        PlayerMovement.instance.dr.StartDialogue("Line3");
        yield return new WaitForSeconds(3f);
        PlayerMovement.instance.animator.SetBool("Cutscene", true);
        yield return new WaitForSeconds(12f);

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        GameManager.instance.dialogueFinished = true;

        //Sanity is decreased
        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        doorHandler.EnableDoors();
        RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);
    }
}
