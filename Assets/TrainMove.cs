using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrainMove : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("TrainMove", true);
            audio.Play();
        }
    }
}
