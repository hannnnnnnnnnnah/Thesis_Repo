using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene7 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<GameObject> phones, lights;
    [SerializeField] GameObject OldStuff, NewStuff;

    bool cutsceneTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            Debug.Log("start cutscene7");
            BackgroundMusic.instance.StopBackgroundMusic();
            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            DeathTimer.instance.visionCover.SetBool("Transition", true);
            PlayerMovement.instance.speed = 5f;
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;

        foreach (GameObject obj in phones)
        {
            obj.GetComponent<PhoneTrigger>().PhoneStop();
        }

        audioSource.Play();
        PlayerMovement.instance.dr.StartDialogue("Line7");

        yield return new WaitForSeconds(10f);

        OldStuff.SetActive(false);
        NewStuff.SetActive(true);

        yield return new WaitForSeconds(3f);

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        DeathTimer.instance.visionCover.SetBool("Transition", false);
        PlayerMovement.instance.speed = 15f;

        foreach (GameObject obj in lights)
            obj.SetActive(true);
    }
}
