using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrackKillTrigger : MonoBehaviour
{
    public GameObject[] metrocars;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartTrain();
        }
    }

    public void StartTrain()
    {
        foreach(GameObject metrocar in metrocars)
            metrocar.GetComponent<TrainMove>().move = true;
    }
}
