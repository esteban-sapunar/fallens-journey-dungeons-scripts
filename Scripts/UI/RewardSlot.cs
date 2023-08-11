using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSlot : MonoBehaviour
{
    //Value
    public GameObject GM;
    public GameObject deleteButton;
    public Item item = null;
    public Image icon;
    public bool active = true;
    public GameObject lootSlot;

    //Methods
    public void AddItem(Item newItem, GameObject currentLootSlot) {
    	item = newItem;
    	icon.enabled = true;
    	icon.sprite = item.icon;
    	lootSlot = currentLootSlot;
    	lootSlot.GetComponent<Button>().interactable = false;
    	GM.GetComponent<DataController>().selectedItems.Add(item);
    	deleteButton.SetActive(true);
    }

    public void RemoveItem() {
    	GM.GetComponent<DataController>().selectedItems.Remove(item);
    	item = null;
    	icon.enabled = false;
    	lootSlot.GetComponent<Button>().interactable = true;
    	deleteButton.SetActive(false);
    }

}
