using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownPlayerStats : MonoBehaviour
{
	//Health
	public int maxHealth = 50;
	public int currentHealth;
    public int baseStamina = 25;
    public int maxStamina = 50;
    public int currentStamina;
	//Stats
    public int battlePower;
    public Stat attack;
    public Stat armor;
    public Stat dodge;
    public Stat dodgeValue;
    public Stat criticalChance;
    public Stat criticalMulti;
    public Stat defense;
    public Stat weight;
    public int level;
    public int exp;
    public int expNextLevel;
    public int gold;
    public int cardPoints;
    public int craftingLevel;
    public int craftingExp;
    public int craftingExpNextLevel;
    public int capacity = 1;
    public string[] arrayStages;
    public string[] arrayMissions;

    public int strength;
    public int agility;
    public int dexterity;
    public int resistance;
    public int vitality;
    public int points; 
    //Battle Stats
    public int blockAmount;
    //Crafting UI
    public Text craftingLevelText;
    public Slider craftingSlider;
    public Text craftingExpText;
    public Text craftingNextText;
    //Stats UI
    public Text powerUI;
    public Text attackUI;
    public Text armorUI;
    public Text defenseUI;
    public Text weightUI;
    public Text dodgeUI;
    public Text dodgeValueUI;
    public Text criticalChanceUI;
    public Text criticalMultiUI;
    public Slider staminaSlider;
    public Slider healthSlider;
    public Text currentStaminaNumber;
    public Text maxStaminaNumber;
    public Text currentHealthNumber;
    public Text maxHealthNumber;

    public Text expNumber;
    public Text goldNumber;
    public Text trainerGoldNumber;
    public Text trainerCardPointsNumber;
    public Text shopGoldNumber;
    public Text blacksmithGoldNumber;
    public Text levelNumber;
    public Text nextLevelNumber;
    public Text pointsNumber;
    public Text strengthNumber;
    public Text agilityNumber;
    public Text dexterityNumber;
    public Text resistanceNumber;
    public Text vitalityNumber;

    public Transform blacksmithParent;
    //Delegate
    public delegate void OnPlayerDie();
    public OnPlayerDie OnPlayerDieCallback;

    public static TownPlayerStats instance;

    void Awake(){
        if(instance != null){
            Debug.LogWarning("More than one instace of TownPlayerStats found!");
            return;
        }
        instance = this;
    }

    //Methods
    public void InitData(){
        EquipmentManager.instance.OnEquipChangedCallback += OnEquipHasChanged;
        currentHealth = maxHealth;
        maxStamina = baseStamina;
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        maxHealthNumber.text = maxHealth.ToString();
        currentHealthNumber.text = maxHealth.ToString();
        expNextLevel = 10*level +10*(level-1)*(level-1);
        craftingExpNextLevel = 10*craftingLevel +10*(craftingLevel-1)*(craftingLevel-1);
        craftingSlider.maxValue = craftingExpNextLevel;
        craftingSlider.value = craftingExp;
        craftingExpText.text = craftingExp.ToString();
        craftingNextText.text = craftingExpNextLevel.ToString();
        craftingLevelText.text = craftingLevel.ToString();
        levelNumber.text = level.ToString();
        expNumber.text = exp.ToString();
        nextLevelNumber.text = expNextLevel.ToString();
        goldNumber.text = gold.ToString();
        trainerGoldNumber.text = gold.ToString();
        trainerCardPointsNumber.text = cardPoints.ToString();
        shopGoldNumber.text = gold.ToString();
        blacksmithGoldNumber.text = gold.ToString();
        pointsNumber.text = points.ToString();
        strengthNumber.text = strength.ToString();
        agilityNumber.text = agility.ToString();
        dexterityNumber.text = dexterity.ToString();
        resistanceNumber.text = resistance.ToString();
        vitalityNumber.text = vitality.ToString();
        criticalChance.SetBaseValue(5);
        criticalChance.AddModifier((int)Mathf.Round(dexterity/2f));
        criticalChanceUI.text = criticalChance.GetValue().ToString();
        criticalMulti.SetBaseValue(125);
        criticalMultiUI.text = criticalMulti.GetValue().ToString();
        dodgeValue.SetBaseValue(60);
        dodge.AddModifier((int)Mathf.Round(agility/2f));
        dodgeUI.text = dodge.GetValue().ToString();
        dodgeValueUI.text = dodgeValue.GetValue().ToString();
        attack.AddModifier(strength);
        SetBattlePower();
    }

    public int GetStatValue(string type){
        switch (type){
            case "attack":
                return attack.GetValue();
                break;
            case "defense":
                return defense.GetValue();
                break;
            case "armor":
                return armor.GetValue();
                break;
            case "weight":
                return weight.GetValue();
                break;
            case "dodge":
                return dodge.GetValue();
                break;
            case "dodgeValue":
                return dodgeValue.GetValue();
                break;
            case "criticalChance":
                return criticalChance.GetValue();
                break;
            case "criticalMulti":
                return criticalMulti.GetValue();
                break;
            default:
                return 0;
                break;
        }
    }

    //Equipment
    void OnEquipHasChanged (Equipment newItem,Equipment oldItem){
        if(newItem != null){
            attack.AddModifier(newItem.attackModifier);
            armor.AddModifier(newItem.armorModifier);
            defense.AddModifier(newItem.defenseModifier);
            weight.AddModifier(newItem.weightModifier);
            dodge.AddModifier(newItem.dodgeModifier);
            dodgeValue.AddModifier(newItem.dodgeValueModifier);
            criticalChance.AddModifier(newItem.criticalModifier);
            criticalMulti.AddModifier(newItem.criticalMultiModifier);
            capacity += newItem.capacityModifier;
        }
        if(oldItem != null){
            attack.RemoveModifier(oldItem.attackModifier);
            armor.RemoveModifier(oldItem.armorModifier);
            defense.RemoveModifier(oldItem.defenseModifier);
            weight.RemoveModifier(oldItem.weightModifier);
            dodge.RemoveModifier(oldItem.dodgeModifier);
            dodgeValue.RemoveModifier(oldItem.dodgeValueModifier);
            criticalChance.RemoveModifier(oldItem.criticalModifier);
            criticalMulti.RemoveModifier(oldItem.criticalMultiModifier);
            capacity -= oldItem.capacityModifier;
        }
        criticalChanceUI.text = criticalChance.GetValue().ToString();
        criticalMultiUI.text = criticalMulti.GetValue().ToString();
        dodgeUI.text = dodge.GetValue().ToString();
        dodgeValueUI.text = dodgeValue.GetValue().ToString();
        attackUI.text = attack.GetValue().ToString();
        armorUI.text = armor.GetValue().ToString();
        defenseUI.text = defense.GetValue().ToString();
        weightUI.text = weight.GetValue().ToString();
        maxStamina = baseStamina - (int)Mathf.Round(weight.GetValue()/2f);
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
        SetBattlePower();
    }

    //Stats
    public void LevelUp(){
        SoundManager.instance.PlayEffect("Point",false,false);
    	level +=1;
		exp -= expNextLevel;
		points +=1;
		expNextLevel = 10*level +10*(level-1)*(level-1);
        levelNumber.text = level.ToString();
        expNumber.text = exp.ToString();
        nextLevelNumber.text = expNextLevel.ToString();
        pointsNumber.text = points.ToString();
        SetBattlePower();
    }

    public void ExpUp(int newExp){
        exp += newExp;
        expNumber.text = exp.ToString();     
    }

    public void CraftingExpUp(int exp){
        craftingExp += exp;
        craftingExpText.text = craftingExp.ToString();
        craftingSlider.value = craftingExp;
        if(craftingExp >= craftingExpNextLevel){
            CraftingLevelUp();
        }        
    }

    public void CraftingLevelUp(){
        craftingLevel +=1;
        craftingExp -= craftingExpNextLevel;
        craftingExpNextLevel = 10*craftingLevel +10*(craftingLevel-1)*(craftingLevel-1);
        craftingSlider.maxValue = craftingExpNextLevel;
        craftingSlider.value = craftingExp;
        craftingLevelText.text = craftingLevel.ToString();
        craftingExpText.text = craftingExp.ToString();
        craftingNextText.text = craftingExpNextLevel.ToString();
        BlacksmithSlot[] blacksmithslots = blacksmithParent.GetComponentsInChildren<BlacksmithSlot>();
        foreach(BlacksmithSlot slot in blacksmithslots){
          slot.Init();
        }
    }

    public void StrengthUp(){
        SoundManager.instance.PlayEffect("Point",false,false);
    	attack.RemoveModifier(strength);
    	strength += 1;
    	points -= 1;
    	attack.AddModifier(strength);
    	strengthNumber.text = strength.ToString();
    	pointsNumber.text = points.ToString();
    	attackUI.text = attack.GetValue().ToString();
        SetBattlePower();
    }
    public void AgilityUp(){
        SoundManager.instance.PlayEffect("Point",false,false);
        dodge.RemoveModifier((int)Mathf.Round(agility/2f));
        agility += 1;
        points -= 1;
        dodge.AddModifier((int)Mathf.Round(agility/2f));
        agilityNumber.text = agility.ToString();
        pointsNumber.text = points.ToString();
        dodgeUI.text = dodge.GetValue().ToString();
        SetBattlePower();
    }
    public void DexterityUp(){
        SoundManager.instance.PlayEffect("Point",false,false);
        criticalChance.RemoveModifier((int)Mathf.Round(dexterity/2f));
        dexterity += 1;
        points -= 1;
        criticalChance.AddModifier((int)Mathf.Round(dexterity/2f));
        dexterityNumber.text = dexterity.ToString();
        pointsNumber.text = points.ToString();
        criticalChanceUI.text = criticalChance.GetValue().ToString();
        SetBattlePower();
    }
    public void ResistanceUp(){
        SoundManager.instance.PlayEffect("Point",false,false);
    	resistance += 1;
    	points -= 1;
    	baseStamina = 25 +resistance*2;
    	resistanceNumber.text = resistance.ToString();
    	pointsNumber.text = points.ToString();
    	maxStamina = baseStamina - (int)Mathf.Round(weight.GetValue()/2f);
    	currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
        SetBattlePower();
    }
    public void VitalityUp(){
        SoundManager.instance.PlayEffect("Point",false,false);
    	vitality += 1;
    	points -= 1;
    	maxHealth = 50 + vitality*5;
    	vitalityNumber.text = vitality.ToString();
    	pointsNumber.text = points.ToString();
    	healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        maxHealthNumber.text = maxHealth.ToString();
        currentHealthNumber.text = maxHealth.ToString();
        SetBattlePower();
    }
    public void PayGold(int price){
        if(price <= gold){
            SoundManager.instance.PlayEffect("Coins",false,false);
            gold -= price;
            goldNumber.text = gold.ToString();
            trainerGoldNumber.text = gold.ToString();
            shopGoldNumber.text = gold.ToString();
            blacksmithGoldNumber.text = gold.ToString();
        }
    }
    public void EarnGold(int earnings){
            SoundManager.instance.PlayEffect("Coins",false,false);
            gold += earnings;
            goldNumber.text = gold.ToString();
            trainerGoldNumber.text = gold.ToString();
            shopGoldNumber.text = gold.ToString();
            blacksmithGoldNumber.text = gold.ToString();
    }
    public void PayCardPoints(int price){
        if(price <= cardPoints){
            cardPoints -= price;
            trainerCardPointsNumber.text = cardPoints.ToString();
        }
    }
    public void EarnCardPoints(int earnings){
            SoundManager.instance.PlayEffect("Point",false,false);
            cardPoints += earnings;
            trainerCardPointsNumber.text = cardPoints.ToString();
    }
    public void SetBattlePower(){
        int dodgeAux = GetStatValue("dodge")*((int)Mathf.Round(GetStatValue("dodgeValue")/60f)*10);
        int critAux = GetStatValue("criticalChance")*((int)Mathf.Round(GetStatValue("criticalMulti")/125f)*15);
        battlePower = -180+(level-1)*5+GetStatValue("attack")*7+GetStatValue("armor")*9+GetStatValue("defense")*11+(maxStamina-10)*7+(maxHealth-50)*10+dodgeAux+critAux+TownDesk.instance.deskPower;
        powerUI.text = battlePower.ToString();
    }
}

