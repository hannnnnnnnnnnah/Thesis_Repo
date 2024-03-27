using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioCollision : MonoBehaviour
{
    public float impactAmount;

    Rigidbody body;
    AudioSource audioSource;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.impulse.magnitude);

        if (collision.impulse.magnitude >= impactAmount)
        {
            audioSource.pitch = 1f + Random.Range(-0.1f, 0.1f);
            audioSource.Play();
        }
    }
}
