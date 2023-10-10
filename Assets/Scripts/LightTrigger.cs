using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public float deathTimeReset;

    [SerializeField] AudioSource source;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Death effects are reset
        other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
        other.GetComponent<DeathTimer>().StopAllCoroutines();
        other.GetComponent<DeathTimer>().DeathEffectsCancel();

        //Figure stops chasing
        if (InteractionManager.instance.sanity <= 2)
            FigureApproach.instance.approachPlayer = false;
    }

    private void OnTriggerExit(Collider other)
    {
        //Sanity is decreased
        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        //Start surround sound
        if(InteractionManager.instance.steps)
            other.GetComponent<PlayerMovement>().StartSurroundSound();

        //Figure starts chasing
        if (InteractionManager.instance.sanity <= 2)
            FigureApproach.instance.approachPlayer = true;


        //Death effects start
        other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());

        if (InteractionManager.instance.lightExplodes)
        {
            source.Play();
            animator.SetBool("LightExplode", true);
        }
    }
}
