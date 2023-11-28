using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] AudioSource breathing, heartbeat, metro;
    [SerializeField] Volume vol;
    [SerializeField] Animator animator;

    public bool playVisuals = false;
    public float deathTime = 11;

    private void Update()
    {
        if(deathTime == 0)
        {
            RespawnManager.instance.Die();
            DeathEffectsCancel();
            deathTime = 11;
        }
    }

    private void FixedUpdate()
    {
        //if (deathTime < 11 && vol.weight < 1 && playVisuals == true)
        if (vol.weight < 1 && playVisuals == true)
        {
            vol.weight += 0.002f;
            //Debug.Log("working");
        }
            
        if(playVisuals == false && vol.weight > 0)
            vol.weight -= 0.01f;
    }

    public void startDeathTimer()
    {
        StartCoroutine(DeathTime());
    }

    public IEnumerator DeathTime()
    {
        while(deathTime > 0)
        {
            DeathAudioTrigger();
            yield return new WaitForSeconds(1f);
            deathTime--;
            //Debug.Log(deathTime);
        }    
    }

    public void DeathAudioTrigger() 
    {
        switch (deathTime)
        {
            case 11:
                playVisuals = true;
                animator.SetBool("DV", true);

                breathing.mute = false;
                heartbeat.mute = false;
                metro.mute = false;

                if (!breathing.isPlaying)
                {
                    breathing.Play();
                }
                break;

            case 10:
                if (!metro.isPlaying)
                {
                    metro.Play();
                }
                break;

            case 6:
                if (!heartbeat.isPlaying)
                {
                    heartbeat.Play();
                }
                break;
        }
    }

    public IEnumerator DeathEffectFade()
    {
        breathing.mute = true;
        heartbeat.mute = true;
        metro.mute = true;
        yield return new WaitForSeconds(1f);
        breathing.Stop();
        heartbeat.Stop();
        metro.Stop();
    }

    public void DeathEffectsCancel()
    {
        StartCoroutine(DeathEffectFade());
        animator.SetBool("DV", false);
        playVisuals = false;
    }
}
