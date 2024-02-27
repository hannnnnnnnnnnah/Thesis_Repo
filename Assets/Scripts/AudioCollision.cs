using UnityEngine;

public class AudioCollision : MonoBehaviour
{
    Rigidbody body;
    AudioSource audioSource;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude >= Vector3.one.magnitude / 2)
            audioSource.Play();
    }
}
