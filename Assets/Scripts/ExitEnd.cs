using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            Debug.Log("PELASE");
        }
    }
}
