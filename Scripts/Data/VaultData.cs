using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VaultData
{
	//Values
	public List<int> slots;

	public VaultData(){
		this.slots = new List<int>();
	}
}
