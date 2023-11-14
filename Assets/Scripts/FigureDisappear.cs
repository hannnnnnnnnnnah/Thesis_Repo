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

    [SerializeField] float speed, sightDistance, hearingDistance;
    [SerializeField] AudioSource scream;

    public LayerMask checkRaycast;

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

        //raycasting stuff

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, sightDistance, checkRaycast))
        {
            if (hitData.collider.gameObject.tag == "Player")
                approachPlayer = true;
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= hearingDistance && !approachPlayer && player.GetComponent<PlayerMovement>().speed > 3f) 
        { 
            approachPlayer = true;

            if (!UIManager.instance.flashlightShown)
            {
                UIManager.instance.ShowText("Toggle flashlight with left mouse button");
                UIManager.instance.flashlightShown = true;
            }
        }

        if (approachPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, step);
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

            //Sanity is decreased
            InteractionManager.instance.sanity--;
            InteractionManager.instance.UpdateSanity();

            //turn off flashlight
            other.GetComponent<PlayerMovement>().flashlight.intensity = 0;
            other.GetComponent<PlayerMovement>().speed = 5;

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
