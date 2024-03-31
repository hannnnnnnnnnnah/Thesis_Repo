using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbackTrigger : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnim()
    {
        animator.SetInteger("FlashbackNum", 0);
    }
}
