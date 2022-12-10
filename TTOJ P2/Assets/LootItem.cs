using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    public LootItemsList lootItemsList;

    public PlayerInvertory playerInvertory;

    GameObject player;

    void Start(){
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f){
            ShowDetails();
        }

        Pickup();
    }

    void ShowDetails(){

    }

    void Pickup(){
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f && Input.GetKeyDown(KeyCode.E)){
            playerInvertory.Invertory.Add(lootItemsList);
            Destroy(gameObject);
        }
    }
}