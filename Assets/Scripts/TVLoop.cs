using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVLoop : MonoBehaviour
{
    [SerializeField] AudioSource channel;
    public AudioSource loop;

    public void Loop()
    {
        loop.Play();
    }

    public void Switch()
    {
        channel.Play();
    }
}
