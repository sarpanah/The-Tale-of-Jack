using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTest : MonoBehaviour
{

    string questName = "Save the bird";

    bool isQuestStarted = false;
    bool isQuestDone = false;
    bool isQuestFinished = false;

    GameObject player;

    public float startDis = 3f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted)
            if(isQuestStarted){
            OngoingDialogue();
            
        }

         if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted == false){
            isQuestStarted = true;
            InitialDialogue();
            QuestsManager.UnlockedQuests.Add(questName);
        }

        
    }

    void InitialDialogue(){
        Debug.Log("Initial Dialogue");
    }

    void OngoingDialogue(){
        Debug.Log("Quest is ongoing");
    }

    void FinishedDialogue(){

    }

}
