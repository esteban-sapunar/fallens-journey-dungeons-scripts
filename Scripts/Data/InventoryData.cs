using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
	//Values
	public List<int> slots;

	public InventoryData(){
		this.slots = new List<int>();
		this.slots.Add(1);
	}
}
