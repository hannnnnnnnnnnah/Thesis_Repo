using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public float deathTimeReset;
    Collider playerC;

    [SerializeField] AudioSource source;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerC = other;

        //Death effects are reset
        other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
        other.GetComponent<DeathTimer>().StopAllCoroutines();
        other.GetComponent<DeathTimer>().DeathEffectsCancel();

        //Figure stops chasing
        if (InteractionManager.instance.sanity <= 2)
            GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().approachPlayer = false;
    }

    private void OnTriggerExit(Collider other)
    {
        //Sanity is decreased
        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        //Start surround sound
        if(InteractionManager.instance.surroundSound)
            other.GetComponent<PlayerMovement>().StartSurroundSound();

        //Figure starts chasing
        if (InteractionManager.instance.sanity <= 2)
            GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().approachPlayer = true;


        //Death effects start
        other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());

        if (InteractionManager.instance.lightExplodes)
        {
            source.Play();
            animator.SetBool("LightExplode", true);
        }
    }

    private void OnDisable()
    {
        //playerC.GetComponent<DeathTimer>().StartCoroutine(playerC.GetComponent<DeathTimer>().DeathTime());
    }
}
