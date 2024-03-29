using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public AudioSource[] audioSources;

    // Movement variables
    public float runSpeed = 10f;
    public float walkSpeed = 5f;
    private float moveSpeed; 
    private float horizontalMove;
    private float verticalMove;

    // Jump variables
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool isAttack;


    // Atack variables
    public Transform atackPoint;
    public float atackRange = 0.5f;
    public LayerMask enemyLayer;


    private void Awake()
    {
        moveSpeed = walkSpeed;
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get horizontal input for movement
        horizontalMove = Input.GetAxisRaw("HorizontalArrow");
        verticalMove = Input.GetAxisRaw("VerticalArrow");

        if (Input.GetKey(KeyCode.RightControl))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        if (!isAttack)
        {
            // Flip character sprite if moving left
            if (horizontalMove < 0)
            {
                Quaternion target = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1000);
            }
            else if (horizontalMove > 0)
            {
                Quaternion target = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1000);
            }

            // Set animator parameters based on movement
            animator.SetFloat("Speed", (Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove)) * moveSpeed);


            // Jump
            /* if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetTrigger("Jump");
                isGrounded = false;
            } */
        }


        // Atack
        if (Input.GetKeyDown(KeyCode.Keypad7) && isGrounded)
        {
            Attack1();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) && isGrounded)
        {
            Attack2();
        }
    }

    private void FixedUpdate()
    {
        if (!isAttack && isGrounded)
        {
            // Move the character
            rb.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed).normalized * 10;
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

    public void Attack3()
    {
        isAttack = true;
        moveSpeed = 0;
        animator.SetTrigger("Attack3");
    }

    public void Damage(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atackPoint.position, atackRange, enemyLayer);
        if (hitEnemies.Length > 0)
        {
            audioSources[0].Play();
        }
        else
        {
            audioSources[1].Play();
        }
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public void Protect()
    {
        animator.SetTrigger("Protect");
    }

    public void stopAtack()
    {
        isAttack = false;
    }

    public void stopJump()
    {
        isGrounded = true;
    }

    void OnDrawGizmosSelected()
    {
        if (atackPoint == null)
            return;

        Gizmos.DrawWireSphere(atackPoint.position, atackRange);
    }
}

