using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float sprintSpeed, walkSpeed, gravity, sprintLength, sprintDelay;
    [SerializeField] int minAngle, maxAngle;

    [SerializeField] bool sprintDisabled, isSprinting = false;

    public int sensitivity = 140;

    public LayerMask checkRaycast;

    private float speed, stepRate, stepCoolDown, stepRateSet;
    private Vector3 camRotation, moveDirection;
    private Camera mainCamera;

    [SerializeField] AudioSource audioFoot, audioBreath, other_steps, whisper, voice;
    [SerializeField] GameObject SurroundSound;

    Light flashlight;

    /*private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        flashlight = GetComponentInChildren<Light>();

        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        speed = walkSpeed;
    }*/

    private void Start()
    {
        gameObject.transform.position = RespawnManager.instance.spawnPoint;
        //RespawnManager.instance.gameStart = true;

        characterController = GetComponent<CharacterController>();
        flashlight = GetComponentInChildren<Light>();

        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        speed = walkSpeed;
    }


    public void StartSurroundSound()
    {
        SurroundSound.GetComponent<Animator>().SetBool("Rotate", true);

        if(InteractionManager.instance.steps)
            other_steps.Play();
        if(InteractionManager.instance.whisper)
            whisper.Play();

        if (!InteractionManager.instance.steps)
        {
            SurroundSound.GetComponent<Animator>().SetBool("Rotate", false);
            other_steps.Stop();
            whisper.Stop();
        }
            
    }

    public void StopSurroundSound()
    {
        SurroundSound.GetComponent<Animator>().SetBool("Rotate", false);
    }

    void Update()
    {
        Move();
        Rotate();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(flashlight.intensity == 0)
                flashlight.intensity = 300;

            else if (flashlight.intensity != 0)
                flashlight.intensity = 0;
        }
            
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!voice.isPlaying) 
            {
                voice.Play();
            }
        }

        //raycasting stuff

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 15f, checkRaycast))
        {
            if (hitData.collider.gameObject.tag == "Figure" && !hitData.collider.gameObject.GetComponent<Animator>().GetBool("Disappear") && flashlight.intensity > 0)
                hitData.collider.gameObject.GetComponent<FigureDisappear>().Die();

            if (hitData.collider.gameObject.tag == "Figure" && !hitData.collider.gameObject.GetComponent<Animator>().GetBool("Disappear") && voice.isPlaying)
                hitData.collider.gameObject.GetComponent<FigureDisappear>().Disappear();
        }

    }

    private void Move()
    {
        stepRate = 0.4f;

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(horizontalMove, 0, verticalMove);
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetKey(KeyCode.LeftShift) && !sprintDisabled)
            {
                speed = sprintSpeed;
                stepRateSet = 0.25f;

                if (!isSprinting)
                    StartCoroutine(Sprint());
            }
            else
            {
                speed = walkSpeed;
                stepRateSet = stepRate;
            }

            //step audio

            stepCoolDown -= Time.deltaTime;

            //&& characterController.velocity.y == 0

            if (characterController.velocity.magnitude > 0 && characterController.velocity.y == 0 && stepCoolDown < 0f)
            {
                audioFoot.pitch = 1f + Random.Range(-0.1f, 0.1f);
                audioFoot.Play();
                stepCoolDown = stepRateSet;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if (characterController.enabled)
        {
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
    }

    IEnumerator Sprint()
    {
        //Debug.Log("Sprint started");

        isSprinting = true;
        yield return new WaitForSeconds(sprintLength);
        sprintDisabled = true;
        audioBreath.Play();
        yield return new WaitWhile(() => Input.GetKey(KeyCode.LeftShift));

        //Debug.Log("shift is not being spammed");

        yield return new WaitForSeconds(sprintDelay);
        sprintDisabled = false;
        isSprinting = false;
        audioBreath.Stop();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * (Input.GetAxis("Mouse X")));

        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        mainCamera.transform.localEulerAngles = camRotation;
    }
}