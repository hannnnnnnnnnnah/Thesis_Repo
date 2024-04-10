using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] TrainMove trainMove;

    bool spawnPointSet = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && !spawnPointSet && !trainMove.move)
        {
            RespawnManager.instance.ChangeSpawn(gameObject.transform.position);
            spawnPointSet = true;
        }
    }
}
