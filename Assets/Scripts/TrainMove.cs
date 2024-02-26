using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour
{
    [SerializeField] bool move_right, move_left, move;
    [SerializeField] float speed;
    [SerializeField] GameObject stopPos;
    [SerializeField] AudioSource t_audio;

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;

        if (move)
        {
            if (!t_audio.isPlaying)
                t_audio.Play();

            if (move_right)
                transform.Translate(Vector3.back * step);

            if (move_left)
                transform.Translate(Vector3.forward * step);

            if (transform.position.z >= stopPos.transform.position.z)
                move = false;
        }
    }
}
