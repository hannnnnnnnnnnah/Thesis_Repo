using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene8 : MonoBehaviour
{
    [SerializeField] AudioSource lastLine;
    [SerializeField] GameObject[] doors;
    [SerializeField] GameObject newStopPos, signs, NewSpawn;
    [SerializeField] Collider a, b;
    [SerializeField] PhoneTrigger phoneTrigger;
    bool triggered = false;
    TrainMove trainMove;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            signs.SetActive(true);

            Debug.Log("Starting cutscene 8");

            foreach (var door in doors)
                door.GetComponent<Collider>().enabled = false;

            a.enabled = true;
            b.enabled = true;

            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            PlayerMovement.instance.speed = 5f;

            phoneTrigger.PhoneStop();

            StartCoroutine(Cutscene());

            triggered = true;
        }
    }

    IEnumerator Cutscene()
    {
        lastLine.Play();

        trainMove.speed = 11f;
        trainMove.moveDirection = Vector3.forward;
        trainMove.stopPos = newStopPos;
        trainMove.move = true;

        yield return new WaitForSeconds(9f);

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        BackgroundMusic.instance.PlayBackgroundMusic(3);

        yield return new WaitForSeconds(60f);

        PlayerMovement.instance.speed = 15f;

        foreach (var door in doors)
            door.GetComponent<Collider>().enabled = true;

        a.enabled = false;
        b.enabled = false;

        RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);
    }
}
