using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] GameObject wife, newPlayer, furniture, particles, newSpawn;
    [SerializeField] Animator animator, playerAnim;
    [SerializeField] TrainMove trainMove;
    [SerializeField] AudioSource wifeShout, bg, wind;

    bool endReady = false;

    private void Update()
    {
        if(!trainMove.move && endReady)
        {
            Exit();
            endReady = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bg.Play();
            wind.Play();
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
        trainMove.move = true;
        yield return new WaitForSeconds(.5f);
        animator.SetBool("End", true);
        yield return new WaitForSeconds(2f);
        wifeShout.Play();
        endReady = true;
    }

    public void Exit()
    {
        wind.Stop();
        bg.Stop();

        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        NarrativeManager.instance.EndOfGame();

        //Player reenabled 
        newPlayer.SetActive(false);
        PlayerMovement.instance.mainCamera.enabled = true;
        PlayerMovement.instance.move = true;
        PlayerMovement.instance.rotate = true;

        RespawnManager.instance.Die();
    }
}
