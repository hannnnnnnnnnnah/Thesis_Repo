using UnityEngine;


public class TrainRide : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;

    CharacterController controller;

    public bool riding = false;
    public float limiter; 

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        controller = PlayerMovement.instance.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (riding)
        {
            PlayerMovement.instance.transform.SetParent(trainMove.transform);
            controller.Move((rb.velocity + (PlayerMovement.instance.moveDirection * PlayerMovement.instance.speed)) / limiter);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("yep");
            riding = true;
            PlayerMovement.instance.riding = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            riding = true;
            PlayerMovement.instance.riding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            riding = false;
            PlayerMovement.instance.transform.SetParent(null);
            PlayerMovement.instance.riding = false;
        }
    }
}
