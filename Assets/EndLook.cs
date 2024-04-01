using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLook : MonoBehaviour
{
    [SerializeField] AudioSource crying;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crying.Stop();
            animator.SetBool("Look", true);
            PlayerMovement.instance.speed = 4f;
        }
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
