using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    Light lightRef;
    public float deathTimeReset;

    [SerializeField] AudioSource source;
    Animator animator;


    private void Start()
    {
        lightRef = GetComponent<Light>();       
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Death effects are reset
        other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
        other.GetComponent<DeathTimer>().StopAllCoroutines();
        other.GetComponent<DeathTimer>().DeathEffectsCancel();
    }

    private void OnTriggerExit(Collider other)
    {
        //Sanity is decreased
        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        //Death effects start
        other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());

        if (InteractionManager.instance.lightExplodes)
        {
            source.Play();
            animator.SetBool("LightExplode", true);
        }
    }
}
