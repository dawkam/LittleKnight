using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestLogView : MonoBehaviour
{
    public Transform questsParent;
    public GameObject questLogUI;

    public Text titleText;
    public Text descriptionText;
    public Text progressText;
    public Text goalText;
    public Text goalNameText;

    public Text expReward;
    public Image itemIcon;

    public Quest quest; // quest wyświtlony w danym momencie 

    public List<QuestLogSlot> slots = new List<QuestLogSlot>();

    public void SetDescription(ref Quest quest)
    {
        this.quest = quest;
        this.titleText.text = quest.title;
        this.descriptionText.text = quest.description;
        this.expReward.text = quest.expReward.ToString();
        this.itemIcon.enabled = true;
        this.itemIcon.sprite = quest.itemReward.icon;
        this.progressText.text = quest.progress.ToString();
        this.goalText.text = quest.maxProgress.ToString();
        this.goalNameText.text = quest.goalName;

    }

    public void AddQuest(Quest quest)
    {
        GameObject prefab = Resources.Load("Prefabs/UI/QuestSlot", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab, questsParent.position, questsParent.rotation, questsParent);
        QuestLogSlot questLogSlot = go.GetComponent<QuestLogSlot>();
        questLogSlot.SetQuest(quest);
        questLogSlot.SetButtonFunction();
        slots.Add(questLogSlot);
        //musi być dodawany do listy obiekt ,który powstaje w wyniku Instantiate. Jeśli zostanie dodany po porstu questLogSlot
        // to będa inne referencje w obiekcie, i nie będą działać późniejsze odwołania takie ja np ustawienie,że quest się zakończył (wizualnie)
    }

    public void SetCompletedIcon(List<Quest> quests)
    {
        List<QuestLogSlot> completedQuest = slots.Where(x => quests.Contains(x.quest)).AsEnumerable().ToList();
        foreach (QuestLogSlot q in completedQuest)
        {
            q.SetCompletedIcon();
        }
    }

    public void UpdateProgress()
    {
        this.progressText.text = quest.progress.ToString();
    }
}
