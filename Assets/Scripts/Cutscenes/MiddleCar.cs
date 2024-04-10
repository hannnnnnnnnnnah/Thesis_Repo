using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCar : MonoBehaviour
{
    [SerializeField] GameObject NewSpawn, FloatArea;
    [SerializeField] PhoneTrigger phoneTrigger;
    [SerializeField] BoxCollider col;
    [SerializeField] AudioSource call;

    TrainMove trainMove;
    DoorHandler doorHandler;
    bool triggered, startStop = false;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
        doorHandler = GetComponent<DoorHandler>();
    }

    private void Update()
    {
        if (!trainMove.move && triggered && !startStop)
            StopAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            trainMove.move = true;

            col.enabled = true;
            doorHandler.DisableDoors();

            FloatArea.SetActive(true);
            triggered = true;
        }
    }

    void StopAction()
    {
        startStop = true;

        doorHandler.EnableDoors();

        phoneTrigger.PhoneStop();
        call.Play();
        PlayerMovement.instance.dr.StartDialogue("LineMiddle");
        InteractionManager.instance.surroundSound = false;
        InteractionManager.instance.StopSurround();

        RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);
    }
}
