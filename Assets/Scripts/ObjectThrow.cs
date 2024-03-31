using Unity.VisualScripting;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    [SerializeField] float force, triggerDist, forceRadius;
    [SerializeField] bool tossLeft, tossRight;    

    Rigidbody rb;

    public bool hasTrigger, toss = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!hasTrigger)
        {
            if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < triggerDist)
                toss = true;
            else
                toss = false;
        }

        if (toss)
        {
            if (tossLeft)
                rb.AddExplosionForce(force, transform.position - Vector3.left, forceRadius);

            if (tossRight)
                rb.AddExplosionForce(force, transform.position + Vector3.left, forceRadius);
        }
    }
}
