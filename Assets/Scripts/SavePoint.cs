using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] GameObject cam1, cam2;

    bool spawnPointSet = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !spawnPointSet)
        {
            RespawnManager.instance.ChangeSpawn(gameObject.transform.position);
            cam1.GetComponent<Animator>().SetBool("CamSave", true);
            cam2.GetComponent<Animator>().SetBool("CamSave", true);
            spawnPointSet = true;
        }
    }
}
