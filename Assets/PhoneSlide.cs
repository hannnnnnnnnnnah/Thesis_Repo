using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneSlide : MonoBehaviour
{
    public bool sliding = false;
    public float speed;

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;

        if (sliding)
        {
            if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) <= 10f)
            {
                transform.Translate(-Vector3.up * step);
            }
        }
    }
}
