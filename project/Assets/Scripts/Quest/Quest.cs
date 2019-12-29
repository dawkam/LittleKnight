using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest")]
public class Quest : ScriptableObject
{

    public string title;
    public string description;

    public Item itemReward;
    public float expReward;

    public bool isCompleted;

    public Quest nextQuest;

    public string goalName;

    public int progress;

    public int maxProgress;

    private void OnEnable()
    {
        progress = 0;
        isCompleted = false;
    }
    public bool AddProgress()
    {
        progress++;
        if (progress == maxProgress)
        {
            if (nextQuest != null)
            {
                QuestLogController questLogController = QuestLogController.instance;
                questLogController.AddQuest(nextQuest);
            }
            isCompleted = true;
            return true; ;
        }
        else return false;
        //if (_progress == maxProgress)
        //{
        //    //GameObject tmp = Prefab.transform.Find("CompletedDynamic").gameObject;
        //    //Image image = tmp.GetComponent<Image>();
        //    //image.enabled = true;
        //}
    }


}

