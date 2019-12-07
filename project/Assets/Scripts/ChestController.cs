using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public List<Item> items;

    public GameObject endParticle;
    private bool isOpennig;

    private ChestAnimation _chestAnimation;
    private LootModel lootModel;
    private bool _endParticleOnScene;

    void Start()
    {
        lootModel = LootModel.instance;
        _chestAnimation = GetComponentInChildren<ChestAnimation>();

    }
    private void Update()
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

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (Input.GetButtonDown("Interact") && !isOpennig && items.Count != 0)
            {

                isOpennig = true;
                if (_chestAnimation != null)
                    _chestAnimation.OpenAnimation();
                // show items
                if (lootModel.lootSize < items.Count)
                {
                    Debug.LogError("Too many items in chest!");

                }
                else
                {
                    lootModel.AddWaitingItems(ref items);
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
            lootModel.RemoveWaitingItems(items);
            if (_chestAnimation != null)
                _chestAnimation.CloseAnimation();
        }
    }
}
