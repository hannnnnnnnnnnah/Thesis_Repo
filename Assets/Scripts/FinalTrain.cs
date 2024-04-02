using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrain : MonoBehaviour
{
    [SerializeField] GameObject newStopPos, save;
    TrainMove trainMove;
    bool triggered = false;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!triggered)
            {
                trainMove.stopPos = newStopPos;
                trainMove.moveDirection = Vector3.forward;
                trainMove.move = true;
                triggered = true;
            }

            if (!trainMove.move)
            {
                RespawnManager.instance.ChangeSpawn(save.transform.position);
            }
        }
    }
}
