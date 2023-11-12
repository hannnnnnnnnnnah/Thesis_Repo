using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator ghostAnim;
    [SerializeField] GameObject figures;

    float distRange = 10f;

    Vector3 pos2;

    bool move = false;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            move = false;
            animator.SetBool("Push", true);
            ghostAnim.SetBool("Spawn", true);
            Scene1Manager.instance.figureKilled = true;
            Scene1Manager.instance.TriggerLevelSwitch();

            //spawn figures
            figures.SetActive(true);

            //Sanity is decreased
            InteractionManager.instance.sanity--;
            InteractionManager.instance.UpdateSanity();
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
