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
        if(collider.gameObject.name == "Player" && PlayerMovement.movingState == 1){
            PlayerMovement.movingState = 2;
            player.transform.position = transform.position;
        }
//|| PlayerMovement.movingState == 3
    }

}
