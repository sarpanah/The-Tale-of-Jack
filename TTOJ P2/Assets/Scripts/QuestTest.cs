using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTest : MonoBehaviour
{

    string questName = "Quest Name: Save the bird";
    string whatToDoInFirst = "Go and touch the capsule";
    string whatToDoInFinal = "Go back to the npc";

    bool isQuestStarted = false;
    bool isQuestDone = false;
    bool isQuestFinished = false;
    bool isQuestFailed = false;

    GameObject player;
    GameObject capsule;

    public float startDis = 3f;

    public Animator anim;
    public Text dialogueBarText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        capsule = GameObject.Find("Capsule");
        
    }

    // Update is called once per frame
    void Update()
    {
        QuestEvents();

        if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted && isQuestFinished == false)
            if(isQuestStarted){
            MiddleDialogue();
            
        }

         if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted == false){
            isQuestStarted = true;
            InitialDialogue();
            QuestsManager.UnlockedQuests.Add(questName);
            QuestsManager.whatToDoQuestString = whatToDoInFirst;
        }
        
        if  (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestFinished){
            EndDialogue();
        }
        
        NpcDialogueBar();


        if  (Input.GetKeyDown(KeyCode.Y)){
            FindObjectOfType<DialogueManager>().DisplayDialogue();
        }
        

    }

    void InitialDialogue(){
        string[] dialogue = {"Could you touch the capsule for me?", "Please do it for me!"};
        FindObjectOfType<DialogueManager>().InitializeDialogue(dialogue);
    }

    void MiddleDialogue(){
        
    }

    void EndDialogue(){
        
    }


    // Quest Events

    void QuestEvents(){
        if (Mathf.Abs(capsule.transform.position.x - player.transform.position.x) < 1f && isQuestStarted){
            QuestsManager.whatToDoQuestString = "whatToDoInFinal";
            QuestFinished();
            
        }
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




    void NpcDialogueBar(){
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted == false ) {
            dialogueBarText.text = "Press E to Talk";
            anim.SetBool("Active", true);
        } else if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted && isQuestFinished == false) {
            dialogueBarText.text = "Please do it for me";
            anim.SetBool("Active", true);
        } else if (Input.GetKeyDown(KeyCode.E) && (Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted && isQuestFinished == true)){
            dialogueBarText.text = "Thanks honey!";
            anim.SetBool("Active", true);
        } else {
            anim.SetBool("Active", false);
        }
    }

}
