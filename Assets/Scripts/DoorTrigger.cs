using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("OpenDoor", true);
            audio.Play();
        }
    }
}
