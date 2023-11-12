using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FigureDisappear : MonoBehaviour
{
    GameObject player;
    public bool approachPlayer;
    bool approaching = false;

    [SerializeField] float speed;

    int LayerIgnoreRaycast;

    Animator animator;

    void Start()
    {
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        if(Vector3.Distance(player.transform.position, transform.position) <= 15f && !approachPlayer) 
        { 
            approachPlayer = true;
        }

        if (approachPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, step);
            //Vector3 newDirection = Vector3.RotateTowards(transform.position, player.transform.position, step * .1f, 0.0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
            //transform.rotation = Quaternion.LookRotation(player.transform.position);
        }

        if (!approachPlayer && approaching)
        {
            approaching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            approachPlayer = false;
            Debug.Log("caught");
        }
    }

    public void Disappear()
    {
        animator.SetBool("Disappear", true);
        gameObject.layer = LayerIgnoreRaycast;
    }

    public void Die()
    {
        animator.SetBool("Disappear", true);
        gameObject.layer = LayerIgnoreRaycast;

        //Sanity is decreased
        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();
    }
}
