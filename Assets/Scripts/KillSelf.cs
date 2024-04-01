using System.Collections;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    public float lifespan;
    bool dieTriggered = false;
    ObjectThrow objThrow;
    Rigidbody rb;
    MeshCollider mc;

    private void Start()
    {
        mc = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        objThrow = GetComponent<ObjectThrow>();
    }

    private void Update()
    {
        if (objThrow.toss && !dieTriggered)
        {
            StartCoroutine(Die());
            dieTriggered = true;
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(lifespan);
        mc.enabled = false;
        rb.mass = 10;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
