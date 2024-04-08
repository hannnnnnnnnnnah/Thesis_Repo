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

    public void SwitchScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
