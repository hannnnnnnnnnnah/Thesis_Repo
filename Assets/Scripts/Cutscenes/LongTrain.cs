using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTrain : MonoBehaviour
{
    TrainMove trainMove;
    [SerializeField] GameObject NewSpawn;
    [SerializeField] PhoneTrigger phoneTrigger;
    [SerializeField] List<Collider> colliders;
    [SerializeField] AudioSource call;

    bool triggered, startStop = false;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
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

            foreach (var collider in colliders)
                collider.enabled = true;

            triggered = true;
        }
    }

    void StopAction()
    {
        startStop = true;

        foreach (var collider in colliders)
            collider.enabled = false;

        phoneTrigger.PhoneStop();
        call.Play();
        PlayerMovement.instance.dr.StartDialogue("LineMiddle");
        InteractionManager.instance.surroundSound = false;
        InteractionManager.instance.StopSurround();

        RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);
    }
}
