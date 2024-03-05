using UnityEngine;

public class SignFall : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            rb.useGravity = true;
    }
}
