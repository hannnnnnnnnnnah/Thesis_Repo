using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    bool spawnPointSet = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !spawnPointSet)
        {
            RespawnManager.instance.ChangeSpawn(gameObject.transform.position);
            spawnPointSet = true;
        }
    }
}
