using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool sprintDisabled = false;
    [SerializeField] float speed, sprintSpeed, walkSpeed, sneakSpeed, gravity;
    [SerializeField] int minAngle, maxAngle, sensitivity;
    [SerializeField] GameObject SurroundSound, camHeight, camCrouch;
    [SerializeField] AudioSource audioFoot, audioBreath, other_steps, whisper, flash_on, flash_off;
    [SerializeField] DeathTimer deathTimer;

    public bool isSprinting, isCrouching, inTracks;
    public LayerMask checkRaycast;
    public Light flashlight;
    public Animator animator;
    public AudioSource flashback;

    private float stepRate, stepCoolDown, stepRateSet;
    private Vector3 camRotation, moveDirection;
    private Camera mainCamera;

    CharacterController characterController;

    public static PlayerMovement instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        //Sets player position to spawn point

        gameObject.transform.position = RespawnManager.instance.spawnPoint;

        //Camera stuff

        mainCamera = Camera.main;
        mainCamera.transform.position = camHeight.transform.position;
        
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        flashlight = GetComponentInChildren<Light>();
        Cursor.lockState = CursorLockMode.Locked;

        speed = walkSpeed;
    }

    public void StartSurroundSound()
    {
        SurroundSound.GetComponent<Animator>().SetBool("Rotate", true);

        if(InteractionManager.instance.steps)
            other_steps.Play();
        if(InteractionManager.instance.whisper)
            whisper.Play();
    }

    public void StopSurroundSound()
    {
        SurroundSound.GetComponent<Animator>().SetBool("Rotate", false);
        other_steps.Stop();
        whisper.Stop();
    }

    void Update()
    {
        Move();
        Rotate();   
    }

    private void Move()
    {
        stepRate = 0.3f;

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
                //isSprinting = true;

                animator.SetBool("StartCrouch", false);
                mainCamera.transform.position = camHeight.transform.position;
            }
            if (Input.GetKey(KeyCode.LeftControl) && !isSprinting)
            {
                isCrouching = true;
                speed = sneakSpeed;
                stepRateSet = 0.6f;

                animator.SetBool("StartCrouch", true);
                mainCamera.transform.position = camCrouch.transform.position;
            }
            else
            {
                isSprinting = false;
                isCrouching = false;

                //Set walk speed to normal
                speed = walkSpeed;
                stepRateSet = stepRate;
                animator.SetBool("StartCrouch", false);
                mainCamera.transform.position = camHeight.transform.position;
            }

            //step audio

            stepCoolDown -= Time.deltaTime;
            //characterController.velocity.y == 0

            if (characterController.velocity.magnitude > 0 && stepCoolDown < 0f)
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

    //Camera rotation stuff
    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * (Input.GetAxis("Mouse X")));

        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        mainCamera.transform.localEulerAngles = camRotation;
    }

    public void StartDeathTimer()
    {
        deathTimer.startDeathTimer();
    }
}