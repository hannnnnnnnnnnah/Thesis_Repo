using UnityEngine;

public class PlayerMovementOld : MonoBehaviour
{
    [SerializeField] float dragControl, sensitivity, minRotation, maxRotation, playerHeight, horizontalMovement, verticalMovement;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource audioFoot, flashback;

    private float mouseX, mouseY, xRotation, yRotation, stepRate, stepRateSet, stepCoolDown;
    private float mult = 0.01f;
    public float speed;
    float groundDistance = 0.4f;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;
    RaycastHit hitSlope;

    bool isGrounded;
    public bool move, inTracks;
    public Animator animator;

    [SerializeField] Camera mainCamera;

    public static PlayerMovementOld instance;
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

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool OnSlope()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out hitSlope, Mathf.Infinity, ground))
        {
            if (hitSlope.normal != Vector3.up)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, playerHeight / 2, 0), groundDistance, ground);

        CheckInput();
        ControlDrag();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, hitSlope.normal);

        //camera

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivity * mult;
        xRotation -= mouseY * sensitivity * mult;

        xRotation = Mathf.Clamp(xRotation, minRotation, maxRotation);

        mainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void FixedUpdate()
    {
        if (move)
        {
            MovePlayer();
        }
    }

    void CheckInput()
    {
        //movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = Vector3.forward * verticalMovement + Vector3.right * horizontalMovement;
    }

    void MovePlayer()
    {
        stepRate = 0.3f;

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * speed, ForceMode.Acceleration);

            stepRateSet = stepRate;

            //step audio

            stepCoolDown -= Time.deltaTime;

            if (rb.velocity.magnitude > 0 && stepCoolDown < 0f)
            {
                audioFoot.pitch = 1f + Random.Range(-0.1f, 0.1f);
                audioFoot.Play();
                stepCoolDown = stepRateSet;
            }
        }
        else if (isGrounded && OnSlope())
            rb.AddForce(slopeMoveDirection.normalized * speed, ForceMode.Acceleration);
    }

    void ControlDrag()
    {
        rb.drag = dragControl;
    }

}
