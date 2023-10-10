using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackAttack : MonoBehaviour
{

    float movementDir;
    Animator anim;
    [SerializeField] 
    //private Transform swordControllerGameoject;
    public GameObject swordCollider_GO;
    public JackController jackController;
    EdgeCollider2D swordCollider;
    Health health;
    float nextAttackTime = .5f;
    void Awake(){
        anim = GetComponent<Animator>();
        swordCollider = swordCollider_GO.GetComponent<EdgeCollider2D>();
        jackController = GetComponent<JackController>();
        health = GetComponent<Health>();
    }

    void Update(){
        
        AttackTimeCounter();

        if(Input.GetMouseButtonDown(0)){
            if(AttackTimeCounter() < 0){
                Attack();
                nextAttackTime = .5f;
                }
        }

        movementDir = Input.GetAxis("Horizontal");

        // AdjustColliderRotation();
     }

    private void OnTriggerEnter2D(){
        
    }

    public void Attack(){
        //anim.SetTrigger("Attack");
   
    }

    //void AdjustColliderRotation(){
    //    if(movementDir > 0.01){
    //        swordCollider_GO.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    } else if (movementDir < -0.01) {
    //    swordCollider_GO.transform.rotation = Quaternion.Euler(0, 180, 0);
    //    s}
    //}

    public void EnableSwordCollider(){
        swordCollider_GO.gameObject.SetActive(true);
        jackController.canMove = false;
    }
    public void DisableSwordCollider(){
        swordCollider_GO.gameObject.SetActive(false);
        jackController.canMove = true;
    }

    float AttackTimeCounter(){
        nextAttackTime -= Time.deltaTime;
        return nextAttackTime;
    }
}