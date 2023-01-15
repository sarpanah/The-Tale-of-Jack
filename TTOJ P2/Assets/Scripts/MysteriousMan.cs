using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteriousMan : MonoBehaviour
{
    // Quest name and what to dos
    string questName = "Quest Name: Mysterious Man";
    string whatToDoInFirst = "Go and kill the man";
    string whatToDoInFinal = "zzz";

    bool isQuestStarted = false;
    bool isQuestDone = false;
    bool isQuestFinished = false;
    bool isQuestFailed = false;

    GameObject player;
    GameObject capsule;

    public float startDis = 3f;

    Animator characterAnim;
 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        characterAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        QuestEvents();

        if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted && isQuestFinished == false)
            if(isQuestStarted){

            
        }

         if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted == false){
            isQuestStarted = true;

            QuestsManager.UnlockedQuests.Add(questName);
            QuestsManager.whatToDoQuestString = whatToDoInFirst;
        }
        
        if  (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestFinished){

        }

        if  (Input.GetKeyDown(KeyCode.Y)){
            FindObjectOfType<DialogueManager>().DisplayDialogue();
        }
        

    }


    // Quest Events

    void QuestEvents(){
        
           // QuestsManager.whatToDoQuestString = "whatToDoInFinal";
           // QuestFinished();
       
    }

    void FailedQuest(){
        if(isQuestFailed){
            QuestsManager.FailedQuests.Add(questName);
            QuestsManager.UnlockedQuests.Remove(questName);
        }
    }

    void QuestFinished(){
        isQuestFinished = true;
        QuestsManager.UnlockedQuests.Remove(questName);
        QuestsManager.FinishedQuests.Add(questName);
    }


    void DialogueTrigger(){
        
    }


}
