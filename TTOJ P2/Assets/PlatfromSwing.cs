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


    void OnCollisionEnter2D(Collision2D collider){
        if(collider.gameObject.name == "Player"){
            PlayerMovement.moving = false;
            PlayerMovement.movingInSwing = true;
            player.transform.position = transform.position - new Vector3(.2f, 0.7f, 5f);
        }

    }

}
