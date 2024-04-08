using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WifeMoment : MonoBehaviour
{
    //me when wife

    bool triggered, forceRotate = false;
    Animator animator;
    [SerializeField] AudioSource ChannelSwitch, man;
    [SerializeField] GameObject rotTarget;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(forceRotate)
            PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.Lerp(PlayerMovement.instance.mainCamera.transform.rotation, rotTarget.transform.rotation, 10f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            StartCoroutine(WifeTime());
            man.Play();
            PlayerMovement.instance.speed = 2f;
            PlayerMovement.instance.rotate = false;
            forceRotate = true;

            //god forgive me for what I must do
            triggered = true;
        }
    }

    IEnumerator WifeTime()
    {
        yield return new WaitForSeconds(.5f);
        PlayerMovement.instance.animator.SetBool("Flashback", true);
        animator.SetBool("WifeFlash", true);
    }

    public void PlaySwitch()
    {
        ChannelSwitch.Play();
    }

    public void EndWifeTime()
    {
        animator.SetBool("WifeFlash", false);
        PlayerMovement.instance.rotate = true;
        forceRotate = false;
        PlayerMovement.instance.speed = 15f;
    }
}
