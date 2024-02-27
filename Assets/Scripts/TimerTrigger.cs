using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public List<GameObject> lights = new List<GameObject>();

    bool timerStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!timerStarted)
        {
            foreach (var l in lights)
                StartCoroutine(l.GetComponent<LightTimer>().Timer());

            timerStarted = true;
        }
    }
}
