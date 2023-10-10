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

    [SerializeField] AudioSource steps, buzz;

    [SerializeField] float speed;

    public static FigureApproach instance;

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
    }

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
}
