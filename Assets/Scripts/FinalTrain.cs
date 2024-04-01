using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrain : MonoBehaviour
{
    [SerializeField] GameObject newStopPos;
    TrainMove trainMove;
    bool triggered = false;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            trainMove.stopPos = newStopPos;
            trainMove.moveDirection = Vector3.forward;
            trainMove.move = true;
            triggered = true;
        }
    }
}
