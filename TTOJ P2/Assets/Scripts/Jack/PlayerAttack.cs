using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){

         if (collider.tag == "Enemy" && PlayerCombat.attackEnemy){
            collider.GetComponent<EnemyHealth>().DecreaseHealth(damage);
            PlayerCombat.attackEnemy = false;
        }
    }
    
}
