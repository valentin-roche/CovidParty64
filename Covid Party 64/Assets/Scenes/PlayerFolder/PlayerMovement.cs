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
    //GroundCheck variables
    public Transform groundCheck;
    private float groundCheckRadius;
    //Layers
    private LayerMask collisionLayer;
    private LayerMask collisionEnemy;

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
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        groundCheckRadius = .5f;
        rb = GetComponent<Rigidbody2D>();
        collisionLayer = LayerMask.GetMask("Foundation");
        collisionEnemy = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        //si une fondation ou un ennemi est reperé dans le cercle, isGrounded = true
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionEnemy);

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;


        //fait sauter le joueur
        //et active l'animation
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
        //mouvement du joueur
        MovePlayer(horizontalMovement);
        float characterVeclocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVeclocity);

        //?????????????????????????????????????????????????????????
        //permet de ralentir le joueur quand des ennemis sont présents dans le circle collider?
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


    //permet de faire déplacer le joueur horizontalement ainsi que de le faire sauter
    void MovePlayer(float _horizontalMovement)
    {
        
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);


        //donne le sens du sprite en fonction du signe de la vitesse 
        //si vitesse positive -> joueur regarde à droite
        //si vitesse négative -> joueur regarde à gauche
        if(_horizontalMovement > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(_horizontalMovement < 0 && isFacingRight)
        {
            Flip();
        }

        // si boolean est vrai alors le joueur saute
        //boolean vrai si la fondation et repéré et si le joueur appui sur la touche espace
        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    //rotation du sprite du joueur
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Gizmos qui sera utilisé pour la detection des fondations
    //et permettre le saut du joueur
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);        
        
    }


    //recupère les valeurs de playerStat
    //permet ainsi d'être à jour si il y a eu modification 
    //du à un bonus
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


    //permet l'animation de mort du joueur
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
