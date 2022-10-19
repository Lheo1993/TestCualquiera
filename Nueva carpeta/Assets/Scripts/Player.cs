using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float hor;
    float ver;
    private Vector3 inputJugador;
    private Vector3 movJugador;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    private float baseGravity;

    private float timeCanDash = 0.2f;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        } else if (direction.x < 0 )
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Dash());
        } 
        
        if(!isDashing)
        {
        move();
        }  
      
    }

    void FixedUpdate()
    {
           
        if(!isDashing)
        {
        Jump();
        }

    }

    void move()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if(Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
      
    }

    private IEnumerator Dash()
    {
        if(canDash)
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

    /*

     private void updatePlayerState()
    {
        _isGrounded = checkGrounded();
        _animator.SetBool("IsGround", _isGrounded);

        float verticalVelocity = _rigidbody.velocity.y;
        _animator.SetBool("IsDown", verticalVelocity < 0);

        if (_isGrounded && verticalVelocity == 0)
        {
            _animator.SetBool("IsJump", false);
            _animator.ResetTrigger("IsJumpFirst");
            _animator.ResetTrigger("IsJumpSecond");
            _animator.SetBool("IsDown", false);

            jumpLeft = 2;
            _isClimb = false;
            _isSprintable = true;
        }
        else if(_isClimb)
        {
            // one remaining jump chance after climbing
            jumpLeft = 1;
        }
    }*/

   /* private void jumpControl()
    {
        if (!Input.GetButtonDown("Jump"))
            return;

        if (_isClimb)
            climbJump();
        else if (jumpLeft > 0)
            jump();
    }*/


}
