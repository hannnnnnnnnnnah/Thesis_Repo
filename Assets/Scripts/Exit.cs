using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator ghosts;
    AudioSource audioSource;

    [SerializeField] AudioSource victims;

    [SerializeField] GameObject goal;
    [SerializeField] TrackKillTrigger tm;
    [SerializeField] TrackKillTrigger tm2;

    bool exitEntered = false;   

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !exitEntered)
        {
            tm.StartTrain();
            tm2.StartTrain();
            other.GetComponent<CharacterController>().enabled = false;
            goal.SetActive(false);
            animator.SetBool("Exit", true);
            ghosts.SetBool("GhostTracks", true);
            audioSource.Play();
            victims.Play();
            RespawnManager.instance.exitTriggered = true;
            other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());
            exitEntered = true;
        }
    }
}
