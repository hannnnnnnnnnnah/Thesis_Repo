using UnityEngine;


public class TrainRide : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;

    public bool noCutscene, riding = false;

    private void FixedUpdate()
    {
        if (riding)
            PlayerMovement.instance.transform.SetParent(trainMove.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && noCutscene)
        {
            riding = true;
            //trainMove.speed = 20f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && noCutscene)
        {
            riding = false;
            PlayerMovement.instance.transform.SetParent(null);
        }
    }
}
