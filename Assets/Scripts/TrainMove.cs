using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrainMove : MonoBehaviour
{
    AudioSource t_audio;

    [SerializeField] float speed;

    [SerializeField] bool move_right;
    [SerializeField] bool move_left;

    public bool move;

    private void Start()
    {
        t_audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;

        if (move)
        {
            if(!t_audio.isPlaying)
                t_audio.Play();

            if (move_right)
                transform.Translate(Vector3.back * step);

            if (move_left)
                transform.Translate(Vector3.forward * step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RespawnManager.instance.Die();
        }
    }
}
