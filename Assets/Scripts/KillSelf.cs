using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    public float lifespan;
    bool dieTriggered = false;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Debug.Log(rb.velocity.magnitude);

        if (rb.velocity.magnitude > 1f && !dieTriggered)
        {
            StartCoroutine(Die());
            dieTriggered = true;
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
