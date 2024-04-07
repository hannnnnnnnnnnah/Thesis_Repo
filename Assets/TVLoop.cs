using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVLoop : MonoBehaviour
{
    [SerializeField] AudioSource loop, channel;

    public void Loop()
    {
        loop.Play();
    }

    public void Switch()
    {
        channel.Play();
    }
}
