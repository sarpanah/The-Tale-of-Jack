using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private GameObject player;
    float playerScale;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScale = player.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScale > 0) {
            transform.Translate(Vector3.right * Time.deltaTime * 10);
        } else {
            transform.Translate(Vector3.left * Time.deltaTime * 10);
        }

        

    }
}
