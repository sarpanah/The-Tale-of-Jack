using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    GameObject player;

    float x = 100;
    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name == "Player"){
            x -= 1f;
            Debug.Log(x);
        }
    }

}
