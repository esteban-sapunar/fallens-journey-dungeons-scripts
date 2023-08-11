using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipData
{
    public int head;
	public int chest;
	public int legs;
	public int feet;
	public int weapon;
	public int shield;
	public int bag;

	public EquipData(){
		this.head = 0;
		this.chest = 3;
		this.legs = 4;
		this.feet = 5;
		this.weapon = 2;
		this.shield = 0;
		this.bag = 0;
	}

	public List<int> GetData(){
		List<int> list = new List<int>();
		list.Add(head);
		list.Add(chest);
		list.Add(legs);
		list.Add(feet);
		list.Add(weapon);
		list.Add(shield);
		list.Add(bag);

		return list;
	}
}
