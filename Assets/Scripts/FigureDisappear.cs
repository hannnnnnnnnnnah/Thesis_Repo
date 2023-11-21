using System.Collections;
using UnityEngine;

public class FigureDisappear : MonoBehaviour
{
    [SerializeField] float speed, sightDistance, hearingDistance, alertTime;
    [SerializeField] AudioSource scream;

    public bool approachPlayer;
    public LayerMask checkRaycast;

    bool approaching, alertTimeDecreasing, figureDespawning = false;
    int LayerIgnoreRaycast;
    float alertTimeSet = 6f;

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

        //raycasting stuff

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, sightDistance, checkRaycast))
        {
            if (hitData.collider.gameObject.tag == "Player")
                approachPlayer = true;
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= hearingDistance && !approachPlayer && !figureDespawning)
        {
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

        //approach the player

        if (alertTime == 0)
        {
            approachPlayer = true;

            UIManager.instance.ResetEye("EyeRed", true);

            transform.position = Vector3.Lerp(transform.position, player.transform.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, step);
        }

        if (alertTime >= 0 && approaching)
        {
            approaching = false;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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

            figureDespawning = true;
            Disappear();
        }
    }

    public void Disappear()
    {
        animator.SetBool("Disappear", true);
        gameObject.layer = LayerIgnoreRaycast;

        StartCoroutine(Despawn());
    }

    public void Die()
    {
        animator.SetBool("Disappear", true);
        scream.Play();
        gameObject.layer = LayerIgnoreRaycast;

        //Sanity is decreased

        InteractionManager.instance.sanity--;
        InteractionManager.instance.UpdateSanity();

        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void PlayerLeft()
    {
        //reset everything

        alertTimeDecreasing = false;
        StopAllCoroutines();
        alertTime = alertTimeSet;

        //change UI
        UIManager.instance.ResetEye("EyeVisible", false);
    }
} 

