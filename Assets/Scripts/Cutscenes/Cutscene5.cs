using System.Collections;
using UnityEditor;
using UnityEngine;

public class Cutscene5 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator flashback;
    [SerializeField] TrainMove trainMove;
    [SerializeField] PhoneTrigger phoneTrigger;

    bool cutsceneTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            Debug.Log("start cutscene5");

            phoneTrigger.PhoneStop();
            PlayerMovement.instance.animator.SetBool("Cutscene", true);

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();

        yield return new WaitForSeconds(4f);
        trainMove.move = true;

        yield return new WaitForSeconds(20f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
    }
}
