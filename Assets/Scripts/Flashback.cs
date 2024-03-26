using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashback : MonoBehaviour
{
    [SerializeField] Animator animator;

    bool flashbackTriggered = false;

    public List<GameObject> lights;

    [SerializeField] AudioSource lightBreak, laugh;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !flashbackTriggered)
            FlashbackStart();
    }

    public void FlashbackStart()
    {
        flashbackTriggered = true;
        animator.SetBool("Flashback1", true);
        StartCoroutine(FlashbackFade());

        lightBreak.Play();
        laugh.Play();

        foreach (GameObject light in lights)
        {
            light.GetComponentInParent<Animator>().SetBool("LightExplode", true);
            light.GetComponentInChildren<LightTrigger>().lightBroken = true;
        }
    }

    public IEnumerator FlashbackFade()
    {
        yield return new WaitForSeconds(.1f);
        animator.SetBool("Flashback1", false);
    }
}
