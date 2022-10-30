using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromSwing : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");    
    }

    // Update is called once per frame
    void Update()
    {


    }


    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name == "Player" && PlayerMovement.movingInSwing == false){
            PlayerMovement.moving = false;
            PlayerMovement.movingInSwing = true;
            player.transform.position = transform.position;
        }

    }

}
