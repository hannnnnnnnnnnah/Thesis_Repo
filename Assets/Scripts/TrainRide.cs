using UnityEngine;


public class TrainRide : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.instance.riding = false;
            other.gameObject.transform.SetParent(null);
        }
    }
}
