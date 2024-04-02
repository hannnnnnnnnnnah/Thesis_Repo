using UnityEngine;

public class SavePointOutside : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager.instance.ChangeSpawn(gameObject.transform.position);
        }
    }
}