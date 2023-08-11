using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTradePanel : MonoBehaviour
{
    //Values
    Card card;
    public GameObject GM;
    public GameObject player;
    int cost = 0;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text priceNumber;
    public Text pointsNumber;
    public Text staminaNumber;
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

    public void OpenPanel(Card newCard,int price){
        card = newCard;
        cost = price;
        name.text = card.name;
        icon.sprite = card.icon;
        description.text = card.description;
        priceNumber.text = price.ToString();
        pointsNumber.text = card.price.ToString();
        staminaNumber.text = card.staminaCost.ToString();

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
        
        //Colors

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
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 1:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<TownPlayerStats>().GetStatValue("defense")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 2:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<TownPlayerStats>().vitality));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Heal";
            break;
            case 3:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(true);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
                actionSingle2.GetComponent<SingleModSlotController>().SetMod(card.defense+(int)Mathf.Round((card.secondaryPercent/100f)*player.GetComponent<TownPlayerStats>().GetStatValue("defense")));
                actionSingle2.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 4:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 5:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<TownPlayerStats>().GetStatValue("defense")));
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
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 10:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 11:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 12:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 13:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 14:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 15:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 16:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 17:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 18:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
            case 19:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack")));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
        }
    }
    
    public void BuyCard(){
    	if(player.GetComponent<TownPlayerStats>().gold >= cost){
    		player.GetComponent<TownPlayerStats>().PayGold(cost);
    		CardsVault.instance.Add(card);
        	ClosePanel();
    	}        
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
