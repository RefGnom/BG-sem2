using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed = 6;
    public float sneakSpeed = 2;
    float speed;
    public float gravity = -50;
    public float jumpHeight = 3;

    public Transform groundCheck;
    public Animator animator;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool IsPaused => GameManager.instance.PauseManager.IsPaused;

    void Start()
    {
        speed = walkSpeed;
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
        var sneak = Input.GetButton("Sneak");
        animator.SetBool("IsSneak", sneak);
        if (sneak)
            speed = sneakSpeed;
        else
            speed = walkSpeed;
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
