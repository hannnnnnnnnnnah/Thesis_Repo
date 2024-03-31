using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTrain : MonoBehaviour
{
    TrainMove trainMove;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trainMove.move = true;
        }
    }
}
