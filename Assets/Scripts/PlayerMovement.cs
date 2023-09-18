using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float sprintSpeed, walkSpeed, gravity;
    [SerializeField] int minAngle, maxAngle;

    public int sensitivity = 140;

    private float speed, stepRate, stepCoolDown, stepRateSet;
    private Vector3 camRotation, moveDirection;
    private Camera mainCamera;

    [SerializeField] AudioSource audioFoot;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        speed = walkSpeed;
    }

    private void Start()
    {
        RespawnManager.instance.gameStart = true;
        RespawnManager.instance.exitTriggered = false;
    }

    void Update()
    {
        Move();
        Rotate();
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

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
                stepRateSet = 0.25f;

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

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * (Input.GetAxis("Mouse X")));

        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        mainCamera.transform.localEulerAngles = camRotation;
    }
}