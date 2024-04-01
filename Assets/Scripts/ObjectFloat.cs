using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    public float FloatStrength;
    public float RandomRotation;
    public bool floating, addtoList;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(addtoList)
           FloatingArea.instance.floatables.Add(gameObject);
    }

    void FixedUpdate()
    {
        if (floating) 
        {
            rb.AddForce(Vector3.up * Random.Range(1, FloatStrength));
            transform.Rotate(RandomRotation, RandomRotation, RandomRotation);
        }
    }
}
