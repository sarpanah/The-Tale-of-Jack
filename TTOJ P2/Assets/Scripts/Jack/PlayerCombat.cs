using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    float nextAttack = 0f;
    Animator anim;
    EnemyHealth enemy;
    public Transform castPoint;
    public GameObject fire;
    float stopCounter;
    Rigidbody2D rb;
    public static bool attackEnemy = false;
    public float damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            if(Time.time > nextAttack){
                RunSwordAttack();
                nextAttack = Time.time + 1 / 2;
            }
        }


        if (Input.GetKeyDown(KeyCode.F)) {
           RunCastAttack();
        }

        
   
    }

    public void RunSwordAttack(){
       int rand = Random.Range(1, 4);
       if (rand == 1){
            anim.SetTrigger ("Attack1");
        } else if (rand == 2) {
            anim.SetTrigger ("Attack2");
        } else if (rand == 3) {
            anim.SetTrigger ("Attack3");
        }
        attackEnemy = true;
    }
    

    void RunCastAttack(){
        if(Time.time > nextAttack){
                anim.SetTrigger ("Cast");
                PlayerMovement.moving = false;
                rb.velocity = new Vector3 (0f, 0f, 0f);
                nextAttack = Time.time + 3 / 2;
                stopCounter = Time.time;
        }
    }

    public void Cast(){
        Instantiate(fire, castPoint.position, Quaternion.identity);
        Invoke("UnlockMoving", 0.5f);

    }

    public void UnlockMoving(){
        PlayerMovement.moving = true;

    }

}
