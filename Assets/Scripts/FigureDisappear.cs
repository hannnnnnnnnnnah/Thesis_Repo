using System.Collections;
using UnityEngine;

public class FigureDisappear : MonoBehaviour
{
    [SerializeField] float speed, sightDistance, hearingDistance, alertTime;
    [SerializeField] AudioSource scream;
    [SerializeField] GameObject Stop;

    public bool approachPlayer, isPatrolling;
    public LayerMask checkRaycast;

    bool approaching, alertTimeDecreasing, figureDespawning = false;
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

        if (isPatrolling)
        {
            Patrol(rotateStep, step, Stop);
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= sightDistance && !approachPlayer && !figureDespawning)
        {
            animator.SetBool("Aware", true);
            //UIManager.instance.ResetEye("EyeVisible", true);

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

        //rotate towards the player

        if (alertTime == 0 && !PlayerMovement.instance.inTracks)
        {
            isPatrolling = false;
            UIManager.instance.ResetEye("EyeRed", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, -rotateStep);
        }

        //approach the player

        if (approachPlayer)
        {
            animator.SetBool("Aware", true);
            UIManager.instance.ResetEye("EyeRed", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, -rotateStep);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, step);
        }
       
        //kill player when close enough

        if(Vector3.Distance(player.transform.position, transform.position) <= 1.5f && !figureDespawning)
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
        scream.Play();
        gameObject.layer = LayerIgnoreRaycast;
        figureDespawning = false;
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
        Debug.Log("despawned");
    }

    public void PlayerLeft()
    {
        approachPlayer = false;

        //reset everything
        alertTimeDecreasing = false;
        StopAllCoroutines();
        alertTime = alertTimeSet;

        //reset animation
        animator.SetBool("Aware", false);

        //change UI
        UIManager.instance.ResetEye("EyeVisible", false);
    }

    public void Patrol(float step1, float step2, GameObject point)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, point.transform.rotation, -step1);
        transform.position = Vector3.Lerp(transform.position, point.transform.position, step2);
    }
} 

