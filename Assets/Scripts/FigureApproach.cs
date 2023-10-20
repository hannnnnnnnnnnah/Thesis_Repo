using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FigureApproach : MonoBehaviour
{
    GameObject player;
    public bool approachPlayer;
    bool approaching = false;
    Animator animator;

    [SerializeField] AudioSource steps, buzz, laugh;

    [SerializeField] float speed;

    [SerializeField] GameObject strand;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        if (approachPlayer)
        {
            if (!approaching)
                setAnim();

            Debug.Log("I AM CHASING YOU");

            transform.position = Vector3.Lerp(transform.position, player.transform.position, step);
            Vector3 newDirection = Vector3.RotateTowards(transform.position, player.transform.position, step * .5f, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        if (!approachPlayer && approaching)
        {
            animator.SetBool("Chase", false);
            steps.Stop();
            buzz.Stop();
            approaching = false;
        }     
    }

    void setAnim()
    { 
        animator.SetBool("Chase", true);
        steps.Play();
        buzz.Play();
        approaching = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            approaching = false;
            approachPlayer = false;
            steps.Stop();
            other.GetComponent<CharacterController>().enabled = false;
            Debug.Log("GOT YOU");
            StartCoroutine(emmaRevenge());
            //InteractionManager.instance.sanity = -5;

        }
    }

    IEnumerator emmaRevenge()
    {
        laugh.Play();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
