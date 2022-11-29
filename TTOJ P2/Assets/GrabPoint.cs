using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour
{

    GameObject player;

    public Transform grabPoint;

    public Vector3 offset;

    void Start(){
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.2f){

            player.transform.position = transform.position + new Vector3(0.4f, 1f, 0f);

        } else {

        }

         
    }

   
}
