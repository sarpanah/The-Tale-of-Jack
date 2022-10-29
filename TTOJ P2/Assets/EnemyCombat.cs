using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    GameObject player;

    public float damage = 20;
    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name == "Player"){
            player.GetComponent<PlayerHealth>().DecreaseHealth(damage);
        }
    }

}
