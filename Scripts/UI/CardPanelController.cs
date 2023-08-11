using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanelController : MonoBehaviour
{
    //Values
    Card card;
    GameObject slot;
    public GameObject GM;
    public GameObject player;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text blockNumber;
    public Text tier;
    public Text type;
    public Text weaponsBuffText;
    public Text weaponsDebuffText;
    public GameObject cardPanel;
    public GameObject innerPanel;
    //Action UI
    public GameObject actionSingle;
    public GameObject actionSingle2;
    //Colors
    Color attackColor;
    Color defenseColor;
    Color healingColor;
    Color passiveColor;
    Color tier0Color;
    Color tier1Color;
    Color tier2Color;
    Color tier3Color;
    Color tier4Color;


    void Awake(){
        ColorUtility.TryParseHtmlString("#BC4715",out attackColor);
        ColorUtility.TryParseHtmlString("#0A90BC",out defenseColor);
        ColorUtility.TryParseHtmlString("#5BCD1B",out healingColor);
        ColorUtility.TryParseHtmlString("#CACACA",out passiveColor);
        ColorUtility.TryParseHtmlString("#593B19",out tier0Color);
        ColorUtility.TryParseHtmlString("#FF6623",out tier1Color);
        ColorUtility.TryParseHtmlString("#CCD9D9",out tier2Color);
        ColorUtility.TryParseHtmlString("#FFE84A",out tier3Color);
        ColorUtility.TryParseHtmlString("#00FFF3",out tier4Color);
    }

    //Methods
    public void ClosePanel(){
        gameObject.SetActive(false);
    }

    public void OpenPanel(Card newCard,GameObject newslot){
        card = newCard;
        slot = newslot;
        name.text = card.name;
        icon.sprite = card.icon;
        description.text = card.description;

        if(card.hasWeaponsBuffs == true){
            if(card.weaponsBuff.Count > 0){
                string auxString = "+"+card.weaponsBuffPercent+"% ( ";
                foreach(WeaponType type in card.weaponsBuff){
                    auxString += type.ToString() + " ";
                }
                auxString += ")";
                weaponsBuffText.text = auxString;
            }
            else {
                weaponsBuffText.text = "";
            }

            if(card.weaponsDebuff.Count > 0){
                string auxString = ""+card.weaponsDebuffPercent+"% ( ";
                foreach(WeaponType type in card.weaponsDebuff){
                    auxString += type.ToString() + " ";
                }
                auxString += ")";
                weaponsDebuffText.text = auxString;
            }
            else {
                weaponsDebuffText.text = "";
            }
        }
        else {
            weaponsBuffText.text = "";
            weaponsDebuffText.text = "";
        }

        if((int)card.cardSubType != 0){
            type.text = card.cardSubType.ToString();
        }
        else {
            type.text = "";
        }

        switch ((int)card.cardType){
            case 0:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 1:
                innerPanel.GetComponent<Image>().color = defenseColor;
            break;
            case 2:
                innerPanel.GetComponent<Image>().color = healingColor;
            break;
            case 3:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 4:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 5:
                innerPanel.GetComponent<Image>().color = defenseColor;
            break;
            case 6:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 7:
                innerPanel.GetComponent<Image>().color = passiveColor;
            break;
            case 8:
                innerPanel.GetComponent<Image>().color = passiveColor;
            break;
            case 9:
                innerPanel.GetComponent<Image>().color = passiveColor;
            break;
            case 10:
                innerPanel.GetComponent<Image>().color = passiveColor;
            break;
            case 11:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 12:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 13:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 14:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 15:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 16:
                innerPanel.GetComponent<Image>().color = passiveColor;
            break;
            case 17:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
            case 18:
                innerPanel.GetComponent<Image>().color = passiveColor;
            break;
            case 19:
                innerPanel.GetComponent<Image>().color = attackColor;
            break;
        }
        switch (card.tier){
            case 0:
                cardPanel.GetComponent<Image>().color = tier0Color;
                tier.text = "I";
            break;
            case 1:
                cardPanel.GetComponent<Image>().color = tier1Color;
                tier.text = "II";
            break;
            case 2:
                cardPanel.GetComponent<Image>().color = tier2Color;
                tier.text = "III";
            break;
            case 3:
                cardPanel.GetComponent<Image>().color = tier3Color;
                tier.text = "IV";
            break;
            case 4:
                cardPanel.GetComponent<Image>().color = tier4Color;
                tier.text = "V";
            break;
        }
        
        //Action Panel
        switch((int)card.cardType){
            case 0:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 1:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 2:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Heal";
            break;
            case 3:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(true);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
                actionSingle2.GetComponent<SingleModSlotController>().SetMod(card.defense+(int)Mathf.Round((card.secondaryPercent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense")));
                actionSingle2.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 4:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 5:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 6:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 7:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 8:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 9:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Heal";
            break;
            case 10:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 11:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 12:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 13:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 14:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 15:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 16:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 17:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 18:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 19:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
        }
    }
    public void UseCard(Vector3 pos){
        if(player.GetComponent<PlayerStats>().currentStamina >= card.staminaCost && !GM.GetComponent<BattleController>().cardsPaused){
        	GM.GetComponent<BattleController>().PauseCards();
            //Action
            switch((int)card.cardType){
                case 0:
                    GM.GetComponent<BattleController>().AttackOneEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,card.secondaryPercent);
                break;
                case 1:
                    player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                    GM.GetComponent<BattleController>().DiscardCard(card);
                    slot.GetComponent<CardSlot>().UnsetCard();
                    player.GetComponent<PlayerStats>().SetBlock(card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense")));
                    blockNumber.text = player.GetComponent<PlayerStats>().blockAmount.ToString();
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 2:
                    player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                    GM.GetComponent<BattleController>().DiscardCard(card);
                    slot.GetComponent<CardSlot>().UnsetCard();
                    SoundManager.instance.PlayEffect("Magic",false,false);
                    player.GetComponent<PlayerStats>().Heal(card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality));
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 3:
                    GM.GetComponent<BattleController>().AttackOneEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,0);
                break;
                case 4:
                    GM.GetComponent<BattleController>().AttackOneEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,0);
                break;
                case 5:
                    player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                    GM.GetComponent<BattleController>().DiscardCard(card);
                    slot.GetComponent<CardSlot>().UnsetCard();
                    player.GetComponent<PlayerStats>().SetBlock(card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense")));
                    blockNumber.text = player.GetComponent<PlayerStats>().blockAmount.ToString();
                    slot.SetActive(true);
                    GM.GetComponent<BattleController>().DrawCard(slot);
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 6:
                    GM.GetComponent<BattleController>().AttackOneEnemy((int)Mathf.Round(card.percent*player.GetComponent<PlayerStats>().blockAmount/100f),pos,gameObject,0);
                break;
                case 7:
                    if(player.GetComponent<PlayerStats>().permaStrength < card.strength){
                        SoundManager.instance.PlayEffect("Magic",false,false);
                        player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                        GM.GetComponent<BattleController>().DiscardCard(card);
                        slot.GetComponent<CardSlot>().UnsetCard();
                        player.GetComponent<PlayerStats>().permaStrength = card.strength;
                        GM.GetComponent<BattleController>().ActivePlayerStatus(0,card.strength,0,0,0,card.duration);
                        GM.GetComponent<BattleController>().UpdateCardsNumbers();
                    }
                break;
                case 8:
                    if(player.GetComponent<PlayerStats>().permaDef < player.GetComponent<PlayerStats>().GetStatValue("defense")+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/3f)){
                        player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                        SoundManager.instance.PlayEffect("Magic",false,false);
                        GM.GetComponent<BattleController>().DiscardCard(card);
                        slot.GetComponent<CardSlot>().UnsetCard();
                        player.GetComponent<PlayerStats>().permaDef = player.GetComponent<PlayerStats>().GetStatValue("defense")+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/3f);
                        GM.GetComponent<BattleController>().ActivePlayerStatus(2,player.GetComponent<PlayerStats>().GetStatValue("defense")+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/3f),0,0,0,card.duration);
                        GM.GetComponent<BattleController>().UpdateCardsNumbers();

                    }                   
                break;
                case 9:
                    if(player.GetComponent<PlayerStats>().permaHealing < card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality)){
                        player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                        SoundManager.instance.PlayEffect("Magic",false,false);
                        GM.GetComponent<BattleController>().DiscardCard(card);
                        slot.GetComponent<CardSlot>().UnsetCard();
                        player.GetComponent<PlayerStats>().permaHealing = card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality);
                        GM.GetComponent<BattleController>().ActivePlayerStatus(3,card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality),0,0,0,card.duration);
                        GM.GetComponent<BattleController>().UpdateCardsNumbers();

                    }
                    
                break;
                case 10:
                    if(player.GetComponent<PlayerStats>().permaOffBlock < (card.damage+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/1f))){
                        player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                        SoundManager.instance.PlayEffect("Magic",false,false);
                        GM.GetComponent<BattleController>().DiscardCard(card);
                        slot.GetComponent<CardSlot>().UnsetCard();
                        player.GetComponent<PlayerStats>().permaOffBlock  = (card.damage+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/1f));
                        GM.GetComponent<BattleController>().ActivePlayerStatus(4,(card.damage+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/1f)),0,0,0,card.duration);
                        GM.GetComponent<BattleController>().UpdateCardsNumbers();

                    }
                    
                break;
                case 11:
                    GM.GetComponent<BattleController>().AttackOneEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,0);
                break;
                case 12:
                    int percent = card.secondaryPercent + (int)Mathf.Round((player.GetComponent<PlayerStats>().agility+player.GetComponent<PlayerStats>().extraAgility)/2f);
                    GM.GetComponent<BattleController>().MultiAttackOneEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,percent,card.duration);
                break;
                case 13:
                    GM.GetComponent<BattleController>().AttackOneEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,card.tertiaryPercent);
                break;
                case 14:
                    GM.GetComponent<BattleController>().AttackAllEnemies((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,gameObject);
                break;
                case 15:
                    GM.GetComponent<BattleController>().StunOneEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,card.secondaryPercent+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/2f));
                break;
                case 16:
                    if(player.GetComponent<PlayerStats>().permaStrength < card.strength){
                        SoundManager.instance.PlayEffect("Magic",false,false);
                        player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                        GM.GetComponent<BattleController>().DiscardCard(card);
                        slot.GetComponent<CardSlot>().UnsetCard();
                        player.GetComponent<PlayerStats>().permaStrength = card.strength;
                        player.GetComponent<PlayerStats>().permaAgility = card.agility;
                        player.GetComponent<PlayerStats>().permaDexterity = card.dexterity;
                        GM.GetComponent<BattleController>().ActivePlayerStatus(5,card.strength,card.extraStamina,card.agility,card.dexterity,card.duration);
                        GM.GetComponent<BattleController>().UpdateCardsNumbers();
                    }
                break;
                case 17:
                    GM.GetComponent<BattleController>().AttackOneStunnedEnemy((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,card.secondaryPercent+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/2f));
                break;
                case 18:
                    if(player.GetComponent<PlayerStats>().permaEvasion < card.evasion){
                        SoundManager.instance.PlayEffect("Magic",false,false);
                        player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
                        GM.GetComponent<BattleController>().DiscardCard(card);
                        slot.GetComponent<CardSlot>().UnsetCard();
                        player.GetComponent<PlayerStats>().permaEvasion = card.evasion;
                        GM.GetComponent<BattleController>().ActivePlayerStatus(6,card.evasion,0,0,0,card.duration);
                        GM.GetComponent<BattleController>().UpdateCardsNumbers();
                    }
                break;
                case 19:
                    GM.GetComponent<BattleController>().AttackOneEnemyEvasion((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti,pos,gameObject,card.secondaryPercent,card.evasion);
                break;
            }
            ClosePanel();
        }
    }
    public void DiscardCard(){

        GM.GetComponent<BattleController>().DiscardCard(card);
        slot.GetComponent<CardSlot>().UnsetCard();
        ClosePanel();
    }
    public void FinishCardUse(){
        player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
        if((int)card.cardType == 13 || (int)card.cardType == 19){
            int rnd = Random.Range(1,101);
            int percent = card.secondaryPercent+(int)Mathf.Round((player.GetComponent<PlayerStats>().agility+player.GetComponent<PlayerStats>().extraAgility)/2f);
            if(rnd > percent){
                GM.GetComponent<BattleController>().DiscardCard(card);
                slot.GetComponent<CardSlot>().UnsetCard();
            }
        }
        else{
            GM.GetComponent<BattleController>().DiscardCard(card);
            slot.GetComponent<CardSlot>().UnsetCard();
        }
        if((int)card.cardType == 3){
            player.GetComponent<PlayerStats>().SetBlock(card.defense+(int)Mathf.Round((card.secondaryPercent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense")));
            blockNumber.text = player.GetComponent<PlayerStats>().blockAmount.ToString();
        }
        if((int)card.cardType == 4){
            slot.SetActive(true);
            GM.GetComponent<BattleController>().DrawCard(slot);
        }
        if((int)card.cardType == 11){
            int percent = card.secondaryPercent + (int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/2f);
            player.GetComponent<PlayerStats>().SetOneTimeDamageMulti(card.secondaryPercent/100);
        }
        GM.GetComponent<BattleController>().UpdateCardsNumbers();
    }
    private float WeaponBonus() {
        if(card.hasWeaponsBuffs == true){
            int weaponType = player.GetComponent<EquipmentManager>().WeaponType();
            foreach(WeaponType type in card.weaponsBuff){
                if((int)type == weaponType){
                    return 1f + card.weaponsBuffPercent/100f;
                }
            }
            foreach(WeaponType type in card.weaponsDebuff){
                if((int)type == weaponType){
                    return 1f + card.weaponsDebuffPercent/100f;
                }
            }
            return 1f;
        }
        else {
            return 1f;
        }
    }
}
