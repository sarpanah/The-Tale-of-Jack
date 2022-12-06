using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{


    Rigidbody2D rb;
    Vector3 velocity = Vector3.zero;
    Animator anim;
    HingeJoint2D hj;

    bool isGround;
    bool doubleJumpAv;

    public GameObject mile;
    public static float horizontalMove = 0;
    public static int movingState = 1; // 0: Stop, 1: moving, 2: swinging, 3: falling
    public Transform wallCheck;
    BoxCollider2D player_box_collider;


    public LayerMask ground;

    bool lookingRight = true;

    bool allowToMove = false;

    public Transform wallOverlapCheck;

    public Vector2 box_collider_wall_size;

    public float baseAngularSpeed;
    public float rotationSpeed;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
        anim = GetComponent<Animator>();
        player_box_collider = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(1-transform.rotation.z);

        
        if(Input.GetKeyDown(KeyCode.Space) && isGround){
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGround == false && doubleJumpAv == true){
            DoubleJump();
        }
        
        if(movingState == 1){
            Movement();
        }
        

        if(movingState == 2){
            SwingMovement();
        }

        WallSlide();
        
}


    void OnCollisionEnter2D(Collision2D collider){
        
        if(collider.gameObject.tag == "Ground"){
            isGround = true;
            allowToMove = true;
            anim.SetBool("WallSlide", false);

            // Falling State
            
            if(movingState == 3){
                movingState = 1;
                transform.rotation =  Quaternion.identity;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collider){
        
        if(collider.gameObject.tag == "Ground"){
            isGround = false;
        }
    }

    



    public void Jump(){

        if(isGround){
            rb.velocity = new Vector2(rb.velocity.x, 8f);
            doubleJumpAv = true;
        }
    }

    public void DoubleJump(){
       
         if(isGround == false){
            rb.velocity = new Vector2(rb.velocity.x, 6f);
            doubleJumpAv = false;
        }
    }

    public void Movement(){

        if(allowToMove){
            horizontalMove = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
                if(horizontalMove < 0 && lookingRight){
                    Flip();
                    lookingRight = false;
                } else if (horizontalMove > 0 && lookingRight == false){
                    Flip();
                    lookingRight = true;
                }

        Vector3 target = new Vector3(horizontalMove * 6, rb.velocity.y);

        rb.velocity = Vector3.MoveTowards(rb.velocity, target,  2);
       // rb.velocity = Vector3.SmoothDamp(rb.velocity, target ,  ref velocity, 0.1f);

        }

        
    }
    

    public void SwingMovement(){
        rb.angularVelocity = baseAngularSpeed + rotationSpeed * (1-Mathf.Abs(transform.rotation.z));
        hj.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        anim.SetBool("Grab", true);
        
        if (Input.GetKey("down")){
            mile.GetComponent<CircleCollider2D>().enabled = false;
            hj.enabled = false;

            anim.SetBool("Grab", false);
            movingState = 3;
            
            StartCoroutine("EnableMileCollider", 2f);
        }
        // rb.velocity = new Vector3(0f, 0f, 0f);
        // rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        // anim.SetBool("Grab", true);
        // float turn = Input.GetAxis("Horizontal");
        // Vector3 d = new Vector3(0f, 0f, 1f);
        // Vector3 playerPosition = transform.position;


        // if(Input.GetAxis("Horizontal") != 0){
        //     transform.RotateAround(playerPosition, d * turn, 0.1f + baseAngularSpeed * (1-Mathf.Abs(transform.rotation.z)));

        // } else if (Input.GetAxis("Horizontal") == 0){
        //     transform.RotateAround(playerPosition, d * 1, baseAngularSpeed * -transform.rotation.z * Mathf.Acos(transform.rotation.z));
        // }

    }

    void EnableMileCollider(){
        mile.GetComponent<CircleCollider2D>().enabled = true;
    }

    
    // Flip Character
    void Flip(){
        Vector3 charScale = transform.localScale;
        charScale.x *= -1;
        transform.localScale = charScale;
    }

    
    void WallSlide(){
        if (Physics2D.OverlapBox(wallCheck.position, box_collider_wall_size, 0, ground)){
            if(transform.localScale.x > 0){
                if(Input.GetAxis("Horizontal") > 0){
                    if (rb.velocity.y <= 0){
                        anim.SetBool("WallSlide", true);
                    }
                    allowToMove = false;
                } else {
                    anim.SetBool("WallSlide", false);
                    allowToMove = true;
                }
            } else if (transform.localScale.x < 0){
                if(Input.GetAxis("Horizontal") < 0){
                    if (rb.velocity.y <= 0){
                        anim.SetBool("WallSlide", true);
                    }
                allowToMove = false;
                } else {
                    anim.SetBool("WallSlide", false);
                    allowToMove = true;
                }
            }
        } else {
            anim.SetBool("WallSlide", false);
            allowToMove = true;
        }
    }

}

