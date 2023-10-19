using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainInteraction : MonoBehaviour
{
    [SerializeField] AudioSource door;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("NearDoor", true);
            door.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("NearDoor", false);
            door.Play();
        }
    }
}
