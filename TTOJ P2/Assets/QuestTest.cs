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
    public Text dialogueText;

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
    }

    void InitialDialogue(){
        Debug.Log("Hey, Could you touch the capsule, please?");
    }

    void MiddleDialogue(){
        dialogueText.text = "Press E to asdasdTalk";
    }

    void EndDialogue(){
        Debug.Log("Quest Finished SUCKSEXFULLY, Now you will die!");
    }


    // Quest Events

    void QuestEvents(){
        if (Mathf.Abs(capsule.transform.position.x - player.transform.position.x) < 1f && isQuestStarted){
            QuestsManager.whatToDoQuestString = "whatToDoInFinal";
            isQuestFinished = true;
            QuestsManager.UnlockedQuests.Remove(questName);
            QuestsManager.FinishedQuests.Add(questName);
            
        }
    }

    void FailedQuest(){
        if(isQuestFailed){
            QuestsManager.FailedQuests.Add(questName);
            QuestsManager.UnlockedQuests.Remove(questName);
        }
    }


    void NpcDialogueBar(){
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted == false ) {
            dialogueText.text = "Press E to Talk";
            anim.SetBool("Active", true);
        } else if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted && isQuestFinished == false) {
            dialogueText.text = "Please do it for me";
            anim.SetBool("Active", true);
        } else if (Input.GetKeyDown(KeyCode.E) && (Mathf.Abs(transform.position.x - player.transform.position.x) < startDis && isQuestStarted && isQuestFinished == true)){
            dialogueText.text = "Thanks honey!";
            anim.SetBool("Active", true);
        } else {
            anim.SetBool("Active", false);
        }
    }

}
