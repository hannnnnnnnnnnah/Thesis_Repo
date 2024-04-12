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
            Debug.Log("start cutscene3");

            doorHandler.DisableDoors();

            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            PlayerMovement.instance.speed = 5f;

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        trainMove.move = true;
        cutsceneTriggered = true;
        GameManager.instance.SetDialogue = audioSource;
        audioSource.Play();
        PlayerMovement.instance.dr.StartDialogue("Line3");
        yield return new WaitForSeconds(14f);

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        PlayerMovement.instance.speed = 15f;

        //Sanity is decreased
        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        doorHandler.EnableDoors();
        RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);

        GameManager.instance.SetDialogue = null;
    }
}
