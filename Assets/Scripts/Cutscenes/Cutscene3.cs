using System.Collections;
using UnityEngine;

public class Cutscene3 : MonoBehaviour
{
    [SerializeField] GameObject[] doors;
    [SerializeField] Collider a, b;
    [SerializeField] AudioSource audioSource;
    [SerializeField] TrainRide trainRide;
    [SerializeField] GameObject savePoint;

    TrainMove trainMove;

    bool cutsceneTriggered = false;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
    }

    private void Update()
    {
        if (cutsceneTriggered && !trainMove.move)
            RespawnManager.instance.ChangeSpawn(savePoint.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            Debug.Log("start cutscene3");

            foreach (var door in doors)
                door.GetComponent<Collider>().enabled = false;

            a.enabled = true;
            b.enabled = true;

            PlayerMovement.instance.animator.SetBool("Cutscene", true);

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();
        yield return new WaitForSeconds(14f);

        foreach (var door in doors)
            door.GetComponent<Collider>().enabled = true;

        a.enabled = false;
        b.enabled = false;

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        trainMove.move = true; 
        trainRide.noCutscene = true;

        //Sanity is decreased
        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();
    }
}
