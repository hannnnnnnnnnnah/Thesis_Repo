using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] GameObject cam1, cam2;
    [SerializeField] TrainMove trainMove;

    bool spawnPointSet = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && !spawnPointSet && !trainMove.move)
        {
            RespawnManager.instance.ChangeSpawn(gameObject.transform.position);
            RespawnManager.instance.trainHop = false;
            cam1.GetComponent<Animator>().SetBool("CamSave", true);
            cam2.GetComponent<Animator>().SetBool("CamSave", true);
            spawnPointSet = true;
        }
    }
}
