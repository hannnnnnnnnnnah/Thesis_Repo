using System.Collections;
using UnityEditor;
using UnityEngine;

public class Cutscene4 : MonoBehaviour
{
    [SerializeField] GameObject newStopPos, savePoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject[] doors;
    [SerializeField] Collider colliderA, colliderB;
    [SerializeField] PhoneTrigger phoneTrigger;

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
            Debug.Log("start cutscene4");

            phoneTrigger.PhoneStop();
            PlayerMovement.instance.animator.SetBool("Cutscene", true);

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();

        trainMove.speed = 15f;
        trainMove.moveDirection = Vector3.forward;
        trainMove.stopPos = newStopPos;
        trainMove.move = true;

        colliderA.enabled = true;
        colliderB.enabled = true;

        foreach (var door in doors)
            door.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(35f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        yield return new WaitForSeconds(10f);

        foreach (var door in doors)
            door.GetComponent<Collider>().enabled = true;

        colliderA.enabled = false;
        colliderB.enabled = false;
    }
}
