using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogController : MonoBehaviour
{
    #region Singleton
    public static QuestLogController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of QuestLogController !");
        }

        instance = this;
    }
    #endregion

    private QuestLogModel _questLogModel;
    private QuestLogView _questLogView;



    void Start()
    {
        _questLogModel = QuestLogModel.instance;
        _questLogView = GetComponent<QuestLogView>();
        
    }

    public void AddQuest(Quest quest)
    {
        if (_questLogModel.AddQuest(quest))
        {
            _questLogView.AddQuest(quest);
        }
    }

    public void SetDescription(QuestLogSlot questLogSlot)
    {
        _questLogView.SetDescription(ref questLogSlot.quest);
    }

    public void CheckGoal(string goal)
    {
       List<Quest> completedQuests =_questLogModel.AddProgressGoal(goal);// zwraca questy, które się zakończyły z tym progresem
        _questLogView.SetCompletedIcon(completedQuests);
        if (_questLogView.quest !=null && _questLogView.quest.goalName == goal)
            _questLogView.UpdateProgress();
    }

}
