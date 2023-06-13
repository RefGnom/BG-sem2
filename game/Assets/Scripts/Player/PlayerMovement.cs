using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    public float walkSpeed = 6;
    public float sneakSpeed = 2;
    float speed;
    public float gravity = -50;
    public float jumpHeight = 3;

    public Transform groundCheck;
    public Animator animator;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private float sneakHeightDelta;
    private bool IsPaused => GameManager.Instance.PauseManager.IsPaused;

    void Start()
    {
        var collider = GetComponent<Collider>();
        collider.tag = "Player";
        speed = walkSpeed;
        sneakHeightDelta = controller.height - controller.radius * 2;
    }

    void Update()
    {
        if (IsPaused)
            return;
        Move();
        Sneak();
        Jump();
        Gravity();
        CheckGround();
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x == 0 && z == 0)
            animator.SetBool("IsRun", false);
        else
            animator.SetBool("IsRun", true);

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    void Sneak()
    {
        if (Input.GetButtonDown("Sneak"))
        {
            controller.height -= sneakHeightDelta;
            controller.center -= new Vector3(0, sneakHeightDelta / 2, 0);
            EnemyContollor.lookRadius = EnemyContollor.sneakLookRadius;
            speed = sneakSpeed;
            animator.SetBool("IsSneak", true);
        }
        if (Input.GetButtonUp("Sneak"))
        {
            controller.height += sneakHeightDelta;
            controller.center += new Vector3(0, sneakHeightDelta / 2, 0);
            EnemyContollor.lookRadius = EnemyContollor.defaultLookRadius;
            speed = walkSpeed;
            animator.SetBool("IsSneak", false);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
    }
}
