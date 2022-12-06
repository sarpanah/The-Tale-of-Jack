using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float health = 100;

    Animator anim;

    GameObject player;

    Vector3 startPoint;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        startPoint = transform.position;
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
       // Destroy(gameObject);

        transform.position = startPoint;
        health = 100;
    }
}
