using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene7 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<GameObject> phones, lights;
    [SerializeField] GameObject OldStuff, NewStuff, NewSpawn;

    bool cutsceneTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            BackgroundMusic.instance.StopBackgroundMusic();
            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            DeathTimer.instance.visionCover.SetBool("Transition", true);
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);

        cutsceneTriggered = true;

        foreach (GameObject obj in phones)
        {
            obj.GetComponent<PhoneTrigger>().PhoneStop();
        }

        GameManager.instance.dialogueFinished = false;
        GameManager.instance.SetDialogue = audioSource;
        audioSource.Play();
        PlayerMovement.instance.dr.StartDialogue("Line7");

        yield return new WaitForSeconds(10f);

        OldStuff.SetActive(false);
        NewStuff.SetActive(true);

        yield return new WaitForSeconds(3f);

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        GameManager.instance.dialogueFinished = true;
        DeathTimer.instance.visionCover.SetBool("Transition", false);

        foreach (GameObject obj in lights)
            obj.SetActive(true);
    }
}
