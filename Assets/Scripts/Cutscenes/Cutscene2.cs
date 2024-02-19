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
        if (other.tag == "Player")
        {
            if (!cutsceneTriggered)
            {
                Debug.Log("start cutscene2");
                PlayerMovement.instance.animator.SetBool("Cutscene", true);

                StartCoroutine(Cutscene());
            }
        }
    }

    IEnumerator Cutscene()
    {
        cutsceneTriggered = true;
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        Instantiate(can, spawnLoc.transform.position, spawnLoc.transform.rotation);
        yield return new WaitForSeconds(7f);
        PlayerMovement.instance.animator.SetBool("Cutscene", false);

        foreach (var light in lights)
        {
            Debug.Log("triggerred :P");
            light.GetComponent<Animator>().SetBool("LightExplode", false);
            light.GetComponentInChildren<LightTrigger>().spotlight.enabled = true;
            light.GetComponentInChildren<LightTrigger>().lightBroken = false;
        }
    }
}
