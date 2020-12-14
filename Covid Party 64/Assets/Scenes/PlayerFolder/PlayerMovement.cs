using Stats;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Stats
    private float moveSpeed = PlayerStat.Speed;
    private float jumpForce = PlayerStat.Jump;
    //Test booleans
    private bool isJumping;
    private bool isGrounded;
    private bool isFacingRight = true;
    //GroundCheck variables
    public Transform groundCheck;
    private float groundCheckRadius;
    //Layers
    private LayerMask collisionLayer;
    private LayerMask collisionEnemy;
    //Player components
    private Rigidbody2D rb;
    public Animator animator;
    //Variables
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    //Instance
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
        //Layer gesture and initialization
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        collisionLayer = LayerMask.GetMask("Foundation");
        collisionEnemy = LayerMask.GetMask("Enemy");
        //Components initialization
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheckRadius = .5f;
    }

    void Update()
    {
        //Check if the player is grounded to allow jump or not
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionEnemy);
        //Calculate player speed
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //Check if Player can jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //Set info that player wants to jump
            animator.SetBool("Jump", true);
            isJumping = true;
        }
        animator.SetBool("Jump", !isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);     
    }

    private void FixedUpdate()
    {   
        //Update stat from bonuses active
        UpdateBonusEffect();
        //Add force to the player to make it move
        MovePlayer(horizontalMovement);
        float characterVeclocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVeclocity);

        //Slow down the player speed if enemies at proximity
        if(Stats.EnemyStatSmall.Slow == true || Stats.EnemyStatMedium.Slow == true || Stats.EnemyStatLarge.Slow == true)
        {
            if(EnemyDetection.instance.nbrEnemySmall+ EnemyDetection.instance.nbrEnemyMedium+ EnemyDetection.instance.nbrEnemyBig > 0)
            {
                moveSpeed = moveSpeed * 0.75f;
            }
            else
            {
                moveSpeed = PlayerStat.Speed;
            }
        }
        
    }


    //Add force to the player to make it move
    void MovePlayer(float _horizontalMovement)
    {
        
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);


        //Give the sens of  the player sprite (and animator) 
        //positive -> player look at the right
        //negative -> player look at the right
        if(_horizontalMovement > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(_horizontalMovement < 0 && isFacingRight)
        {
            Flip();
        }

        //Make player jump
        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    //Rotate the player sprite
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Gizmos used to detect the ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);        
        
    }


    //Update stats from PlayerStat
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


    //Play death animation
    public void PlayerMovementStop()
    {
        if (!animator.GetBool("DeathPlayer")) 
        { 
            animator.SetBool("DeathPlayer", true);
            animator.SetTrigger("Death");
        }
        
    }
}
