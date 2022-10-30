using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera2 : MonoBehaviour
{

    private GameObject player;

    public GameObject firCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3 (0f, 1.75f, -1f);

        if(Input.GetKeyDown(KeyCode.V)){
            FollowCamera.cam2OnAir = true;
            gameObject.SetActive(false);
            firCamera.SetActive(true);
        }

    }
}
