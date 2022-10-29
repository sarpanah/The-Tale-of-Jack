using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    bool isGround;
    bool doubleJumpAv;
    Vector3 velocity = Vector3.zero;
    Animator anim;

    public GameObject mile;
    
   // public static Vector3 target;
    public static float horizontalMove = 0;

    public static bool moving = true;
    public static bool movingInSwing = false;
   
    bool lookingRight = true;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        
        if(moving){
            Movement();
        }
        

        if(movingInSwing){
            SwingMovement();
        }

    }

    


    void OnCollisionEnter2D(Collision2D collider){
        
        if(collider.gameObject.tag == "Ground"){
            isGround = true;
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
    

    public void SwingMovement(){
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        anim.SetBool("Grab", true);
        float turn = Input.GetAxis("Horizontal");
        Vector3 d = new Vector3(0f, 0f, 1f);
        Vector3 playerPosition = transform.position;
        transform.RotateAround(playerPosition, d * turn, 60 * Time.deltaTime);

    }


    
    // Flip Character
    void Flip(){
        Vector3 charScale = transform.localScale;
        charScale.x *= -1;
        transform.localScale = charScale;
    }







}

