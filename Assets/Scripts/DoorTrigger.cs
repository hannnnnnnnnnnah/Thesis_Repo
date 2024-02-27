using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] AudioSource d_audio;
    [SerializeField] AudioSource d_audioClose;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
