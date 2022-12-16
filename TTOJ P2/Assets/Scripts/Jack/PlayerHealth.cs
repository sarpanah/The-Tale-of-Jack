using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider slider;
    float health = 100;

    Animator anim;

    GameObject player;

    Vector3 startPoint;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        startPoint = transform.position;
        slider.value = health;
    }

    void Update()
    {
       
    }


    public void DecreaseHealth(float damage){

        health -= damage;
        anim.SetTrigger("Hurt");
        slider.value = health;

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
