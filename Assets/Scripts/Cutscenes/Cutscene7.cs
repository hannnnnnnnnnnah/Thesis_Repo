using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene7 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<GameObject> phones, lights;

    bool cutsceneTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            Debug.Log("start cutscene7");
            PlayerMovement.instance.animator.SetBool("Cutscene", true);
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

        yield return new WaitForSeconds(11f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        PlayerMovement.instance.speed = 15f;


        foreach (GameObject obj in lights)
            obj.SetActive(true);
    }
}
