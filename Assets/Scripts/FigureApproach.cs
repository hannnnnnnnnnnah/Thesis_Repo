using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FigureApproach : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] AudioSource steps, buzz, laugh, scream, injured;

    public bool dying, approaching, gettingInjured = false;
    public bool approachPlayer;
    public float speed;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        float rotateStep = 165f * Time.deltaTime;

        if (approachPlayer && !PlayerMovement.instance.inTracks)
        {
            if (!approaching)
                setAnim();

            Debug.Log("I AM CHASING YOU");

            transform.position = Vector3.Lerp(transform.position, PlayerMovement.instance.gameObject.transform.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, PlayerMovement.instance.gameObject.transform.rotation, rotateStep);
        }

        if (!approachPlayer && approaching)
        {
            animator.SetBool("Chase", false);
            steps.Stop();
            buzz.Stop();
            approaching = false;
        }

        if (health <= 0 && !dying)
            StartCoroutine(Die());

        if (Vector3.Distance(PlayerMovement.instance.gameObject.transform.position, transform.position) <= 3f && !PlayerMovement.instance.inTracks)
        {
            StartCoroutine(emmaRevenge());
        }
    }

    public void Despawn()
    {
        InteractionManager.instance.emmaSpawned = false;
        Destroy(gameObject);
    }

    public IEnumerator Injure()
    {
        gettingInjured = true;
        Debug.Log("emma health:" + " " + health);
        health--;
        animator.SetBool("Injure", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Injure", false);
        gettingInjured = false;
    }

    public IEnumerator Die()
    {
        InteractionManager.instance.emmaSpawned = false;
        dying = true;
        approaching = false;
        approachPlayer = false;
        scream.Play();
        animator.SetBool("Die", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void setAnim()
    { 
        animator.SetBool("Chase", true);
        steps.Play();
        buzz.Play();
        approaching = true;
    }

    IEnumerator emmaRevenge()
    {
        approaching = false;
        approachPlayer = false;
        steps.Stop();

        //revenge
        PlayerMovement.instance.gameObject.GetComponent<CharacterController>().enabled = false;
        Debug.Log("GOT YOU");
        PlayerMovement.instance.gameObject.transform.position = GameObject.FindGameObjectWithTag("Revenge").GetComponent<Transform>().position;
        PlayerMovement.instance.gameObject.GetComponent<CharacterController>().enabled = true;

        laugh.Play();
        yield return new WaitForSeconds(1f);
        Despawn();
    }
}
