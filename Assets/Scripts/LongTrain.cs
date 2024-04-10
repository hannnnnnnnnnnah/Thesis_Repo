using UnityEngine;

public class LongTrain : MonoBehaviour
{
    DoorHandler doorHandler;

    private void Start()
    {
        doorHandler = GetComponent<DoorHandler>();
        doorHandler.DisableDoors();
    }
}
