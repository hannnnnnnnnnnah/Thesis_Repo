using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject OverheadLight;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            OverheadLight.gameObject.SetActive(false);

    }
}
