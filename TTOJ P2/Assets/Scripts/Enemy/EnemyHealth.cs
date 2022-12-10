using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    float health = 100;

    Animator anim;

    public GameObject lootBag;

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
        Vector3 bagLocation = transform.position - new Vector3 (0f, 0.4f, 0f);
        Instantiate(lootBag, bagLocation, Quaternion.identity);
    }

}
