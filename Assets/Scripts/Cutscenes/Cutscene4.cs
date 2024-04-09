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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            Debug.Log("start cutscene4");

            phoneTrigger.PhoneStop();
            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            PlayerMovement.instance.speed = 5f;

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();
        PlayerMovement.instance.dr.StartDialogue("Line4");

        yield return new WaitForSeconds(2f);
        trainMove.speed = 15f;
        trainMove.moveDirection = Vector3.forward;
        trainMove.stopPos = newStopPos;
        trainMove.move = true;

        colliderA.enabled = true;
        colliderB.enabled = true;

        foreach (var door in doors)
            door.GetComponentInChildren<Collider>().enabled = false;

        yield return new WaitForSeconds(35f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        PlayerMovement.instance.speed = 15f;
        yield return new WaitForSeconds(10f);

        foreach (var door in doors)
            door.GetComponentInChildren<Collider>().enabled = true;

        colliderA.enabled = false;
        colliderB.enabled = false;

        RespawnManager.instance.ChangeSpawn(savePoint.transform.position);
    }
}
