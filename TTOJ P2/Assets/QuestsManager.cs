using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{

    public static List<string> AllQuests = new List<string>();
    public static List<string> LockedQuests = new List<string>();
    public static List<string> UnlockedQuests = new List<string>();
    public static List<string> FailedQuests = new List<string>();

    string currentQuest;

    string[] AllQuestsString = {"Save the bird", "Quest 2", "Quest 3"};

    // Start is called before the first frame update
    void Start()
    {
        AllQuests.AddRange(AllQuestsString);
    }

    // Update is called once per frame
    void Update()
    {
         CurrentQuest();
    }

    void CurrentQuest(){
        if(UnlockedQuests.Count > 1){
            currentQuest = UnlockedQuests[UnlockedQuests.Count-1];
        } else {
            currentQuest = UnlockedQuests[0];
        }
    }


}
