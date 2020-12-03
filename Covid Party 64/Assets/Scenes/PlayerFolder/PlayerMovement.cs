using Stats;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float moveSpeed = PlayerStat.Speed;
    private float jumpForce = PlayerStat.Jump;

    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheck;


    private float groundCheckRadius;
    private LayerMask collisionLayer;

    private Rigidbody2D rb;
    public Animator animator;
    //private SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    private bool isFacingRight = true;

    public static PlayerMovement instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène.");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        groundCheckRadius = .5f;
        rb = GetComponent<Rigidbody2D>();
        collisionLayer = LayerMask.GetMask("Foundation");
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("Jump", true);
            isJumping = true;
        }
        animator.SetBool("Jump", !isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);     
    }

    private void FixedUpdate()
    {   
        UpdateBonusEffect();
        MovePlayer(horizontalMovement);
        float characterVeclocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVeclocity);
    }

    void MovePlayer(float _horizontalMovement)
    {
        
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(_horizontalMovement > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(_horizontalMovement < 0 && isFacingRight)
        {
            Flip();
        }

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);        
        
    }

    private void UpdateBonusEffect()
    {
        if (moveSpeed != PlayerStat.Speed)
        {
            moveSpeed = PlayerStat.Speed;
        }
        else if (jumpForce != PlayerStat.Jump)
        {
            jumpForce = PlayerStat.Jump;
        }
    }

    public void PlayerMovementStop()
    {
        Debug.Log("PlayerMovementStopCalled");
        if (!animator.GetBool("DeathPlayer")) 
        { 
            Debug.Log("Animation Death set");
            animator.SetBool("DeathPlayer", true);
            animator.SetTrigger("Death");
        }
        
    }
}
