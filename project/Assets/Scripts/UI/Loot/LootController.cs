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

    private Notification _notification;
    private LootModel _lootModel;
    private InventoryController _inventoryController;
    private QuestLogController _questLogController;

    private LootSlot[] _slots; // inventory slots pełnią funkcije widoku


    void Start()
    {
        _lootModel = LootModel.instance;
        _lootModel.onLootItemChangedCallback += UpdateUI;
        _lootModel.onLootItemChangedCallback += ActiveUI;

        _inventoryController = InventoryController.instance;
        _slots = _lootModel.itemsParent.GetComponentsInChildren<LootSlot>();

        _questLogController = QuestLogController.instance;
        _notification = Notification.instance;

        _lootModel.lootUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_lootModel.lootUI.activeSelf)
            Time.timeScale = 0.0f; // po wyłączeniu ui eq czas nie wróci do normy
    }

    void UpdateUI()
    {

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_lootModel.waitingItems.Count != 0 && i < _lootModel.waitingItems[0].Count)
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
            _lootModel.lootUI.SetActive(true);
        }
        else if (_lootModel.waitingItems.Count == 0)
        {
            Time.timeScale = 1.0f;
            _lootModel.lootUI.SetActive(false);
        }
    }

    public void TakeAllItemsFromWaitingList()
    {
        List<Item> items = _lootModel.waitingItems[0];
        if (_inventoryController.AddInventoryItems(items))
        {
            foreach (Item item in items)
            {
                _questLogController.CheckGoal(item.name);
            }
            _lootModel.TakeAllItems();
        }
        else
        {
            _notification.ActiveOk("There is no space in your inventory.");
        }
    }

    public void AddWaitingItems(ref List<Item> items) 
    {
        _lootModel.AddWaitingItems(ref items);
    }


    public void RemoveWaitingItems(List<Item> items)
    {
        _lootModel.RemoveWaitingItems(items);
    }


        public void CloseLoot()
    {
        _lootModel.RemoveWaitingItems();
    }

    public void AddInventoryItem(Item item)
    {
        if (_inventoryController.AddInventoryItem(item)) //próba dodania do inventory
        {
            _questLogController.CheckGoal(item.name);
            _lootModel.RemoveWaitingItem(item); //usuwanie z okna lootu 

        }
        else if (_notification.IsFree())
        {
            _notification.ActiveOk("There is no space in your inventory.");
        }

    }
    public void UseItem(LootSlot lootSlot)
    {
        Item item = lootSlot.GetItem();
        if (item != null)
        {
            AddInventoryItem(item);
        }
    }

    public int GetLootSize() 
    {
        return _lootModel.GetLootSize();
    }
}
