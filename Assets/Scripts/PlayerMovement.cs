using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed, sprintSpeed, walkSpeed, sneakSpeed, gravity;
    [SerializeField] int minAngle, maxAngle, sensitivity;
    [SerializeField] GameObject camHeight, camCrouch;
    [SerializeField] AudioSource audioFoot, audioBreath;

    public bool isCrouching, inTracks, rotate;
    public LayerMask checkRaycast;
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

        mainCamera = Camera.main;
        mainCamera.transform.position = camHeight.transform.position;
        
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        speed = walkSpeed;
    }

    private void FixedUpdate()
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

            /*if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouching = true;
                speed = sneakSpeed;
                stepRateSet = 0.6f;

                animator.SetBool("StartCrouch", true);
                mainCamera.transform.position = camCrouch.transform.position;
                characterController.height = 1f;
            }*/
            //else
            //{
                isCrouching = false;
                speed = walkSpeed;
                stepRateSet = stepRate;

                animator.SetBool("StartCrouch", false);
                mainCamera.transform.position = camHeight.transform.position;
                characterController.height = 1.49f;
            //}

            //step audio

            stepCoolDown -= Time.deltaTime;

            if (characterController.velocity.magnitude > 0 && stepCoolDown < 0f)
            {
                audioFoot.pitch = 1f + Random.Range(-0.1f, 0.1f);
                audioFoot.Play();
                stepCoolDown = stepRateSet;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if (characterController.enabled)
            characterController.Move((moveDirection * speed * Time.deltaTime));
    }

    //Camera rotation stuff
    private void Rotate()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * (Input.GetAxis("Mouse X")));

            camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

            mainCamera.transform.localEulerAngles = camRotation;
        }
    }
}