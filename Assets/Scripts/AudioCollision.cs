using System;
using UnityEngine;

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
            audioSource.Play();
    }
}
