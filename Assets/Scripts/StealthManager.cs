using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.GetComponent<PlayerMovement>().speed > 3f)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, other.transform.rotation, 50);
        }
    }
}
