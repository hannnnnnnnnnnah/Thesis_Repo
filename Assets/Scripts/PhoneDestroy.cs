using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Phone"))
        {
            other.GetComponent<Collider>().enabled = false;
        }
    }
}
