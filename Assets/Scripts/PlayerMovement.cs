using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float sprintSpeed, walkSpeed, gravity, sprintLength, sprintDelay;
    [SerializeField] int minAngle, maxAngle;

    [SerializeField] bool sprintDisabled, isSprinting = false;

    public int sensitivity = 140;
    LayerMask layerMask;  

    private float speed, stepRate, stepCoolDown, stepRateSet;
    private Vector3 camRotation, moveDirection;
    private Camera mainCamera;

    [SerializeField] AudioSource audioFoot, audioBreath, other_steps, whisper;

    [SerializeField] GameObject SurroundSound;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        speed = walkSpeed;
    }

    private void Start()
    {
        layerMask = LayerMask.GetMask("Seeable");

        RespawnManager.instance.gameStart = true;
        RespawnManager.instance.exitTriggered = false;
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

        //raycasting stuff
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 10, layerMask))
            print("There is something in front of the object!");
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