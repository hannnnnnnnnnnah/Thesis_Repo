using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackKillTrigger : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());
            animator.SetBool("TrainKill", true);
        }
    }
}
