using System.Collections;
using UnityEngine;

public class FigureDisappear : MonoBehaviour
{
    [SerializeField] float speed, sightDistance, hearingDistance, alertTime;
    [SerializeField] AudioSource scream;

    public bool approachPlayer;
    public LayerMask checkRaycast;

    bool approaching, alertTimeDecreasing = false;
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

        if (Vector3.Distance(player.transform.position, transform.position) <= hearingDistance && !approachPlayer) 
        {
            UIManager.instance.animator.SetBool("ShowImage", true);

            if (player.GetComponent<PlayerMovement>().isSprinting)
            {
                Debug.Log("this is working :3");
                alertTimeDecreasing = false;
                StopAllCoroutines();
                UIManager.instance.animator.SetBool("EyeAwake", true);
                alertTime = 0;
            }

            if (!player.GetComponent<PlayerMovement>().isCrouching) 
            {
                UIManager.instance.animator.SetBool("EyeAwake", true);
                startAlertTimer();
            }
            else
            {
                Debug.Log("#1 works");
                //reset everything

                alertTimeDecreasing = false;
                StopAllCoroutines();
                alertTime = alertTimeSet;

                //change UI

                UIManager.instance.animator.SetBool("EyeAwake", false);
                UIManager.instance.animator.SetBool("EyeAware", false);
                UIManager.instance.animator.SetBool("ShowImage", false);
            }
        }

        //approach the player

        if (alertTime == 0)
        {
            approachPlayer = true;

            UIManager.instance.animator.SetBool("EyeAware", true);

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
        if(!alertTimeDecreasing)
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
        if(other.tag == "Player")
        {
            approachPlayer = false;
            Debug.Log("caught");

            //Turn off alert & reset time

            UIManager.instance.animator.SetBool("EyeAware", false);
            UIManager.instance.animator.SetBool("EyeAwake", false);
            UIManager.instance.animator.SetBool("ShowImage", false);
            alertTime = alertTimeSet;

            //Sanity is decreased

            InteractionManager.instance.sanity--;
            InteractionManager.instance.UpdateSanity();

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
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
