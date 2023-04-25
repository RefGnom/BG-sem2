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

    Vector3 _velocity;
    bool _isGrounded;

    void Start()
    {
        speed = walkSpeed;
    }

    void Gravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
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
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
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
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }
    }

    void Update()
    {
        Move();
        Sneak();
        Jump();
        Gravity();
        CheckGround();
    }
}
