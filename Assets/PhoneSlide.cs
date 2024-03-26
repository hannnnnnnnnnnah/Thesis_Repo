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
            Debug.Log(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position));

            if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) <= 10f)
            {
                transform.Translate(-Vector3.up * step);
            }
        }
    }
}
