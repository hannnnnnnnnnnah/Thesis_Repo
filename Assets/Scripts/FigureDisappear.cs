using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FigureDisappear : MonoBehaviour
{ 
    int LayerIgnoreRaycast;

    Animator animator;

    void Start()
    {
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        animator = GetComponent<Animator>();
    }

    public void Disappear()
    {
        animator.SetBool("Disappear", true);
        gameObject.layer = LayerIgnoreRaycast;
    }
}
