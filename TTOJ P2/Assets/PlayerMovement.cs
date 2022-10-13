using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    bool isGround;
    bool doubleJumpAv;
    float horizontalMove;
    float verticalMove;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        
        Movement();


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
            Debug.Log("Jumped");
        }
    }

    public void DoubleJump(){
       
         if(isGround == false){
            rb.velocity = new Vector2(rb.velocity.x, 6f);
            doubleJumpAv = false;
            Debug.Log("Double Jumped");
        }
    }

    public void Movement(){
        //if(isGround){
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector3 target = new Vector3(horizontalMove * 3000 * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target ,  ref velocity, 0.3f);
        //}
    }



}
