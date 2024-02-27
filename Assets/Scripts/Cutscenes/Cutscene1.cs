using System.Collections;
using UnityEngine;

public class Cutscene1 : MonoBehaviour
{
    [SerializeField] GameObject[] doors;
    [SerializeField] Collider a, b;
    [SerializeField] AudioSource audioSource;

    bool cutsceneTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            Debug.Log("start cutscene1");

            foreach (var door in doors)
                door.GetComponent<Collider>().enabled = false;

            a.enabled = true;
            b.enabled = true;

            PlayerMovement.instance.animator.SetBool("Cutscene", true);

            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();
        yield return new WaitForSeconds(10f);

        foreach (var door in doors)
            door.GetComponent<Collider>().enabled = true;

        a.enabled = false;
        b.enabled = false;

        PlayerMovement.instance.animator.SetBool("Cutscene", false);
    }
}
