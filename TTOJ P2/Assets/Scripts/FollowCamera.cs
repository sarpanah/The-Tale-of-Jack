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

        if (deltaX > 3 || deltaY > 0.2){
            Vector3 target = transform.position + new Vector3 (deltaX, deltaY, 0f);
            Vector3 newpos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5);	
            transform.position = newpos;

        } else if (deltaX < -3 || deltaY > 0.2){
            Vector3 target = transform.position + new Vector3 (deltaX, deltaY, 0f);
            Vector3 newpos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5);	
            transform.position = newpos;
        }
        
        if(Input.GetKeyDown(KeyCode.V)){
            gameObject.SetActive(false);
            secCamera.SetActive(true);
        }


    }
}
