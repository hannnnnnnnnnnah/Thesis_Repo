using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrain : MonoBehaviour
{
    [SerializeField] GameObject newStopPos, save, Part2, FloatArea, TvArea;
    TrainMove trainMove;
    bool triggered, stopped = false;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
    }

    private void Update()
    {
        if(!trainMove.move && !stopped && triggered)
        {
            RespawnManager.instance.ChangeSpawn(save.transform.position);
            FloatArea.SetActive(false);
            stopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!triggered)
            {
                TvArea.SetActive(true);
                trainMove.stopPos = newStopPos;
                trainMove.moveDirection = Vector3.forward;
                trainMove.move = true;
                Part2.SetActive(false);
                triggered = true;
            }
        }
    }
}
