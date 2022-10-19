using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuplicate : MonoBehaviour
{
    /* public int health;
    public float moveSpeed;
    public float jumpSpeed;
    public int jumpLeft;
    public Vector2 climbJumpForce;
    public float fallSpeed;
    public float sprintSpeed;
    public float sprintTime;
    public float sprintInterval;
    public float attackInterval;

    public Color invulnerableColor;
    public Vector2 hurtRecoil;
    public float hurtTime;
    public float hurtRecoverTime;
    public Vector2 deathRecoil;
    public float deathDelay;

    public Vector2 attackUpRecoil;
    public Vector2 attackForwardRecoil;
    public Vector2 attackDownRecoil;*/

/*
    public GameObject attackUpEffect;
    public GameObject attackForwardEffect;
    public GameObject attackDownEffect;*/

    

    
   // private bool _isClimb;
   /* private bool _isSprintable;
    private bool _isSprintReset;
    private bool _isInputEnabled;
    private bool _isFalling;
    private bool _isAttackable;

    private float _climbJumpDelay = 0.2f;
    private float _attackEffectLifeTime = 0.05f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;*/


   
    float hor;
    float ver;
    private Vector3 inputJugador;
    private Vector3 movJugador;
    private Rigidbody2D body;

    private float baseGravity;

    private float timeCanDash = 1f;
    private bool isDashing;
    private bool canDash = true;

    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    private Vector2 direction;

    private bool isGrounded;
    private float jumpForce = 4f;
    private Transform checkGround;
    private float rayCastLength;
    private LayerMask groundLayer;

    [Header("Dash")]
    private float dashingTime = 0.2f;
    private float dashVelocity = 20f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        baseGravity = body.gravityScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        direction = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        
        /*bool isFlipped = direction.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, isFlipped ? 180f : 0f, 0f));*/
        if(!isDashing)
        {
        move();
        }  
      
    }

    void FixedUpdate()
    {
         
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Dash());
        } 
        if(!isDashing)
        {
        Jump();
        }

       // flipCharacter();

    }

    void move()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if(Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
       /* // calculate movement
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
        // set velocity
        Vector2 newVelocity;
        newVelocity.x = horizontalMovement;
        newVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = newVelocity;
         float moveDirection = -transform.localScale.x * horizontalMovement;
            if (moveDirection < 0)
            {
                // flip player sprite
                Vector3 newScale;
                newScale.x = horizontalMovement < 0 ? 1 : -1;
                newScale.y = 1;
                newScale.z = 1;

                transform.localScale = newScale;
            }*/
    }

    private IEnumerator Dash()
    {
        if( canDash)
        {
        isDashing = true;
        canDash = false;
        body.gravityScale = 0f;
        body.velocity =  new Vector2(direction.x * dashVelocity, 0);
        yield return new WaitForSeconds (dashingTime);
        isDashing = false;
        body.gravityScale = baseGravity;
        yield return new WaitForSeconds (timeCanDash);
        canDash = true;
        }
    }

    void Jump()
    {
       // isGrounded = Physics2D.Raycast(checkGround.position, Vector2.down, rayCastLength, groundLayer);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            body.velocity = Vector2.up * jumpForce;
        }
    }

  /*  void flip()
    {
        transform.Rotate(0,180,0);
       Vector3 currentScale = gameObject.transform.localScale;
       currentScale.x *=-1;
       gameObject.transform.locale = currentScale;
        isFlipped = !isFlipped;
       
    }

    void flipCharacter()
    {
        if(direction < 0 && isFlipped )
        {
            flip();
        }
        else if (direction > 0 && !isFlipped)
        {
            flip();
        }
    }*/

}
