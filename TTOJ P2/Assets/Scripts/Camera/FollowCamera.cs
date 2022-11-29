using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    private GameObject player;

    public Vector3 offset;

    public GameObject secCamera;

    public static bool cam2OnAir = false;

    public float smoothness = 0.3f;

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
    
        if (cam2OnAir){
            Vector3 desiredPos = player.transform.position + new Vector3 (0f, 1.75f, -1f);
            Vector3 smoothedPos = Vector3.Lerp (transform.position, desiredPos, smoothness * Time.deltaTime);
            transform.position = smoothedPos;
            cam2OnAir = false;
        }

        if (Math.Abs(deltaX) > 3){
            Vector3 target = transform.position + new Vector3 (deltaX, 0f, 0f);
            Vector3 newpos = Vector3.Lerp(transform.position, target, smoothness * Time.deltaTime);	
            transform.position = newpos;

        }
        if (Math.Abs(deltaY) > 2){
            Vector3 target = transform.position + new Vector3 (0f, deltaY+1.75f, 0f);
            Vector3 newpos = Vector3.Lerp(transform.position, target, Time.deltaTime * smoothness * Time.deltaTime);	
            transform.position = newpos;
        }
        
        
        if(Input.GetKeyDown(KeyCode.V)){

            secCamera.SetActive(true);
            gameObject.SetActive(false);
            
        }


    }
}
