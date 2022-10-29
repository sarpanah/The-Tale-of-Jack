using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 leftPoint;
    Vector3 rightPoint;
    bool movingLeft = true;
    bool enemyIdleMode = true;
    bool reachedPlayer = false;
    Rigidbody2D rb;
    GameObject player;
    Animator anim;

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
            if (Mathf.Abs(transform.position.x - player.transform.position.x) < attackDist){
                reachedPlayer = true;
                stopAndAttack();

            }else {
                ChasePlayer();
                Flip();
            }
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < chaseDistX && Mathf.Abs(transform.position.y - player.transform.position.y) < chaseDistY){
            enemyIdleMode = false;
        } else{
            enemyIdleMode = true;
        }
        

        


    }


    void stopAndAttack(){
        anim.SetBool("Attack", true);
        anim.SetBool("Running", false);
    }

    void Movement(){
        anim.SetBool("Running", true);
        anim.SetBool("Attack", false);
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
         anim.SetBool("Running", true);
         anim.SetBool("Attack", false);
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
