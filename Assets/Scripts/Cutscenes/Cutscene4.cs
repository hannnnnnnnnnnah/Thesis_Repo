using System.Collections;
using UnityEditor;
using UnityEngine;

public class Cutscene4 : MonoBehaviour
{
    [SerializeField] GameObject newStopPos, phone, savePoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject[] doors;

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

            foreach (var door in doors)
                door.GetComponent<Collider>().enabled = false;

            PlayerMovement.instance.animator.SetBool("Cutscene", true);

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();

        foreach (var door in doors)
            door.GetComponent<Collider>().enabled = true;

        yield return new WaitForSeconds(4f);

        trainMove.speed = 15f;
        trainMove.moveDirection = Vector3.forward;
        trainMove.stopPos = newStopPos;
        trainMove.move = true;

        yield return new WaitForSeconds(41f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
    }
}
