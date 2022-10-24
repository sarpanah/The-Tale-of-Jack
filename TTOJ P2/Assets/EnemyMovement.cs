using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 leftPoint;
    Vector3 rightPoint;
    bool movingLeft = true;
    bool enemyIdleMode = true;
    Rigidbody2D rb;
    GameObject player;
    
    public float moveSpeed = 2f;
    public float chaseSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        leftPoint = transform.position + new Vector3(-6f,0f,0f);
        rightPoint = transform.position + new Vector3(6f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {       

        Debug.Log(enemyIdleMode);

            if(enemyIdleMode){
            Movement();
           if(transform.position.x <= leftPoint.x){
            movingLeft = false;
            
           } else if (transform.position.x >= rightPoint.x){
            movingLeft = true;
            if(transform.localScale.x > 0){
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
                }
           }
        } else if (enemyIdleMode == false){
            ChasePlayer();
            Flip();
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 3){
            enemyIdleMode = false;
        } else {
            enemyIdleMode = true;
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 1){
            Debug.Log("FUCK LIFE");
        } 


    }

    void Movement(){
        if(movingLeft){
            transform.position = Vector3.MoveTowards(transform.position, leftPoint, moveSpeed * Time.deltaTime);
            if(transform.localScale.x > 0){
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
               }
        } else if(movingLeft == false){
            transform.position = Vector3.MoveTowards(transform.position, rightPoint, moveSpeed * Time.deltaTime);   
            if(transform.localScale.x < 0){
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }   
    }
    
    }

    void ChasePlayer(){
         Vector2 target = new Vector2(player.transform.position.x, transform.position.y);
         Vector2 newPos = Vector2.MoveTowards(rb.position, target, chaseSpeed * Time.fixedDeltaTime);
         rb.MovePosition(newPos);
       // rb.velocity = Vector3.MoveTowards(rb.velocity, player.transform.position, 5 * Time.deltaTime);
    }    

    void Flip(){
        if(transform.position.x - player.transform.position.x < 0 && transform.localScale.x < 0){
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
         } else if (transform.position.x - player.transform.position.x > 0 && transform.localScale.x > 0) {
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }
    }

}
