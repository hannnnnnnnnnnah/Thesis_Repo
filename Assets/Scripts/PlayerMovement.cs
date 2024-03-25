using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float sprintSpeed, walkSpeed, sneakSpeed, gravity;
    [SerializeField] int minAngle, maxAngle, sensitivity;
    [SerializeField] AudioSource audioFoot, audioBreath;

    public float speed;
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

            stepRateSet = stepRate;

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