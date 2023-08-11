using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	//Values
	//Player
	public int level;
	public int exp;
	public int gold;
	public int skill_points;
	public int strength;
	public int agility;
	public int dexterity;
	public int resistance;
	public int vitality;
	public int cards_points;
	public int crafting_level;
	public int crafting_exp;

	//Equip
	public EquipData equip;
	//Bag
	public InventoryData inventory;
	//Vault
	public VaultData vault;
	//Desk
	public DeskData desk;
	//Card Valt
	public string cards_vault;
	//Rewards
	public string reward_items;
	//Stages
	public string stages;
	//Misions
	public string missions;

	public GameData(){
		this.level = 1;
		this.exp = 0;
		this.gold = 0;
		this.skill_points = 0;
		this.strength = 0;
		this.agility = 0;
		this.dexterity = 0;
		this.resistance = 0;
		this.vitality = 0;
		this.cards_points = 0;
		this.crafting_level = 1;
		this.crafting_exp = 0;

		this.equip = new EquipData();
		this.inventory = new InventoryData();
		this.vault = new VaultData();
		this.desk = new DeskData();
		this.cards_vault = "1,0|2,0|3,0|4,0";
		this.reward_items = "0";
		this.stages = "0";
		this.missions ="0";
	}

}
