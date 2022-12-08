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
    public static int movingState = 1; // 0: Stop, 1: moving, 2: swinging, 3: falling 4: ground sliding
    public Transform wallCheck;
    BoxCollider2D player_box_collider;


    public LayerMask ground;

    bool lookingRight = true;

    bool allowToMove = false;

    public Transform wallOverlapCheck;

    public Vector2 box_collider_wall_size;

    public float baseAngularSpeed;
    public float rotationSpeed;

    public float remainingTimeForGroundSlide = 1f;


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

        GroundSlide();

        Falling();
}


    void OnCollisionEnter2D(Collision2D collider){
        
        if(collider.gameObject.tag == "Ground"){
            isGround = true;
            allowToMove = true;
            anim.SetBool("WallSlide", false);
            anim.SetBool("Jump", false);

            // Falling State
            
            if(movingState == 3){
                movingState = 1;
                transform.rotation =  Quaternion.identity;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                anim.SetBool("Falling", false);
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
            anim.SetBool("Jump", true);
        }
    }

    public void DoubleJump(){
       
         if(isGround == false){
            rb.velocity = new Vector2(rb.velocity.x, 6f);
            doubleJumpAv = false;
            anim.SetBool("Jump", true);
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
        bool rotatingAntiClockWise = true;

        // ROTATION IN CLOCK SHAPE (WHAT?)
        if(rotatingAntiClockWise){
            rb.angularVelocity = baseAngularSpeed + rotationSpeed * (1-Mathf.Abs(transform.rotation.z));
        } else if(rotatingAntiClockWise == false) {
            rb.angularVelocity = baseAngularSpeed + rotationSpeed * (1-Mathf.Abs(transform.rotation.z) * -1);
        }
        
        hj.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        anim.SetBool("Grab", true);

        // WHERE PLAYER LOOKS
        if(Input.GetAxis("Horizontal") < 0 && lookingRight){
           Flip();
           lookingRight = false;
           rotatingAntiClockWise = true;
        } else if (Input.GetAxis("Horizontal") > 0 && lookingRight == false){
            Flip();
            lookingRight = true;
            rotatingAntiClockWise = false;
        }

        // PLAYER INTERACTION WITH MILE
        if (Input.GetKey("down")){
            mile.GetComponent<CircleCollider2D>().enabled = false;
            hj.enabled = false;
            anim.SetBool("Grab", false);
            movingState = 3;
            StartCoroutine("EnableMileCollider", 2f);

        } else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0){
            rb.velocity = new Vector3 (5f, 5f, 0f);
            mile.GetComponent<CircleCollider2D>().enabled = false;
            hj.enabled = false;
            anim.SetBool("Grab", false);
            anim.SetBool("Smrslt", true);
            movingState = 3;
            StartCoroutine("EnableMileCollider", 2f);

        } else if(Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0){
            rb.velocity = new Vector3 (-5f, 5f, 0f);
            mile.GetComponent<CircleCollider2D>().enabled = false;
            hj.enabled = false;
            anim.SetBool("Grab", false);
            anim.SetBool("Smrslt", true);
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

    void GroundSlide(){
        
        if(Mathf.Abs(rb.velocity.x) > 5 && Input.GetKeyDown(KeyCode.C))
        {
            movingState = 4;
        }

        if (movingState == 4){
            anim.SetBool("GroundSlide", true);
            if(transform.localScale.x > 0){
                rb.velocity = new Vector3 (5f, 0f, 0f);
            } else {
                rb.velocity = new Vector3 (-5f, 0f, 0f);
            }
            
            remainingTimeForGroundSlide -= Time.deltaTime;
                if(remainingTimeForGroundSlide <= 0){
                    movingState = 1;
                    anim.SetBool("GroundSlide", false);
                    remainingTimeForGroundSlide = 1;
                }
            
        }

        
    }


    void Falling(){
        if (isGround == false && rb.velocity.y < -1){
            movingState = 3;
        }

        if(movingState == 3){
            anim.SetBool("Falling", true);
        }
    }


}