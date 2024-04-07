using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TVEnd : MonoBehaviour
{
    [SerializeField] TVTrigger trigger;
    [SerializeField] List<ObjectExplode> objects;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            trigger.Reset();

            foreach(var obj in objects)
                obj.explode = true;

            triggered = true;
        }
    }
}
