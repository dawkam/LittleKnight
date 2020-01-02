using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    #region Singleton
    public static LootController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of LootController !");
        }

        instance = this;
    }
    #endregion


    public Transform itemsParent;
    public GameObject lootUI;

    private Notification _notification;
    private LootModel _lootModel;
    private InventoryModel _inventoryModel;
    private QuestLogController _questLogController;

    private LootSlot[] _slots; // inventory slots pełnią funkcije widoku


    void Start()
    {
        _lootModel = LootModel.instance;
        _lootModel.onLootItemChangedCallback += UpdateUI;
        _lootModel.onLootItemChangedCallback += ActiveUI;

        _inventoryModel = InventoryModel.instance;
        _slots = itemsParent.GetComponentsInChildren<LootSlot>();

        _questLogController = QuestLogController.instance;
        _notification = Notification.instance;

        lootUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (lootUI.activeSelf)
            Time.timeScale = 0.0f; // po wyłączeniu ui eq czas nie wróci do normy
    }

    void UpdateUI()
    {

        for (int i = 0; i < _slots.Length; i++)
        {
            if(_lootModel.waitingItems.Count != 0 && i < _lootModel.waitingItems[0].Count)
            {
                _slots[i].AddItem(_lootModel.waitingItems[0][i]);
            }
            else
            {
                _slots[i].ClearSlot();
                _slots[i].RemoveDescription();
            }

        }
       // lootSystem.waitingItems.RemoveAt(0);
    }

    void ActiveUI()
    {
        if (_lootModel.waitingItems.Count != 0)
        {
            Time.timeScale = 0.0f;
            lootUI.SetActive(true);
        }
        else if (_lootModel.waitingItems.Count == 0)
        {
            Time.timeScale = 1.0f;
            lootUI.SetActive(false);
        }
    }

    public void TakeAllItemsFromWaitingList()
    {
        if (_inventoryModel.AddInventoryItems(_lootModel.waitingItems[0]))
        {
            _lootModel.TakeAllItems();
        }else
        {
            //alert
        }
    }

    public void CloseLoot()
    {
        _lootModel.RemoveWaitingItems();
    }

    public void AddInventoryItem(Item item)
    {
        if (_inventoryModel.AddInventoryItem(item)) //próba dodania do inventory
        {
            _questLogController.CheckGoal(item.name);
            _lootModel.RemoveWaitingItem(item); //usuwanie z okna lootu 

        }
        else if(_notification.IsFree())
        {
            _notification.ActiveOk("There is no space in your inventory.");
        }

    }
    public void UseItem(LootSlot lootSlot)
    {
        Item item = lootSlot.GetItem();
        if ( item != null)
        {
            AddInventoryItem(item);
        }
    }
}
