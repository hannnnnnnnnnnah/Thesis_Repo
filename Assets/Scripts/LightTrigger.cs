using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public float deathTimeReset;

    [SerializeField] AudioSource source;
    [SerializeField] bool lightFlicker;
    [SerializeField] bool lightCanExplode;

    Animator animator;
    bool lightBroken = false;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        if (lightFlicker)
            animator.SetBool("LightFlicker", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!lightBroken)
        {
            if(!InteractionManager.instance.surroundSound)
                other.GetComponent<PlayerMovement>().StopSurroundSound();

            //Death effects are reset
            other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
            other.GetComponent<DeathTimer>().StopAllCoroutines();
            other.GetComponent<DeathTimer>().DeathEffectsCancel();

            //Figure stops chasing
            if (InteractionManager.instance.sanity <= 2)
                GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().approachPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Start and stop surround sound
        if(InteractionManager.instance.surroundSound)
            other.GetComponent<PlayerMovement>().StartSurroundSound();

        if (!InteractionManager.instance.surroundSound)
            other.GetComponent<PlayerMovement>().StopSurroundSound();

        //Figure starts chasing
        if (InteractionManager.instance.sanity <= 2)
            GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().approachPlayer = true;
        

        //Death effects start
        other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());

        if (InteractionManager.instance.lightExplodes && lightCanExplode)
        {
            source.Play();
            animator.SetBool("LightExplode", true);
            lightBroken = true;
        }
    }
}
