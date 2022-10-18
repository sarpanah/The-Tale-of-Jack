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

            if(enemyIdleMode){
            Movement();
           if(transform.position.x <= leftPoint.x){
            movingLeft = false;
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
           } else if (transform.position.x >= rightPoint.x){
            movingLeft = true;
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;

           }
        } else if (enemyIdleMode == false){
            ChasePlayer();
        }

        if (transform.position.x - player.transform.position.x < 3){
            enemyIdleMode = false;
        } else {
            enemyIdleMode = true;
        }


    }

    void Movement(){
        if(movingLeft){
            transform.position = Vector3.MoveTowards(transform.position, leftPoint, moveSpeed * Time.deltaTime);
        } else if(movingLeft == false){
            transform.position = Vector3.MoveTowards(transform.position, rightPoint, moveSpeed * Time.deltaTime);   
        }   
    }


    void ChasePlayer(){
         Vector2 target = new Vector2(player.transform.position.x, transform.position.y);
         Vector2 newPos = Vector2.MoveTowards(rb.position, target, chaseSpeed * Time.fixedDeltaTime);
         rb.MovePosition(newPos);
       // rb.velocity = Vector3.MoveTowards(rb.velocity, player.transform.position, 5 * Time.deltaTime);
    }    

}
