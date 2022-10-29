using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float health = 100;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       
    }


    public void DecreaseHealth(float damage){
        health -= damage;

        anim.SetTrigger("Hurt");

        if(health <= 0){
            Die();
        }
    }


    void Die(){
        Destroy(gameObject);
    }
}
