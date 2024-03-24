using UnityEngine;

public class PhoneTrigger : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Call", true);
            audioSource.Play();
        }
    }

    public void PhoneStop()
    {
        animator.SetBool("Call", false);
        audioSource.Stop();
    }
}
