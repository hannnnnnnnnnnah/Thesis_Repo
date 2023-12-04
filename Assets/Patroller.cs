using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    Animator animator;
    public string patrolSet;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(patrolSet, true);
    }
}
