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
    private PlayerController _playerController;
    private InventoryController _inventoryController;

    void Start()
    {
        _questLogModel = QuestLogModel.instance;
        _questLogView = GetComponent<QuestLogView>();
        _questLogModel.questLogUI.SetActive(false);
        _questLogView.SetQuestParent(_questLogModel.questsParent);
        _playerController = PlayerController.instance;
        _inventoryController = InventoryController.instance;
    }
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (_questLogModel.questLogUI.activeSelf)
            {
                _questLogModel.questLogUI.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
                _questLogModel.questLogUI.SetActive(true);
            }

        }
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 1.0f;
            _questLogModel.questLogUI.SetActive(false);
        }
    }
    public void AddQuest(Quest quest)
    {
        if (_questLogModel.AddQuest(quest))
        {
            _questLogView.AddQuest(quest);
            int countOfItems = _inventoryController.CountOfItem(quest.goalName);
            for (int i = 0; i < countOfItems; i++)
            {
                CheckGoal(quest.goalName);
            }
        }
    }

    public void SetDescription(QuestLogSlot questLogSlot)
    {
        _questLogView.SetDescription(ref questLogSlot.quest);
    }

    public void CheckGoal(string goal)
    {
        List<Quest> completedQuests = _questLogModel.AddProgressGoal(goal);// zwraca questy, które się zakończyły z tym progresem
        _questLogView.SetCompletedIcon(completedQuests);
        foreach (Quest q in completedQuests)
        {
            _playerController.AddExp(q.expReward);
            _inventoryController.AddInventoryItem(q.itemReward);
            CheckGoal(q.itemReward.name);
        }
        if (_questLogView.quest != null && _questLogView.quest.goalName == goal)
            _questLogView.UpdateProgress();
    }

    public bool IsQuestTaken(Quest quest)
    {
        return _questLogModel.IsQuestTaken(quest);
    }
}
