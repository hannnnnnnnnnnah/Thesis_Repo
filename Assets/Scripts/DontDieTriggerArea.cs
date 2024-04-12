using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDieTriggerArea : MonoBehaviour
{
    float deathTimeReset = 8;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeathTimer.instance.deathTime = deathTimeReset;
            DeathTimer.instance.StopAllCoroutines();
            DeathTimer.instance.DeathEffectsCancel();
        }
    }
}
