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

            Debug.Log("start cutscene6");

            InteractionManager.instance.surroundSound = false;
            PlayerMovement.instance.animator.SetBool("Cutscene", true);

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;

        foreach (GameObject hospObj in hospitalObjects)
            hospObj.GetComponent<ObjectThrow>().toss = true;

        audioSource.Play();

        yield return new WaitForSeconds(14f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        InteractionManager.instance.surroundSound = true;

        bedFloat.enabled = true;
        bedObject.GetComponent<MeshCollider>().enabled = false;
    }
}
