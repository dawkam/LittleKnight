using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
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
#if UNITY_EDITOR
        QuestLogController myScriptInstance = FindObjectOfType<QuestLogController>();
        var btn = GetComponent<Button>();
        UnityAction<QuestLogSlot> action1 = new UnityAction<QuestLogSlot>(myScriptInstance.SetDescription);
        UnityEventTools.AddObjectPersistentListener(btn.onClick, action1, this);
#endif
    }

    public void SetCompletedIcon()
    {
        icon.enabled = true;
    }


}
