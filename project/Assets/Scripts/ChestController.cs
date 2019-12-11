using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public List<Item> items;

    public GameObject endParticle;
    private bool isOpennig;

    private ChestAnimation _chestAnimation;
    private LootModel _lootController;
    private bool _endParticleOnScene;

    void Start()
    {
        _lootController = LootModel.instance;
        _chestAnimation = GetComponentInChildren<ChestAnimation>();

        //Store all Gameobjects in an array like this
        Object[] allItems = Resources.LoadAll("Items");
        int numberOfItems = Random.Range(1, 5);

        for (int i = 0; i < numberOfItems; i++)
        {
            items.Add((Item)allItems[Random.Range(0, allItems.Length )]);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (items.Count == 0)
            {
                if (!_endParticleOnScene)
                {
                    endParticle = Instantiate(endParticle, this.transform.position, this.transform.rotation, null);
                    _endParticleOnScene = true;
                }
                else if (_chestAnimation == null || _chestAnimation.EndCloseAnimation())
                    Destroy(gameObject);
            }
            if (Input.GetButtonDown("Interact") && !isOpennig && items.Count != 0)
            {

                isOpennig = true;
                if (_chestAnimation != null)
                    _chestAnimation.OpenAnimation(isOpennig);
                // show items
                if (_lootController.lootSize < items.Count)
                {
                    Debug.LogError("Too many items in chest!");

                }
                else
                {
                    _lootController.AddWaitingItems(ref items);
                }

            }
            else if (isOpennig && items.Count == 0)
                CloseChestAnimation();

        }
    }

    private void OnTriggerExit(Collider other)
    {

        CloseChestAnimation();
    }

    private void CloseChestAnimation()
    {
        if (isOpennig)
        {
            isOpennig = false;
            if (items.Count != 0 && _chestAnimation != null)
                _chestAnimation.OpenAnimation(isOpennig);
            _lootController.RemoveWaitingItems(items);
            if (_chestAnimation != null)
                _chestAnimation.CloseAnimation();
        }
    }
}
