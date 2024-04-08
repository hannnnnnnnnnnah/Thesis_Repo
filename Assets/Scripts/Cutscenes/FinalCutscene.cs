using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] GameObject wife, newPlayer, furniture, particles, newSpawn, Part3;
    [SerializeField] Animator animator, playerAnim;
    [SerializeField] AudioSource wifeShout, bg;

    bool endReady = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bg.Play();
            BackgroundMusic.instance.StopBackgroundMusic();
            particles.SetActive(true);
            playerAnim.SetBool("EndCover", true);
            furniture.gameObject.SetActive(false);
            newPlayer.SetActive(true);
            PlayerMovement.instance.move = false;
            PlayerMovement.instance.rotate = false;
            StartCoroutine(TheEnd());
        }
    }

    public IEnumerator TheEnd()
    {
        wife.SetActive(true);
        yield return new WaitForSeconds(.25f);
        playerAnim.SetBool("EndCover", false);
        PlayerMovement.instance.mainCamera.enabled = false;
        yield return new WaitForSeconds(.5f);
        animator.SetBool("End", true);
        //yield return new WaitForSeconds(2f);
        wifeShout.Play();
        endReady = true;
    }

    public void Exit()
    {
        bg.Stop();

        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        NarrativeManager.instance.EndOfGame();

        //Player reenabled 
        newPlayer.SetActive(false);
        PlayerMovement.instance.mainCamera.enabled = true;
        PlayerMovement.instance.move = true;
        PlayerMovement.instance.rotate = true;

        RespawnManager.instance.Die();

        Part3.SetActive(false);
    }
}
