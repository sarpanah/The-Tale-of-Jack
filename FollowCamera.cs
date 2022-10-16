using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    private GameObject player;

    public Vector3 offset;

    public GameObject secCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        float deltaX = player.transform.position.x - transform.position.x;
        float deltaY = player.transform.position.y - transform.position.y;

        Debug.Log(deltaY);

        if (Math.Abs(deltaX) > 3){
            Vector3 target = transform.position + new Vector3 (deltaX, 0f, 0f);
            Vector3 newpos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5);	
            transform.position = newpos;

        }
        if (Math.Abs(deltaY) > 2){
            Vector3 target = transform.position + new Vector3 (0f, deltaY+1.75f, 0f);
            Vector3 newpos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5);	
            transform.position = newpos;
        }
        
        if(Input.GetKeyDown(KeyCode.V)){
            gameObject.SetActive(false);
            secCamera.SetActive(true);
        }


    }
}
