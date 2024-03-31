using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    public float FloatStrength;
    public float RandomRotation;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * Random.Range(1, FloatStrength));
        transform.Rotate(RandomRotation, RandomRotation, RandomRotation);
    }
}
