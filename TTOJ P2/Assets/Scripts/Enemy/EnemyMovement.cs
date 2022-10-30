using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   


    Rigidbody2D rb;
    GameObject player;
    Animator anim;
    Vector3 leftPoint;
    Vector3 rightPoint;
    
    bool movingLeft = true;
    bool enemyIdleMode = true;
    bool reachedPlayer = false;
    public static bool attackPlayer = false;

    public float moveSpeed = 2f;
    public float chaseSpeed = 2f;
    public float chaseDistX = 4f;
    public float chaseDistY = 3f;
    public float attackDist = 1.5f;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        leftPoint = transform.position + new Vector3(-6f,0f,0f);
        rightPoint = transform.position + new Vector3(6f,0f,0f);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {       
        EnemeyState();

    }


    // Check & decide what enemy should do
    void EnemeyState(){

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < chaseDistX && Mathf.Abs(transform.position.y - player.transform.position.y) < chaseDistY){
            enemyIdleMode = false;
        } else {
            enemyIdleMode = true;
        }

        if(enemyIdleMode){
            Movement();
            
        } else if (enemyIdleMode == false){
            EnemyDistanceChecker();
        }

    }

    // Check enemy have to move Left or Right
    void Movement(){
        anim.SetBool("Running", true);
        anim.SetBool("Attack", false);
        if (transform.position.x <= leftPoint.x){
                movingLeft = false;
        } else if (transform.position.x >= rightPoint.x){
                movingLeft = true;
        }

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

    // What enemy should do when idle mode is false
    void EnemyDistanceChecker(){
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < attackDist){
                reachedPlayer = true;
                stopAndAttack();

            } else {
                ChasePlayer();
                Flip();
            }
    }

    void ChasePlayer(){
         Vector2 target = new Vector2(player.transform.position.x, transform.position.y);
         Vector2 newPos = Vector2.MoveTowards(rb.position, target, chaseSpeed * Time.fixedDeltaTime);
         rb.MovePosition(newPos);
         anim.SetBool("Running", true);
         anim.SetBool("Attack", false);
       // rb.velocity = Vector3.MoveTowards(rb.velocity, player.transform.position, 5 * Time.deltaTime);
    }    

   void stopAndAttack(){
        anim.SetBool("Attack", true);
        anim.SetBool("Running", false);
        attackPlayer = true;
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
