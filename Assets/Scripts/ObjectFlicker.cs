using UnityEngine;

public class ObjectFlicker : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject wife;

    public int animNumber;
    bool flickerStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !flickerStarted)
        {
            flickerStarted = true;
            wife.SetActive(false);
            animator.SetInteger("FlashbackNum", animNumber);
        }
    }
}
