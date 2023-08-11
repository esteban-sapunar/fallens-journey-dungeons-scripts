using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	//Health
	public int maxHealth = 50;
	public int currentHealth {get; private set;}
    public int baseStamina = 25;
    public int maxStamina = 50;
    public int currentStamina {get; private set;}
	//Stats
    public Stat attack;
    public Stat armor;
    public Stat defense;
    public Stat weight;
    public Stat dodge;
    public Stat dodgeValue;
    public Stat criticalChance;
    public Stat criticalMulti;
    public int expGained {get; private set;}
    public int strength;
    public int agility;
    public int dexterity;
    public int resistance;
    public int vitality;
    public int capacity = 1;
    public string[] arrayStages;
    //Battle Stats
    public int blockAmount;
    public List <PlayerStatus> statusList = new List<PlayerStatus>();
    public List <GameObject> statusSlots = new List<GameObject>();
    public int extraStrength;
    public int extraAgility;
    public int extraDexterity;
    public int extraStamina;
    public int counterDamage = 0;
    public int oneTimeDamageMulti = 1;
    public int oneTurnEvasion = 0;
    //Stats UI
    public Text attackUI;
    public Text armorUI;
    public Text defenseUI;
    public Text weightUI;
    public Text critUI;
    public Text critMultiUI;
    public Text dodgeUI;
    public Text dodgeValueUI;
    public Slider staminaSlider;
    public Slider healthSlider;
    public Slider healthSliderMenu;
    public Slider staminaSliderMenu;
    public Text currentStaminaNumber;
    public Text maxStaminaNumber;
    public Text currentHealthNumber;
    public Text maxHealthNumber;
    public Text currentHealthNumberMenu;
    public Text maxHealthNumberMenu;
    public Text currentStaminaNumberMenu;
    public Text maxStaminaNumberMenu;
    public Text blockNumber;
    public Text armorNumber;
    //UI
    public GameObject damageEffect;
    public GameObject blockEffect;
    public GameObject healingEffect;
    public GameObject defenseEffect;
    public GameObject breathingEffect;
    public GameObject dodgeEffect;
    //Delegate
    public delegate void OnPlayerDie();
    public OnPlayerDie OnPlayerDieCallback;
    //Objects
    public GameObject GM;
    //Permanent Status
    public int permaStrength = 0;
    public int permaAgility = 0;
    public int permaDexterity = 0;
    public int permaDef = 0;
    public int permaHealing = 0;
    public int permaOffBlock = 0;
    public int permaEvasion = 0;
    //Temp Status
    public int tempMomentum = 0;

    #region Singleton
    public static PlayerStats instance;
    void Awake(){
        if(instance != null){
            Debug.LogWarning("More than one instace of PlayerStats found!");
            return;
        }
        instance = this;
    }
    #endregion

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
        staminaSliderMenu.maxValue = maxStamina;
        staminaSliderMenu.value = maxStamina;
        maxStaminaNumberMenu.text = maxStamina.ToString();
        currentStaminaNumberMenu.text = maxStamina.ToString();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        maxHealthNumber.text = maxHealth.ToString();
        currentHealthNumber.text = maxHealth.ToString();
        criticalChance.SetBaseValue(5);
        criticalChance.AddModifier((int)Mathf.Round(dexterity/2f));
        critUI.text = criticalChance.GetValue().ToString();
        /*criticalChanceUI.text = criticalChance.GetValue().ToString();*/
        criticalMulti.SetBaseValue(125);
        critMultiUI.text = criticalMulti.GetValue().ToString();
        dodge.AddModifier((int)Mathf.Round(agility/2f));
        dodgeValue.SetBaseValue(60);
        dodgeValueUI.text = dodgeValue.GetValue().ToString();
        dodgeUI.text = dodge.GetValue().ToString();
        /*dodgeUI.text = dodge.GetValue().ToString();*/
        attack.AddModifier(strength);
        attackUI.text = attack.GetValue().ToString();
    }

    public void GainExp(int exp){
        expGained += exp;
    }
    public void Heal(int healing){
        if(maxHealth-currentHealth > healing){
            currentHealth += healing;
        }
        else{
            currentHealth = maxHealth;
        }
        StartCoroutine(HealingEffect());
        healthSlider.value = currentHealth;
        currentHealthNumber.text = currentHealth.ToString(); 
        healthSliderMenu.value = currentHealth;
        currentHealthNumberMenu.text = currentHealth.ToString(); 
    }
    public void UseStamina(int cost){
        currentStamina -= cost;
        staminaSlider.value = currentStamina;
        currentStaminaNumber.text = currentStamina.ToString();
    }
    public void ResetBlock(){
        blockAmount = 0;
        blockNumber.text = blockAmount.ToString();
    }
    public void SetBlock(int def){
        SoundManager.instance.PlayEffect("Shield",false,false);
        blockAmount += def;
        StartCoroutine(DefenseEffect());
    }
    public void ResetStamina(){
        currentStamina = maxStamina;
        staminaSlider.value = currentStamina;
        currentStaminaNumber.text = currentStamina.ToString();
    }
    public void Damage(int damage){
        int finalDamage = 0;
        if(doesPlayerDodge()){
            dodgeAnim();
            finalDamage = dodgeResult(damage) - armor.GetValue();
        }
        else {
            finalDamage = damage - armor.GetValue();
        }
    	if(finalDamage < 0){
    		finalDamage = 0;
    	}
        if(blockAmount > 0){
            if(blockAmount <= finalDamage){
                finalDamage -= blockAmount;
                blockAmount = 0;
            }
            else{
                blockAmount -= finalDamage;
                finalDamage = 0;
            }
        }
        if(finalDamage > 0){
            SoundManager.instance.PlayEffect("Hit",false,false);
            StartCoroutine(DamageEffect());
        }
        else{
            StartCoroutine(BlockEffect());
        }
        blockNumber.text = blockAmount.ToString();
    	currentHealth -= finalDamage;
        healthSlider.value = currentHealth;
        currentHealthNumber.text = currentHealth.ToString(); 
    	if(currentHealth <= 0){
    		currentHealth = 0;
    		Die();
    	}

    }
    public void PoisonDamage(int damage){
        int finalDamage = damage;
        if(finalDamage < 0){
            finalDamage = 0;
        }
        if(finalDamage > 0){
            SoundManager.instance.PlayEffect("Debuff",false,false);
            StartCoroutine(DamageEffect());
        }
        else{
            StartCoroutine(BlockEffect());
        }
        currentHealth -= finalDamage;
        healthSlider.value = currentHealth;
        currentHealthNumber.text = currentHealth.ToString(); 
        if(currentHealth <= 0){
            currentHealth = 0;
            Die();
        }
    }

    public void Die(){
    	damageEffect.SetActive(false);
        OnPlayerDieCallback.Invoke();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        maxHealthNumber.text = maxHealth.ToString();
        currentHealthNumber.text = maxHealth.ToString();
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
    public void SetStatus(PlayerStatus newStatus){
        statusList.Add(newStatus);
        switch ((int)newStatus.statusType){
            case 0:
                extraStrength += newStatus.value;
                attack.AddModifier(newStatus.value);
            break;
            case 1:
            break;
            case 2:
            break;
            case 3:
                Heal(newStatus.value);
            break;
            case 4:
                counterDamage = newStatus.value;
            break;
            case 5:
                extraStrength += newStatus.value;
                extraAgility += newStatus.value3;
                extraDexterity += newStatus.value4;
                criticalChance.AddModifier((int)Mathf.Round(extraDexterity/2f));
                dodge.AddModifier((int)Mathf.Round(extraAgility/2f));
                attack.AddModifier(newStatus.value);
                extraStamina += newStatus.value2;
                maxStamina += newStatus.value2;
                staminaSlider.maxValue = maxStamina;
                maxStaminaNumber.text = maxStamina.ToString();
                breathingEffect.SetActive(true);
            break;
            case 6:                
            break;
            case 7:
            break;
        }
        foreach(GameObject statusSlot in statusSlots){
            if(!statusSlot.active){
                statusSlot.SetActive(true);
                statusSlot.GetComponent<StatusSlot>().SetStatus(newStatus);
                break;
            }
        }
        GM.GetComponent<BattleController>().UpdateCardsNumbers();
    }
    public void SetOneTimeDamageMulti(int value){
        oneTimeDamageMulti = value;
    }
    public void RemoveOneTimeDamageMulti(){
        oneTimeDamageMulti = 1;
    }
    public void SetOneTurnEvasion(int value){
        oneTurnEvasion += value;
        PlayerStatus newStatus = new PlayerStatus(7,oneTurnEvasion,0,0,0,1);
        if(StatusExist(PlayerStatusTypes.oneturnevasion)){
            GameObject currentSlot = SearchStatusSlot(PlayerStatusTypes.oneturnevasion);
            PlayerStatus currentStatus = SearchStatus(PlayerStatusTypes.oneturnevasion);
            statusList.Remove(currentStatus);
            statusList.Add(newStatus);
            currentSlot.GetComponent<StatusSlot>().SetStatus(newStatus);
        }
        else{
            SetStatus(newStatus);
        }        
    }
    public void NewTurn(){
        List <PlayerStatus> removeStatusList = new List<PlayerStatus>();
        foreach(PlayerStatus playerStatus in statusList){
            playerStatus.duration -= 1;
            switch ((int)playerStatus.statusType){
                case 0:
                    foreach(GameObject statusSlot in statusSlots){
                        if(statusSlot.active){
                            if(statusSlot.GetComponent<StatusSlot>().status == playerStatus){
                                if(playerStatus.duration >= 0){
                                    statusSlot.GetComponent<StatusSlot>().SetStatus(playerStatus);
                                }
                                if(playerStatus.duration == 0){
                                    extraStrength -= playerStatus.value;
                                    attack.RemoveModifier(playerStatus.value);
                                    removeStatusList.Add(playerStatus);
                                    statusSlot.SetActive(false);
                                }
                                break;
                            }
                        }
                    }
                break;
                case 1:
                    PoisonDamage(playerStatus.value);
                    foreach(GameObject statusSlot in statusSlots){
                        if(statusSlot.active){
                            if(statusSlot.GetComponent<StatusSlot>().status == playerStatus){
                                if(playerStatus.duration >= 0){
                                    statusSlot.GetComponent<StatusSlot>().SetStatus(playerStatus);
                                }
                                if(playerStatus.duration == 0){
                                    removeStatusList.Add(playerStatus);
                                    statusSlot.SetActive(false);
                                }
                                break;
                            }
                        }
                    }
                break;
                case 2:
                    SetBlock(playerStatus.value);
                    blockNumber.text = blockAmount.ToString();
                    foreach(GameObject statusSlot in statusSlots){
                        if(statusSlot.active){
                            if(statusSlot.GetComponent<StatusSlot>().status == playerStatus){
                                if(playerStatus.duration >= 0){
                                    statusSlot.GetComponent<StatusSlot>().SetStatus(playerStatus);
                                }
                                if(playerStatus.duration == 0){
                                    removeStatusList.Add(playerStatus);
                                    statusSlot.SetActive(false);
                                }
                                break;
                            }
                        }
                    }
                break;
                case 3:
                    Heal(playerStatus.value);
                    foreach(GameObject statusSlot in statusSlots){
                        if(statusSlot.active){
                            if(statusSlot.GetComponent<StatusSlot>().status == playerStatus){
                                if(playerStatus.duration >= 0){
                                    statusSlot.GetComponent<StatusSlot>().SetStatus(playerStatus);
                                }
                                if(playerStatus.duration == 0){
                                    removeStatusList.Add(playerStatus);
                                    statusSlot.SetActive(false);
                                }
                                break;
                            }
                        }
                    }
                break;
                case 4:
                    foreach(GameObject statusSlot in statusSlots){
                        if(statusSlot.active){
                            if(statusSlot.GetComponent<StatusSlot>().status == playerStatus){
                                if(playerStatus.duration >= 0){
                                    statusSlot.GetComponent<StatusSlot>().SetStatus(playerStatus);
                                }
                                if(playerStatus.duration == 0){
                                    counterDamage -= playerStatus.value;
                                    removeStatusList.Add(playerStatus);
                                    statusSlot.SetActive(false);
                                }
                                break;
                            }
                        }
                    }
                break;
                case 7:
                    foreach(GameObject statusSlot in statusSlots){
                        if(statusSlot.active){
                            if(statusSlot.GetComponent<StatusSlot>().status == playerStatus){
                                if(playerStatus.duration == 0){
                                    oneTurnEvasion = 0;
                                    removeStatusList.Add(playerStatus);
                                    statusSlot.SetActive(false);
                                }
                                break;
                            }
                        }
                    }
                break;
            }
        }
        foreach(PlayerStatus playerStatus in removeStatusList){
            statusList.Remove(playerStatus);
        }
        GM.GetComponent<BattleController>().UpdateCardsNumbers();
    }
    public void RemoveAllStatus(){
        List <PlayerStatus> removeStatusList = new List<PlayerStatus>();
        foreach(PlayerStatus playerStatus in statusList){
            switch ((int)playerStatus.statusType){
                case 0:
                        extraStrength -= playerStatus.value;
                        attack.RemoveModifier(playerStatus.value);
                        removeStatusList.Add(playerStatus);
                break;
                case 1:
                        removeStatusList.Add(playerStatus);
                break;
                case 2:
                        removeStatusList.Add(playerStatus);
                break;
                case 3:
                        removeStatusList.Add(playerStatus);
                break;
                case 4:
                        counterDamage -= playerStatus.value;
                        removeStatusList.Add(playerStatus);
                break;
                case 5:
                        extraStrength -= playerStatus.value;
                        attack.RemoveModifier(playerStatus.value);
                        extraAgility -= playerStatus.value3;
                        extraDexterity -= playerStatus.value4;
                        dodge.RemoveModifier(playerStatus.value3);
                        criticalChance.RemoveModifier(playerStatus.value4);                        
                        removeStatusList.Add(playerStatus);
                        extraStamina -= playerStatus.value2;
                        maxStamina -= playerStatus.value2;
                        staminaSlider.maxValue = maxStamina;
                        maxStaminaNumber.text = maxStamina.ToString();
                        breathingEffect.SetActive(false);
                break;
                case 6:
                        removeStatusList.Add(playerStatus);
                break;
                case 7:
                        removeStatusList.Add(playerStatus);
                break;
            
            }
        }
        foreach(PlayerStatus playerStatus in removeStatusList){
            statusList.Remove(playerStatus);
        }
        foreach(GameObject statusSlot in statusSlots){
            if(statusSlot.active){
                statusSlot.SetActive(false);
            }
        }
        permaStrength = 0;
        permaAgility = 0;
        permaDexterity = 0;
        permaDef = 0;
        permaHealing = 0;
        permaOffBlock = 0;
        permaEvasion = 0;
        oneTurnEvasion = 0;

    }

    //Dodge
    public bool doesPlayerDodge(){
        return dodge.GetValue()+permaEvasion+oneTurnEvasion > Random.Range(0,100);
    }

    public int dodgeResult(int value){
        return (int)Mathf.Round(value*(100f-dodgeValue.GetValue())/100f);
    }

    public void dodgeAnim(){
        dodgeEffect.SetActive(true);
        dodgeEffect.GetComponent<TextVerticalMovement>().StartMovement();
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
        attackUI.text = attack.GetValue().ToString();
        armorUI.text = armor.GetValue().ToString();
        armorNumber.text = armor.GetValue().ToString();
        defenseUI.text = defense.GetValue().ToString();
        weightUI.text = weight.GetValue().ToString();
        maxStamina = baseStamina - (int)Mathf.Round(weight.GetValue()/2f);
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
        staminaSliderMenu.maxValue = maxStamina;
        staminaSliderMenu.value = maxStamina;
        maxStaminaNumberMenu.text = maxStamina.ToString();
        currentStaminaNumberMenu.text = maxStamina.ToString();
        critUI.text = criticalChance.GetValue().ToString();
        critMultiUI.text = criticalMulti.GetValue().ToString();
        dodgeValueUI.text = dodgeValue.GetValue().ToString();
        dodgeUI.text = dodge.GetValue().ToString();
    }

    //Status
    bool StatusExist(PlayerStatusTypes type){
        foreach(GameObject statusSlot in statusSlots){
            if(statusSlot.active){
                if(statusSlot.GetComponent<StatusSlot>().StatusType() == type){
                    return true;
                }                
            }
        }
        return false;
    }

    GameObject SearchStatusSlot(PlayerStatusTypes type){
        foreach(GameObject statusSlot in statusSlots){
            if(statusSlot.active){
                if(statusSlot.GetComponent<StatusSlot>().StatusType() == type){
                    return statusSlot;
                }                
            }
        }
        return null;
    }

    PlayerStatus SearchStatus(PlayerStatusTypes type){
        foreach(PlayerStatus playerStatus in statusList){
            if(playerStatus.statusType == type){
                return playerStatus;
            }                
        }
        return null;
    }

    //IEnumerator
    IEnumerator DamageEffect(){
        damageEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        damageEffect.SetActive(false);
    }
    IEnumerator BlockEffect(){
        SoundManager.instance.PlayEffect("Block",false,false);
        blockEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        blockEffect.SetActive(false);
    }
    IEnumerator HealingEffect(){
        healingEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        healingEffect.SetActive(false);
    }
    IEnumerator DefenseEffect(){
        defenseEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        defenseEffect.SetActive(false);
    }

}
