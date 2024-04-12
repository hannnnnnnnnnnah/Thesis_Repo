using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLook : MonoBehaviour
{
    [SerializeField] AudioSource crying, explode, breathe, metro, flash, bg, lastWords;
    [SerializeField] GameObject endingCreditsUi;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.instance.move = false;
            PlayerMovement.instance.rotate = false;
            PlayerMovement.instance.mainCamera.enabled = false;
            animator.SetBool("Reach", true);
        }
    }

    public void StopCrying()
    {
        crying.Stop();
    }

    public void Explode()
    {
        explode.Play();
    }

    public void Breathe()
    {
        breathe.Play();
    }

    public void Metro()
    {
        metro.Play();
    }

    public void Flash()
    {
        flash.Play();
    }

    public void BG()
    {
        bg.Play();
    }

    public void StopAll()
    {
        bg.Stop();
        metro.Stop();
        breathe.Stop();
    }

    public void LastWords()
    {
        lastWords.Play();
        PlayerMovement.instance.dr.StartDialogue("Line9");
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void EndingCreditsUI()
    {
        endingCreditsUi.SetActive(true);
    }

}
