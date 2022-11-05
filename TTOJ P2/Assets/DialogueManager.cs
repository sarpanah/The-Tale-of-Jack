using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    private Queue<string> sentences;
    // string[] s = {"salam", "dalam"};
    
    public Text showDialogue;

    void Start(){
        sentences = new Queue<string>();
    }

    void Update(){
        
    }

    public void InitializeDialogue(string[] sSentences){

        sentences.Clear();

        foreach(string sentence in sSentences){
            sentences.Enqueue(sentence);
        }

        DisplayDialogue();
    }

    public void DisplayDialogue(){
        if(sentences.Count == 0){
            return;
        }
        string sentence = sentences.Dequeue();
        showDialogue.text = sentence;
    }

}
