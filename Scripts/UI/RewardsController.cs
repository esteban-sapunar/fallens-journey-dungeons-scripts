using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    //Values
    public GameObject player;
    Item item;
    public List <GameObject> itemSlots = new List<GameObject>();
    public Sprite nullIcon;
    public GameObject lootSlot;

    //Methods
    public void SetItemsSlots() {
    	int capacity = player.GetComponent<PlayerStats>().capacity;
    	for(int i = capacity; i<5;i++) {
    		itemSlots[i].GetComponent<RewardSlot>().active = false;
    		itemSlots[i].GetComponent<RewardSlot>().icon.enabled = true;
    		itemSlots[i].GetComponent<RewardSlot>().icon.sprite = nullIcon;
    	}
    }

    public void AddItem (Item newItem, GameObject currentSlot) {
    	item = newItem;
    	lootSlot = currentSlot;
    }

    public void SelectItem () {
    	if(item != null) {
    		for(int i = 0; i <= player.GetComponent<PlayerStats>().capacity-1;i++) {
	    		if(itemSlots[i].GetComponent<RewardSlot>().active && ReferenceEquals(itemSlots[i].GetComponent<RewardSlot>().item, null)) {
	    			itemSlots[i].GetComponent<RewardSlot>().AddItem(item,lootSlot);
	    			item = null;
	    			break;
	    		}
	    	}
    	}
    }
}
