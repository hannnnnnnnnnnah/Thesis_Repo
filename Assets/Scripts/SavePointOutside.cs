using UnityEngine;

public class SavePointOutside : MonoBehaviour
{
    bool spawnPointSet = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !spawnPointSet)
        {
            RespawnManager.instance.ChangeSpawn(gameObject.transform.position);
        }
    }
}