using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class JackController : MonoBehaviour
{

    public bool isDead = false;

    public bool canMove = true;
    public bool _isFacingRight = true;
    public bool isJumping = false;
    public float walkSpeed = 5f;
    Vector2 moveInput;
    [SerializeField] public bool IsMoving { get; private set; }
    [SerializeField] float jumpValue = 9f;
    Rigidbody2D rb;
    Animator anim;
    TouchingDirections touchingDirections;
    Health _health;
    GameObject bloodPref;

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall && canMove)
            {
                return walkSpeed;

                //For walking on the Coffing and ramps, I added a ramp layer in editor, then in TouchingDirections.cs added a new CastFilter to filter ramp layer and added and 
                //a new bool OnRamp, then I came to this script and addid the code below. this is going to filter ramp and allow player to walk and jump into them even IsOnWall is
                //returning true. Also I added IsOnRamp in Jump function.
            } else if (IsMoving && touchingDirections.IsOnWall && touchingDirections.IsOnRamp)
            {
                return walkSpeed;
            }
            else
            {
                return 0;
            }
        }
    }

    public float JumpImpulse
    {
        get
        {
            if (touchingDirections.IsOnWall && Mathf.Abs(moveInput.x) > 0)
            {
                return jumpValue / 2 + 1;
            }
            return jumpValue;
        }
    }


    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            // Flip only if value is new
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite directino
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        bloodPref = Resources.Load("Blood Splash") as GameObject;
        _health = GetComponent<Health>();
        _health.OnTakeDamage += onTakeDamage;
        _health.OnDie += onDie;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      //  anim.SetFloat("Speed", Mathf.Abs(moveInput.x));
        SetFacingDirection(moveInput);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded || touchingDirections.IsOnRamp)
        {
            Debug.Log("We jumped");
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, JumpImpulse);
        }
    }


    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face the left
            IsFacingRight = false;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag != "movingPlatform")
    //    {
    //        transform.SetParent(null);
    //    }
    //}

    private void onTakeDamage()
    {
        Splash();
        anim.SetTrigger("TakeHit");
    }

    void onDie()
    {
        isDead = true;
        anim.SetBool("Death", true);
        gameObject.GetComponent<JackAttack>().enabled = false;
        gameObject.GetComponent<JackController>().enabled = false;
        gameObject.tag = "Dead";
    }

    void Splash()
    {
        Instantiate(bloodPref, gameObject.transform.position, Quaternion.identity);
        Debug.Log(gameObject.transform.position);
    }

}