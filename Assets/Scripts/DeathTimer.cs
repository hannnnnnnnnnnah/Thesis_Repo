using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] AudioSource breathing, heartbeat, metro;
    [SerializeField] Animator visionCover;
    [SerializeField] GameObject SurroundSound;

    public Volume vol;
    public List<AudioClip> wifeSounds;

    public bool playVisuals, deathRunning = false;
    public float deathTime = 8;

    AudioSource audioSource;

    public static DeathTimer instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //vol.weight = 0f;
    }

    private void FixedUpdate()
    {
        if (vol.weight < 1f && playVisuals)
            vol.weight += 0.002f;

        if (!playVisuals && vol.weight > 0f)
            vol.weight -= 0.01f;

        if (deathTime == 0)
        {
            RespawnManager.instance.Die();
            DeathEffectsCancel();
            deathTime = 8;
        }

        //Debug.Log(vol.weight);
    }

    public void StartDeathTimer()
    {
        StartCoroutine(DeathTime());
    }

    public IEnumerator DeathTime()
    {
        while(deathTime > 0)
        {
            deathRunning = true;
            DeathAudioTrigger();
            yield return new WaitForSeconds(1f);
            deathTime--;
            //metro.mute = false;
        }    
    }

    public void DeathAudioTrigger() 
    {
        switch (deathTime)
        {
            case 8:
                playVisuals = true;
                visionCover.SetBool("DV", true);

                breathing.mute = false;
                heartbeat.mute = false;
                metro.mute = false;

                breathing.Stop();
                heartbeat.Stop();
                metro.Stop();
                break;

            case 6:
                if (!metro.isPlaying)
                    metro.Play();
                break;

            case 5:
                if (!breathing.isPlaying)
                    breathing.Play();
                break;

            case 4:
                if (!heartbeat.isPlaying)
                    heartbeat.Play();
                break;
        }
    }

    public void DeathEffectsCancel()
    {
        StartCoroutine(DeathEffectFade());
        visionCover.SetBool("DV", false);
        playVisuals = false;
    }

    public IEnumerator DeathEffectFade()
    {
        deathRunning = false;
        breathing.mute = true;
        heartbeat.mute = true;
        metro.mute = true;
        yield return new WaitForSeconds(.5f);
        breathing.Stop();
        heartbeat.Stop();
        metro.Stop();
    }

    public IEnumerator StartSurroundSound(int soundMin, int SoundMax)
    {
        SurroundSound.GetComponent<Animator>().SetBool("Rotate", true);

        while (InteractionManager.instance.surroundSound)
        {
            yield return new WaitForSeconds(2f);
            //audioSource.PlayOneShot(wifeSounds[Random.Range(soundMin, SoundMax)]);
            AudioSource.PlayClipAtPoint(wifeSounds[Random.Range(soundMin, SoundMax)], SurroundSound.transform.position);
            yield return new WaitForSeconds(1f);
        }
    }
}
