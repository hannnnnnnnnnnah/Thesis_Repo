using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    [SerializeField] bool disappear;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("player touched");
        }
    }
}
