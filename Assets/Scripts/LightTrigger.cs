using System.Collections;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] bool lightFlicker, lightCanExplode, lightOut;
    [SerializeField] AudioSource source;

    public bool lightBroken = false;
    public Light spotlight;
    public Animator animator;

    float deathTimeReset = 8;

    private void Start()
    {
        spotlight = GetComponent<Light>();  
        animator = GetComponentInParent<Animator>();

        if (lightFlicker)
            animator.SetBool("LightFlicker", true);

        if (lightOut)
            NarrativeManager.instance.lights.Add(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!lightBroken && RespawnManager.instance.spawnChange)
            {
                if (InteractionManager.instance.surroundSound)
                    InteractionManager.instance.StopSurround();

                //Death effects are reset
                DeathTimer.instance.deathTime = deathTimeReset;
                DeathTimer.instance.StopAllCoroutines();
                DeathTimer.instance.DeathEffectsCancel();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (RespawnManager.instance.spawnChange && !PlayerMovement.instance.inTracks)
            {
                //Start and stop surround sound
                if (InteractionManager.instance.surroundSound)
                    InteractionManager.instance.StartSurround();

                //Death effects start
                DeathTimer.instance.StartCoroutine(DeathTimer.instance.DeathTime());

                if (InteractionManager.instance.lightExplodes && lightCanExplode)
                {
                    source.Play();
                    animator.SetBool("LightExplode", true);
                    lightBroken = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && lightBroken && !DeathTimer.instance.deathRunning)
            DeathTimer.instance.StartCoroutine(DeathTimer.instance.DeathTime());

        if(other.CompareTag("Player") && !lightBroken)
        {
            //Death effects are reset
            DeathTimer.instance.deathTime = deathTimeReset;
            DeathTimer.instance.StopAllCoroutines();
            DeathTimer.instance.DeathEffectsCancel();
        }
    }
}
