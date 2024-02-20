using System.Collections;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] bool lightFlicker, lightCanExplode, lightOut;
    [SerializeField] AudioSource source;

    public float deathTimeReset;
    public bool lightBroken = false;
    public Light spotlight;

    public Animator animator;

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
        if(other.tag == "Player")
        {
            if (!lightBroken && RespawnManager.instance.spawnChange)
            {
                if (!InteractionManager.instance.surroundSound)
                    PlayerMovement.instance.StopSurroundSound();

                //Death effects are reset
                other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
                other.GetComponent<DeathTimer>().StopAllCoroutines();
                other.GetComponent<DeathTimer>().DeathEffectsCancel();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if (RespawnManager.instance.spawnChange && !PlayerMovement.instance.inTracks)
            {
                //Start and stop surround sound
                if (InteractionManager.instance.surroundSound)
                    PlayerMovement.instance.StartSurroundSound();

                if (!InteractionManager.instance.surroundSound)
                    PlayerMovement.instance.StopSurroundSound();

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
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && lightBroken && !other.GetComponent<DeathTimer>().deathRunning)
            other.GetComponent<DeathTimer>().StartCoroutine(other.GetComponent<DeathTimer>().DeathTime());

        if(other.tag == "Player" && !lightBroken)
        {
            //Death effects are reset
            other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
            other.GetComponent<DeathTimer>().StopAllCoroutines();
            other.GetComponent<DeathTimer>().DeathEffectsCancel();
        }
    }
}
