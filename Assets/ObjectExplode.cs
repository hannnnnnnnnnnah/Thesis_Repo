using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplode : MonoBehaviour
{
    [SerializeField] float force, forceRadius;
    Sink sink;
    Rigidbody rb;
    public bool explode = false;
    bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sink = GetComponent<Sink>();
    }

    private void FixedUpdate()
    {
        if (explode)
        {
            //rb.isKinematic = false;
            rb.AddExplosionForce(force, transform.position + Vector3.up, forceRadius);

            if (!triggered)
            {
                StartCoroutine(StartSink());
                triggered = true;
            }
        }
    }

    IEnumerator StartSink()
    {
        yield return new WaitForSeconds(5f);
        sink.StartSink();
    }
}
