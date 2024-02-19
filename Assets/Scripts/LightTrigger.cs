using System.Collections;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] bool lightFlicker, lightCanExplode, lightOut;
    [SerializeField] AudioSource source;
    [SerializeField] GameObject lmat;
    [SerializeField] Material material1, material2;

    public float deathTimeReset;
    public bool lightBroken = false;
    
    Animator animator;
    Light spotlight;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        spotlight = GetComponent<Light>();

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
}
