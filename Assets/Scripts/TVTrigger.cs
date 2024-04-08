using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVTrigger : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<Animator> TVanims = new List<Animator>();
    bool isTriggered = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isTriggered)
        {
            BackgroundMusic.instance.StopBackgroundMusic();
            BackgroundMusic.instance.PlayBackgroundMusic(2);

            foreach(var anim in TVanims)
                anim.SetBool("TVActive", true);

            audioSource.Play();

            isTriggered = true;
        }
    }

    public void Reset()
    {
        foreach (var anim in TVanims)
        {
            anim.SetBool("TVActive", false);
            anim.GetComponentInParent<TVLoop>().loop.Stop();
        }

        audioSource.Stop();
    }
}
