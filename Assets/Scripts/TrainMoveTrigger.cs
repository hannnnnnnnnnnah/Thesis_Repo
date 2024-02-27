using UnityEngine;

public class TrainMoveTrigger : MonoBehaviour
{
    [SerializeField] TrainMove tMove;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !tMove.move)
            tMove.move = true;
    }
}
