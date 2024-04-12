using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] GameObject wife, newPlayer, furniture, particles, newSpawn, Part3, StairsCutsceneUi;
    [SerializeField] Animator animator, playerAnim;
    [SerializeField] AudioSource wifeShout, bg;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
            //bg.Play();
            //BackgroundMusic.instance.StopBackgroundMusic();
            //particles.SetActive(true);
            //playerAnim.SetBool("EndCover", true);
            //furniture.gameObject.SetActive(false);
            //newPlayer.SetActive(true);
            //PlayerMovement.instance.move = false;
            //PlayerMovement.instance.rotate = false;
            //StartCoroutine(TheEnd());
    //    }
    //}

    public void TheEnd()
    {
        //wife.SetActive(true);
        //yield return new WaitForSeconds(.25f);
        //playerAnim.SetBool("EndCover", false);
        //PlayerMovement.instance.mainCamera.enabled = false;
        //yield return new WaitForSeconds(.5f);
        //animator.SetBool("End", true);
        SoundManager.PlaySound("look");
        PlayerMovement.instance.dr.StartDialogue("lookatme");
    }

    public void Exit()
    {
        StartCoroutine(ExitDelay());
    }

    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(.25f);

        //newPlayer.SetActive(false);
        //PlayerMovement.instance.mainCamera.enabled = true;
        //PlayerMovement.instance.move = true;
        //PlayerMovement.instance.rotate = true;
        NarrativeManager.instance.EndOfGame();

        //yield return new WaitForSeconds(.25f);

        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        RespawnManager.instance.Die();
        Part3.SetActive(false);

        yield return new WaitForSeconds(.2f);

        StairsCutsceneUi.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
