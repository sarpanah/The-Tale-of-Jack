using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public Slider slider;
    Animator anim;
    public GameObject lootBag;


    float health = 100;

    void Start()
    {
        anim = GetComponent<Animator>();
        slider.value = health;
    }

    public void DecreaseHealth(float damage){

        health -= damage;
        slider.value = health;
        anim.SetTrigger("Hurt");

        if(health <= 0){
            Die();
        }
    }


    void Die(){
        Destroy(gameObject);
        Vector3 bagLocation = transform.position - new Vector3 (0f, 0.4f, 0f);
        Instantiate(lootBag, bagLocation, Quaternion.identity);
    }

}
