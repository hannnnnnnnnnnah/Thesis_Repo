using System.Collections;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] bool lightFlicker, lightCanExplode, lightOut, lightTimed;
    [SerializeField] AudioSource source;
    [SerializeField] GameObject lmat;
    [SerializeField] Material material1, material2;

    float intensitySet;
    public float deathTimeReset, lightDelay;
    public bool lightBroken = false;
    public bool lightTimer = true;
    
    Animator animator;
    Light spotlight;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        spotlight = GetComponent<Light>();
        intensitySet = spotlight.intensity;

        if (lightFlicker)
            animator.SetBool("LightFlicker", true);

        if (lightOut)
            NarrativeManager.instance.lights.Add(gameObject);

        if (lightTimed)
            StartCoroutine(LightTimed());
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

    public IEnumerator LightTimed()
    {
        while (lightTimer)
        {
            spotlight.enabled = false;
            lmat.GetComponent<MeshRenderer>().material = material1;
            yield return new WaitForSeconds(lightDelay);
            spotlight.enabled = true;
            lmat.GetComponent<MeshRenderer>().material = material2;
        }
    }
}
