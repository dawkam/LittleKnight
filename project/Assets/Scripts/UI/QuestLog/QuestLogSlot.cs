using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestLogSlot : MonoBehaviour
{
    public Image icon;
    public Text title;
    public Quest quest;

    public void SetQuest(Quest quest)
    {
        icon.enabled = false;
        this.quest = quest;
        this.title.text = quest.title;
    }

    public void SetButtonFunction()
    {

        QuestLogController questLogController = QuestLogController.instance;
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { questLogController.SetDescription(this); });
        //UnityAction<QuestLogSlot> action1 = new UnityAction<QuestLogSlot>(myScriptInstance.SetDescription);
        //UnityEventTools.AddObjectPersistentListener(btn.onClick, action1, this);

    }

    public void SetCompletedIcon()
    {
        icon.enabled = true;
    }


}
