using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Quest quest;
    private QuestLogController questLogController;

    private Notification notification;

    private void Start()
    {
        notification = Notification.instance;
        questLogController = QuestLogController.instance;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            notification.SetText("You got a new quest!");
            notification.ActiveOk();
            questLogController.AddQuest(quest);
        }
    }
}
