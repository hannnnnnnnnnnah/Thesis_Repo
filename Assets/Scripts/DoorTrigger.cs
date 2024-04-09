using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    AudioSource d_audio;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        d_audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetBool("NearDoor", true);
            d_audio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetBool("NearDoor", false); 
            d_audio.Play();
        }
    }
}
