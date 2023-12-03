using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] bool lightFlicker, lightCanExplode;
    [SerializeField] AudioSource source;

    public float deathTimeReset;
    public bool lightBroken = false;
    
    Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();

        if (lightFlicker)
            animator.SetBool("LightFlicker", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!lightBroken && RespawnManager.instance.spawnChange)
            {
                if (!InteractionManager.instance.surroundSound)
                    other.GetComponent<PlayerMovement>().StopSurroundSound();

                //Death effects are reset
                other.GetComponent<DeathTimer>().deathTime = deathTimeReset;
                other.GetComponent<DeathTimer>().StopAllCoroutines();
                other.GetComponent<DeathTimer>().DeathEffectsCancel();

                //Figure stops chasing
                if (InteractionManager.instance.emmaSpawned)
                    GameObject.FindGameObjectWithTag("Emma").GetComponent<FigureApproach>().approachPlayer = false;
            }
        }

        if (other.tag == "Figure")
        {
            Debug.Log("figure died in the light uwu");
            other.gameObject.GetComponent<FigureDisappear>().Die();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {

            if (RespawnManager.instance.spawnChange && !other.GetComponent<PlayerMovement>().inTracks)
            {
                //Start and stop surround sound
                if (InteractionManager.instance.surroundSound)
                    other.GetComponent<PlayerMovement>().StartSurroundSound();

                if (!InteractionManager.instance.surroundSound)
                    other.GetComponent<PlayerMovement>().StopSurroundSound();

                //Figure starts chasing
                if (InteractionManager.instance.emmaSpawned)
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
    }
}
