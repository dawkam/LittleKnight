using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string NotificationText;
    public Quest quest;

    private QuestLogController questLogController;
    private Canvas _questionMark;
    private Notification notification;

    private void Start()
    {
        notification = Notification.instance;
        questLogController = QuestLogController.instance;
        _questionMark = GetComponentInChildren<Canvas>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Interact") && !questLogController.IsQuestTaken(quest))
        {
            if (NotificationText != "")
            {
                notification.ActiveOk(NotificationText);
            }
            questLogController.AddQuest(quest);
            if (_questionMark != null)
                _questionMark.enabled = false;
        }
    }
}
