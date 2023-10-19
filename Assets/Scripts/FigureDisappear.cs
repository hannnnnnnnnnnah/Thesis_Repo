using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FigureDisappear : MonoBehaviour
{ 
    public static FigureDisappear instance;
    int LayerIgnoreRaycast;

    Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        animator = GetComponent<Animator>();
    }

    public void Disappear()
    {
        animator.SetBool("Disappear", true);
        gameObject.layer = LayerIgnoreRaycast;
    }
}
