using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed, gravity;
    [SerializeField] int minAngle, maxAngle;
    [SerializeField] AudioSource audioFoot, flashback;

    public Slider slider;
    public float sensitivity = 100f;

    public float speed;
    public bool inTracks, rotate, move, riding;
    public LayerMask checkRaycast;
    public Animator animator;

    public DialogueRunner dr;

    public Vector3 moveDirection;

    private float stepRate, stepCoolDown, stepRateSet;
    private Vector3 camRotation;
    public Camera mainCamera;

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
        slider.value = sensitivity / 10;

        //Sets player position to spawn point
        gameObject.transform.position = RespawnManager.instance.spawnPoint;


        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        speed = walkSpeed;
    }

    public void AdjustSpeed(float newSpeed)
    {
        sensitivity = newSpeed * 10;
    }

    private void FixedUpdate()
    {
        if (move)
        {
            Move();
            Rotate();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Rideable"))
        {
            riding = true;
            gameObject.transform.SetParent(hit.transform);
        }
    }

    private void Move()
    {
        stepRate = 0.25f;

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(horizontalMove, 0, verticalMove);
            moveDirection = transform.TransformDirection(moveDirection);

            stepRateSet = stepRate;

            //step audio

            stepCoolDown -= Time.fixedDeltaTime;

            if (characterController.velocity.magnitude > 0 && stepCoolDown < 0f)
            {
                audioFoot.pitch = 1f + Random.Range(-0.1f, 0.1f);
                audioFoot.Play();
                stepCoolDown = stepRateSet;
            }
        }

        moveDirection.y -= gravity * Time.fixedDeltaTime;

        if (characterController.enabled)
            characterController.Move(moveDirection * speed * Time.fixedDeltaTime);
    }

    //Camera rotation stuff
    private void Rotate()
    {
        if (Input.GetKey(KeyCode.O))
            Debug.Log(rotate);

        if (rotate)
        {
            transform.Rotate(Vector3.up * sensitivity * Time.fixedDeltaTime * (Input.GetAxis("Mouse X")));

            camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;
            camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

            mainCamera.transform.localEulerAngles = camRotation;
        }
    }

    public void FlashbackReset()
    {
        animator.SetBool("Flashback", false);
    }
}