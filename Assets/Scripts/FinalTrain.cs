using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrain : MonoBehaviour
{
    [SerializeField] GameObject newStopPos, save, Part2;
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
                Part2.SetActive(false);
                triggered = true;
            }

            if (!trainMove.move)
            {
                RespawnManager.instance.ChangeSpawn(save.transform.position);
            }
        }
    }
}
