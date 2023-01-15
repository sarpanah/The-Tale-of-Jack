using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInvertory : MonoBehaviour
{

    public List<LootItemsList> Invertory = new List<LootItemsList>();

    void Update(){
        ShowInvertory();
    }


    void ShowInvertory(){
        if(Input.GetKeyDown(KeyCode.I)){
            for (int i = 0; i < Invertory.Count() ; i++)          {
                Debug.Log(Invertory[i].itemName);
            }
        }
    }

}
