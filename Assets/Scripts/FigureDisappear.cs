using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FigureDisappear : MonoBehaviour
{
    [SerializeField] float speed, sightDistance, hearingDistance, alertTime;
    [SerializeField] AudioSource scream;
    [SerializeField] GameObject[] waypoints;
    [SerializeField] UIFigureTrigger trigger;

    public bool approachPlayer, isPatrolling;
    public LayerMask checkRaycast;

    bool approaching, alertTimeDecreasing, figureDespawning = false;
    int LayerIgnoreRaycast, waypointLoop;
    float alertTimeSet = 2.5f;

    GameObject currentWaypoint;
    Animator animator;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypoint = waypoints[waypoints.Length - 1];
        waypointLoop = 2;

        alertTime = alertTimeSet;
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        float rotateStep = 165f * Time.deltaTime;

        //raycasting stuff

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        if (isPatrolling)
            Patrol();


        if (Physics.Raycast(ray, out hitData, sightDistance, checkRaycast))
        {
            Debug.Log("figure saw player");
            approachPlayer = true;
        }

        if (Vector3.Distance(PlayerMovement.instance.gameObject.transform.position, transform.position) <= sightDistance && !approachPlayer && !figureDespawning)
        {
            animator.SetBool("Aware", true);
            //UIManager.instance.ResetEye("EyeVisible", true);

            if (!PlayerMovement.instance.isCrouching)
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
        }

        //rotate towards the player

        if (alertTime <= 0 && !PlayerMovement.instance.inTracks)
        {
            UIManager.instance.ResetEye("EyeRed", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, PlayerMovement.instance.gameObject.transform.rotation, -rotateStep);
        }

        //approach the player

        if (approachPlayer)
        {
            animator.SetBool("Aware", true);
            UIManager.instance.ResetEye("EyeRed", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, PlayerMovement.instance.gameObject.transform.rotation, -rotateStep);
            transform.position = Vector3.Lerp(transform.position, PlayerMovement.instance.gameObject.transform.position, step);
        }
       
        //kill player when close enough

        if(Vector3.Distance(PlayerMovement.instance.gameObject.transform.position, transform.position) <= 1.5f && !figureDespawning)
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
        approachPlayer = false;
        Debug.Log("caught");

        //reset everything
        alertTimeDecreasing = false;
        StopAllCoroutines();
        alertTime = alertTimeSet;

        //Sanity is decreased

        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        animator.SetBool("Disappear", true);
        gameObject.layer = LayerIgnoreRaycast;

        StartCoroutine(Despawn());
    }

    public void Die()
    {
        figureDespawning = true;
        animator.SetBool("Disappear", true);
        scream.Play();
        gameObject.layer = LayerIgnoreRaycast;
        figureDespawning = false;
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        figureDespawning = true;
        trigger.disableSelf();

        //change UI
        UIManager.instance.ResetEye("EyeRed", false);
        UIManager.instance.ResetEye("EyeVisible", false);

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

    public void Patrol()
    {
        agent.destination = currentWaypoint.transform.position;
        //Debug.Log(Vector3.Distance(transform.position, currentWaypoint.transform.position));

        if(Vector3.Distance(transform.position, currentWaypoint.transform.position) <= 4.05)
        {
            if (currentWaypoint == waypoints[0])
            {
                waypointLoop = 1;
                currentWaypoint = waypoints[waypoints.Length - 1];  
            }
            else
            {
                waypointLoop++;
                currentWaypoint = waypoints[waypoints.Length - waypointLoop];     
            }
        }
    }

    private void OnDisable()
    {
        if(figureDespawning)
            UIManager.instance.ResetEye("EyeRed", false);
    }
} 