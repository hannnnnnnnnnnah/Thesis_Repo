using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    public float force, triggerDist, forceRadius;
    public bool toss = false;
    public bool tossLeft, tossRight;    

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
            if(tossLeft)
                rb.AddExplosionForce(force, transform.position - Vector3.left, forceRadius);

            if(tossRight)
                rb.AddExplosionForce(force, transform.position + Vector3.left, forceRadius);
        }
    }
}
