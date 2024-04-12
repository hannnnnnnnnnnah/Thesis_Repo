using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCutscene : MonoBehaviour
{
    [SerializeField] GameObject StairsCutsceneUi;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StairsCutsceneUi.SetActive(true);
        }
    }
}
