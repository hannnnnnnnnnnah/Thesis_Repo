using UnityEngine;

public class ObjectFlicker : MonoBehaviour
{
    [SerializeField] GameObject wife;

    bool flickerStarted = false;
    public bool invisible = true;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (invisible)
        {
            if (DeathTimer.instance.deathRunning && !flickerStarted)
                wife.SetActive(true);
            else
                wife.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !flickerStarted)
        {
            PlayerMovement.instance.animator.SetBool("Flashback", true);
            wife.SetActive(false);
            audioSource.Play();
            flickerStarted = true;
        }
    }

    public void Disappear()
    {
        if(!flickerStarted && wife.activeSelf) 
        {
            PlayerMovement.instance.animator.SetBool("Flashback", true);
            wife.SetActive(false);
            audioSource.Play();
            flickerStarted = true;
        }
    }
}
