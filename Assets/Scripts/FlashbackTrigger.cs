using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbackTrigger : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnim()
    {
        animator.SetInteger("FlashbackNum", 0);
        playerAnim.SetBool("Flashback", false);
    }
}
