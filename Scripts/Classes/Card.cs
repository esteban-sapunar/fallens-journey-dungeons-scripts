using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
	//values
	new public string name = "New Card";
	public Sprite icon = null;
	public string description;
	public int tier = 0;
	public int price = 0;

	public CardTypes cardType;
	public CardSubTypes cardSubType;
	public bool hasWeaponsBuffs = false;
	public int weaponsBuffPercent = 0;
	public List<WeaponType> weaponsBuff = new List<WeaponType>();
	public int weaponsDebuffPercent = 0;
	public List<WeaponType> weaponsDebuff = new List<WeaponType>();

	//Actions Values
	public int staminaCost = 0;
	public int damage = 0;
	public int defense = 0;
	public int healing = 0; 
	public int percent = 0;


	public int duration = 0;
	public int strength = 0;
	public int agility = 0;
	public int dexterity = 0;
	public int evasion = 0;
	public int extraStamina = 0;
	public int secondaryPercent = 0;
	public int tertiaryPercent = 0;
}
public enum CardTypes {Attack,Block,Healing,MixAttack,AttackPick,DefensePick,ShieldBash,PermanentStrength,PermanentDefensive,PermanentHealthRecovery,PermanentOffBlock,PreparationAttack,ChainAttacks,LightAttack,WideAttack,StunAttack,Breathing,AttackStunned,PermanentEvasion,EvasionAttack}
public enum CardSubTypes {None,Light,Normal,Heavy}