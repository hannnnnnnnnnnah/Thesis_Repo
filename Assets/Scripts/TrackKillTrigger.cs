using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrackKillTrigger : MonoBehaviour
{
    Animator animator;
    [SerializeField] Animator ghost;
    [SerializeField] AudioSource metro;
    [SerializeField] Volume vol;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartTrain();
        }
    }

    public void StartTrain()
    {
        animator.SetBool("TrainKill", true);
        metro.Play();
        ghost.SetBool("GhostTracks", true);
    }
}
