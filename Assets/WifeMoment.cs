using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class WifeMoment : MonoBehaviour
{
    //me when wife

    bool triggered, forceRotate, blendRotation = false;
    Animator animator;
    [SerializeField] AudioSource ChannelSwitch, man;
    [SerializeField] GameObject rotTarget;
    [SerializeField] List<GameObject> lights = new List<GameObject>();

    Quaternion storedRotation;

    float rotateSpeed = 150f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (forceRotate)
        {
            var targetRotation = Quaternion.LookRotation(rotTarget.transform.position - PlayerMovement.instance.mainCamera.transform.position);
            PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.RotateTowards(PlayerMovement.instance.mainCamera.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            //PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.Slerp(PlayerMovement.instance.mainCamera.transform.rotation, new Vector3(), 7f * Time.deltaTime);
        }

        if (blendRotation)
        {
            PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.RotateTowards(PlayerMovement.instance.mainCamera.transform.rotation, storedRotation, rotateSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            StartCoroutine(WifeTime());
            man.Play();
            PlayerMovement.instance.speed = 2f;
            PlayerMovement.instance.rotate = false;
            triggered = true;
        }
    }

    IEnumerator WifeTime()
    {
        yield return new WaitForSeconds(1f);
        storedRotation = Quaternion.LookRotation(PlayerMovement.instance.mainCamera.transform.position);
        forceRotate = true;
        yield return new WaitForSeconds(.75f);
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
        forceRotate = false;
        blendRotation = true;
        yield return new WaitForSeconds(1.2f);
        PlayerMovement.instance.rotate = true;
        animator.SetBool("WifeFlash", false);
        PlayerMovement.instance.speed = 15f;
        foreach (var obj in lights)
            obj.SetActive(true);
    }
}
