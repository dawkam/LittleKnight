using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class QuestLogModel : MonoBehaviour
{
    #region Singleton
    public static QuestLogModel instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of QuestLog !");
        }

        instance = this;
    }
    #endregion

    public Transform questsParent;
    public GameObject questLogUI;

    private List<Quest> quests = new List<Quest>();


    public bool AddQuest(Quest quest)
    {
        if (!quests.Contains(quest))
        {
            quests.Add(quest);
            return true;
        }

        return false;
    }

    public List<Quest> AddProgressGoal(string goalName)
    {
        List<Quest> result = new List<Quest>();
        List<Quest> tmp = quests.Where(x => x.goalName == goalName && !x.isCompleted).AsEnumerable().ToList();
        foreach (Quest quest in tmp )
        {
            if(quest.AddProgress())
            result.Add(quest);
        }
        return result;
        
    }

    public bool IsQuestTaken(Quest quest)
    {
        return quests.Contains(quest);
    }
}
