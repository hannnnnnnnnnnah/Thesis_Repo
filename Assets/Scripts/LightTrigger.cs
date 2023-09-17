using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    Light lightRef;
    public float deathTimeReset;

    [SerializeField] AudioSource source;
    [SerializeField] bool lightExplodes = false;
    [SerializeField] Animator animator;


    private void Start()
    {
        lightRef = GetComponent<Light>();          
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("light area entered");
        other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
        //other.GetComponent<DeathTimer>().StopCoroutine(other.GetComponent<DeathTimer>().DeathTime());
        other.GetComponent<DeathTimer>().StopAllCoroutines();
        other.GetComponent<DeathTimer>().DeathEffectsCancel();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("light area exited");
        other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());

        if (lightExplodes)
        {
            source.Play();
            animator.SetBool("LightExplode", true);
        }
    }
}
