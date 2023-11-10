using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    float distRange = 10f;

    Vector3 pos2;

    bool move = false;
    bool push = false;

    Animator animator;
    [SerializeField] Animator ghostAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            move = false;
            Debug.Log("wooaoaoaooao push");
            animator.SetBool("Push", true);
            ghostAnim.SetBool("Spawn", true);
        }
    }

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        float dist = Vector3.Distance(transform.position, pos2);
        
        if (move)
        {
            animator.SetBool("Turn", false);
            transform.Translate(Vector3.right * step);

            if (dist <= distRange)
                move = false;
        }
        else if(!move)
        {
            animator.SetBool("Turn", true);
        }
    }
}
