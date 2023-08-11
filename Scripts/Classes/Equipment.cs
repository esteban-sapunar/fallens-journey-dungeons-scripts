using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
	//Values
	public int attackModifier;
	public int armorModifier;
	public int defenseModifier;
	public int weightModifier;
	public int dodgeModifier;
	public int dodgeValueModifier;
	public int criticalMultiModifier;
	public int criticalModifier;
	public int capacityModifier;

	public EquipmentSlot equipSlot;
	public WeaponType weaponType;

	//Methods
	public override void Use(){
		EquipmentManager.instance.Equip(this);
		RemoveFromInventory();
	}
	public override void UnEquip(){
		int slotIndex = (int)equipSlot;
		EquipmentManager.instance.Unequip(slotIndex);
	}

}
public enum EquipmentSlot {Head,Chest,Legs,Feet,Weapon,Shield,Bag}
public enum WeaponType {None,SmallBlade,NormalBlade,LargeBlade,SmallAxe,GreatAxe,Spear,Mace,Hammer}
