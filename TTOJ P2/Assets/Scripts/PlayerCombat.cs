using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    float nextAttack = 0f;
    Animator anim;

    public Transform castPoint;
    public GameObject fire;
    float stopCounter;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            if(Time.time > nextAttack){
                Attack();
                nextAttack = Time.time + 1 / 2;
            }
        }

        // Cast Code
        if (Input.GetKeyDown(KeyCode.F)) {
           if(Time.time > nextAttack){
                anim.SetTrigger ("Spell");
                PlayerMovement.moving = false;
                rb.velocity = new Vector3 (0f, 0f, 0f);
                nextAttack = Time.time + 3 / 2;
                stopCounter = Time.time;
            }
        }

        
   
    }

    public void Attack(){
        int rand = Random.Range(1, 4);
        if (rand == 1){
            anim.SetTrigger ("Attack1");
        } else if (rand == 2) {
            anim.SetTrigger ("Attack2");
        } else if (rand == 3) {
            anim.SetTrigger ("Attack3");
        }
    }
    
    public void Spell(){
        Instantiate(fire, castPoint.position, Quaternion.identity);
        Invoke("UnlockMoving", 0.5f);

    }

    public void UnlockMoving(){
        PlayerMovement.moving = true;

    }

}
