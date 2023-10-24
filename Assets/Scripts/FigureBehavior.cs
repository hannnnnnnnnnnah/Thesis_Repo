using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    [SerializeField] Collider dist1, dist2, dist3;
    [SerializeField] GameObject loc1, loc2;
    [SerializeField] float speed;
    float distRange = 10f;

    Vector3 pos2;

    bool move = false;
    bool push = false;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (dist1.bounds.Intersects(other.bounds))
            { 
                pos2 = loc1.transform.position;
                move = true;
                dist1.enabled = false;
            }

            if (dist2.bounds.Intersects(other.bounds))
            {
                pos2 = loc2.transform.position;
                move = false;
                dist2.enabled = false;
   
            }

            if (dist3.bounds.Intersects(other.bounds))
            {
                move = false;
                Debug.Log("wooaoaoaooao push");
                dist3.enabled = false;
                animator.SetBool("Push", true);
            }
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
