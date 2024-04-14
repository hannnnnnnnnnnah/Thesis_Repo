using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene6 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource, wifeCrying;
    [SerializeField] ObjectFloat bedFloat;
    [SerializeField] GameObject bedObject;
    [SerializeField] List<GameObject> hospitalObjects;

    bool cutsceneTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            wifeCrying.Stop();
            InteractionManager.instance.surroundSound = false;
            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;

        foreach (GameObject hospObj in hospitalObjects)
        {
            if(hospObj != null)
                hospObj.GetComponent<ObjectThrow>().toss = true;
        }

        GameManager.instance.dialogueFinished = false;
        GameManager.instance.SetDialogue = audioSource;
        audioSource.Play();
        PlayerMovement.instance.dr.StartDialogue("Line6");

        yield return new WaitForSeconds(14f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        GameManager.instance.dialogueFinished = true;
        InteractionManager.instance.surroundSound = true;

        bedFloat.enabled = true;
        bedObject.GetComponent<MeshCollider>().enabled = false;
    }
}
