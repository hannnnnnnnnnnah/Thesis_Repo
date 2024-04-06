using UnityEngine;

public class ObjectFlicker : MonoBehaviour
{
    [SerializeField] Animator animator, playerAnimator;
    [SerializeField] GameObject wife;

    public int animNumber;
    bool flickerStarted = false;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !flickerStarted)
        {
            flickerStarted = true;
            wife.SetActive(false);
            animator.SetInteger("FlashbackNum", animNumber);
            audioSource.Play();
            playerAnimator.SetBool("Flashback", true);
        }
    }
}
