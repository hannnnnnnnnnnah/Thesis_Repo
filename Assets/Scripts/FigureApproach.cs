using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FigureApproach : MonoBehaviour
{
    GameObject player;
    public bool approachPlayer;
    public bool dying, approaching, gettingInjured = false;
    Animator animator;

    [SerializeField] AudioSource steps, buzz, laugh, scream, injured;
    [SerializeField] float health;
    public float speed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        if (approachPlayer && !player.GetComponent<PlayerMovement>().inTracks)
        {
            if (!approaching)
                setAnim();

            Debug.Log("I AM CHASING YOU");

            transform.position = Vector3.Lerp(transform.position, player.transform.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, step);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !other.GetComponent<PlayerMovement>().inTracks)
        {
            approaching = false;
            approachPlayer = false;
            steps.Stop();

            //revenge
            other.GetComponent<CharacterController>().enabled = false;
            Debug.Log("GOT YOU");
            StartCoroutine(emmaRevenge());
            other.gameObject.transform.position = GameObject.FindGameObjectWithTag("Revenge").GetComponent<Transform>().position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }

    IEnumerator emmaRevenge()
    {
        laugh.Play();
        yield return new WaitForSeconds(1f);
        Despawn();
    }
}
