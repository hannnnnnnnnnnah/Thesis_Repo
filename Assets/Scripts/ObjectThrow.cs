using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    public float force, triggerDist;
    public bool toss = false;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < triggerDist)
            toss = true;
        else
            toss = false;

        if (toss)
        {
            rb.AddForce(transform.right * force);
        }
    }
}
