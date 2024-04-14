using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2 : MonoBehaviour
{
    [SerializeField] GameObject can, spawnLoc;
    
    public List<GameObject> lights;

    bool cutsceneTriggered = false;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cutsceneTriggered)
        {
            PlayerMovement.instance.animator.SetBool("Cutscene", true);
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        GameManager.instance.dialogueFinished = false;
        GameManager.instance.SetDialogue = audioSource;
        audioSource.Play();
        yield return new WaitForSeconds(5.5f);
        Instantiate(can, spawnLoc.transform.position, spawnLoc.transform.rotation);
        PlayerMovement.instance.dr.StartDialogue("Line2");
        yield return new WaitForSeconds(7f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);
        GameManager.instance.dialogueFinished = true;
        GameManager.instance.dialogueFinished = true;

        foreach (var light in lights)
        {
            light.GetComponent<Animator>().SetBool("LightExplode", false);
            light.GetComponentInChildren<LightTrigger>().spotlight.enabled = true;
            light.GetComponentInChildren<LightTrigger>().lightBroken = false;
        }
    }
}
