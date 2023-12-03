using System.Collections;
using UnityEngine;

public class FigureDisappear : MonoBehaviour
{
    [SerializeField] float speed, sightDistance, hearingDistance, alertTime;
    [SerializeField] AudioSource scream;

    public bool approachPlayer, patrol1;
    public LayerMask checkRaycast;

    bool approaching, alertTimeDecreasing, figureDespawning, playerLeft = false;
    int LayerIgnoreRaycast;
    float alertTimeSet = 4f;

    GameObject player;
    Animator animator;

    void Start()
    {
        alertTime = alertTimeSet;
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (patrol1)
            animator.SetBool("BackForth", true);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        float rotateStep = 165f * Time.deltaTime;

        //raycasting stuff

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, sightDistance, checkRaycast))
        {
            Debug.Log("figure saw player");
            approachPlayer = true;
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= hearingDistance && !approachPlayer && !figureDespawning)
        {
            animator.SetBool("Aware", true);

            if (!player.GetComponent<PlayerMovement>().isCrouching)
            {
                UIManager.instance.ResetEye("EyeAware", true);
                startAlertTimer();
            }
            else
            {
                //reset everything
                alertTimeDecreasing = false;
                StopAllCoroutines();
                alertTime = alertTimeSet;

                //change UI
                UIManager.instance.ResetEye("EyeVisible", true);
            }

            if (player.GetComponent<PlayerMovement>().isSprinting)
            {
                alertTimeDecreasing = false;
                StopAllCoroutines();

                UIManager.instance.ResetEye("EyeRed", true);

                alertTime = 0;
            }
        }
        else
        {
            if (!playerLeft)
            {
                animator.SetBool("Aware", false);
                PlayerLeft();
                playerLeft = true;
            }
        }

        //rotate towards the player

        if (alertTime == 0 && !PlayerMovement.instance.inTracks)
        {
            UIManager.instance.ResetEye("EyeRed", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, -rotateStep);
        }

        //approach the player

        if (approachPlayer)
        {
            animator.SetBool("BackForth", false);
            animator.SetBool("Aware", true);
            UIManager.instance.ResetEye("EyeRed", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, -rotateStep);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, step);
        }
       
        //kill player when close enough

        if(Vector3.Distance(player.transform.position, transform.position) <= 3f && !figureDespawning)
            Disappear();
    }

    public void startAlertTimer()
    {
        if (!alertTimeDecreasing)
            StartCoroutine(AlertTime());
    }

    public IEnumerator AlertTime()
    {
        while (alertTime > 0)
        {
            alertTimeDecreasing = true;
            yield return new WaitForSeconds(1f);
            alertTime--;
            Debug.Log(alertTime);
        }
    }

    public void Disappear()
    {
        figureDespawning = true;

        approachPlayer = false;
        Debug.Log("caught");

        //reset everything
        alertTimeDecreasing = false;
        StopAllCoroutines();
        alertTime = alertTimeSet;

        //change UI
        UIManager.instance.ResetEye("EyeVisible", false);

        //Sanity is decreased

        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        animator.SetBool("Disappear", true);
        gameObject.layer = LayerIgnoreRaycast;

        StartCoroutine(Despawn());
    }

    public void Die()
    {
        UIManager.instance.ResetEye("EyeVisible", false);

        figureDespawning = true;
        animator.SetBool("Disappear", true);
        //scream.Play();
        gameObject.layer = LayerIgnoreRaycast;

        Destroy(gameObject);
        //StartCoroutine(Despawn());

        figureDespawning = false;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
        Debug.Log("despawned");
    }

    public void PlayerLeft()
    {
        //reset everything
        alertTimeDecreasing = false;
        StopAllCoroutines();
        alertTime = alertTimeSet;

        //change UI
        UIManager.instance.ResetEye("EyeVisible", false);

        approachPlayer = false;
        playerLeft = false;
    }
} 

