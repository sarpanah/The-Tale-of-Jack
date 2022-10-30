using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireScript : MonoBehaviour
{   

   // EnemyHealth enemy;
    GameObject player;
    float playerScale;
    float damage = 20;

    void Start()
    {   
        player = GameObject.Find("Player");
        playerScale = player.transform.localScale.x;
    }

    void Update()
    {
        
        ReleaseFire();

    }

    void ReleaseFire(){
        if(playerScale > 0) {
            transform.Translate(Vector3.right * Time.deltaTime * 10);
        } else {
            transform.Translate(Vector3.left * Time.deltaTime * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Enemy"){
            collider.GetComponent<EnemyHealth>().DecreaseHealth(damage);
            Destroy(gameObject);
        }
    }
}
