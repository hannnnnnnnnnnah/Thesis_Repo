using System.Collections;
using UnityEditor;
using UnityEngine;

public class Cutscene5 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] PhoneTrigger phoneTrigger;
    [SerializeField] TrainMove trainMove;

    bool cutsceneTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            Debug.Log("start cutscene5");

            InteractionManager.instance.surroundSound = false;

            phoneTrigger.PhoneStop();
            PlayerMovement.instance.animator.SetBool("Cutscene", true);

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();

        yield return new WaitForSeconds(30f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        InteractionManager.instance.surroundSound = true;
        trainMove.move = true;
    }
}
