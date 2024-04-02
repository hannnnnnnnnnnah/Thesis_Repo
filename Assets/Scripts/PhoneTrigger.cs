using UnityEngine;

public class PhoneTrigger : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    bool phoneStarted = false;
    BoxCollider coll;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        coll = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !phoneStarted)
        {
            animator.SetBool("Call", true);
            audioSource.Play();
            phoneStarted = true;
        }
    }

    public void PhoneStop()
    {
        animator.SetBool("Call", false);
        audioSource.Stop();
        coll.enabled = false;
    }
}
