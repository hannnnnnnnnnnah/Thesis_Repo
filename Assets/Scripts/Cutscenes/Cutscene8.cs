using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene8 : MonoBehaviour
{
    [SerializeField] AudioSource lastLine;
    [SerializeField] GameObject newStopPos, signs, NewSpawn;
    [SerializeField] PhoneTrigger phoneTrigger;

    bool triggered = false;
    TrainMove trainMove;
    DoorHandler doorHandler;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
        doorHandler = GetComponent<DoorHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            signs.SetActive(true);
            doorHandler.DisableDoors();
            doorHandler.leftSided = false;

            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            PlayerMovement.instance.speed = 5f;

            phoneTrigger.PhoneStop();

            StartCoroutine(Cutscene());

            triggered = true;
        }
    }

    IEnumerator Cutscene()
    {
        GameManager.instance.SetDialogue = lastLine;
        lastLine.Play();
        PlayerMovement.instance.dr.StartDialogue("Line8");

        trainMove.speed = 11f;
        trainMove.moveDirection = Vector3.forward;
        trainMove.stopPos = newStopPos;
        trainMove.move = true;

        yield return new WaitForSeconds(9f);

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        BackgroundMusic.instance.PlayBackgroundMusic(3);

        yield return new WaitForSeconds(60f);

        PlayerMovement.instance.speed = 15f;

        doorHandler.EnableDoors();

        RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);

        GameManager.instance.SetDialogue = null;
    }
}
