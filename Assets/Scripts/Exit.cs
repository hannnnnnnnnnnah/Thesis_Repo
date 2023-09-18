using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] Animator animator;
    AudioSource audioSource;

    [SerializeField] AudioSource victims;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("Exit", true);
            audioSource.Play();
            RespawnManager.instance.exitTriggered = true;
            other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());
        }
    }
}
