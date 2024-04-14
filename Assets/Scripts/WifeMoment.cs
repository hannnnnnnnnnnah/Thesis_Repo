using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeMoment : MonoBehaviour
{
    bool triggered = false;
    Animator animator;
    [SerializeField] AudioSource ChannelSwitch, man;
    [SerializeField] List<GameObject> lights = new List<GameObject>();

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            //StartCoroutine(WifeTime());
            man.Play();
            PlayerMovement.instance.animator.SetBool("Flashback", true);
            animator.SetBool("WifeFlash", true);
            triggered = true;
        }
    }

    IEnumerator WifeTime()
    {
        yield return new WaitForSeconds(1f);
        PlayerMovement.instance.animator.SetBool("Flashback", true);
        animator.SetBool("WifeFlash", true);
    }

    public void PlaySwitch()
    {
        ChannelSwitch.Play();
    }

    public void EndWifeTime()
    {
        StartCoroutine(EndingWifeTime());
    }

    IEnumerator EndingWifeTime()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("WifeFlash", false);
        foreach (var obj in lights)
            obj.SetActive(true);
    }
}
