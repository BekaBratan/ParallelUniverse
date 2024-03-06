using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Movement variables
    public float runSpeed = 10f;
    public float walkSpeed = 5f;
    private float moveSpeed;
    private float horizontalMove;


    // Jump variables
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool isAttack;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = walkSpeed;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        // Get horizontal input for movement
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        } else
        {
            moveSpeed = walkSpeed;
        }

        if (!isAttack)
        {
            // Flip character sprite if moving left
            if (horizontalMove < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (horizontalMove > 0)
            {
                spriteRenderer.flipX = false;
            }

            // Set animator parameters based on movement
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove) * moveSpeed);


            // Jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("isJumping", true);
            } else if (isGrounded)
            {
                animator.SetBool("isJumping", false);
            }
        }


        // Atack
        if (Input.GetMouseButton(0) && isGrounded)
        {
            Attack1();
        }
        if (Input.GetMouseButton(1) && isGrounded)
        {
            Attack2();
        }
    }

    private void FixedUpdate()
    {
        if (!isAttack)
        {
            // Move the character
            rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
        }
    }

    // You can call these methods from other scripts to trigger specific animations
    public void Attack1()
    {
        isAttack = true;
        moveSpeed = 0;
        animator.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        isAttack = true;
        moveSpeed = 0;
        animator.SetTrigger("Attack2");
    }


    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void Protect()
    {
        animator.SetTrigger("Protect");
    }

    public void stopAtack()
    {
        isAttack = false;
    }
}