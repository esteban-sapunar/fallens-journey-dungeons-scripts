using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PackCardController : MonoBehaviour
{
    //Values
    Card card;
    public Image icon;
    public GameObject cardPanel;
    public GameObject innerPanel;
    public Text name;
    public Text desc;
    public Text attack;
    public Text defense;
    public Text heal;
    public Text stamina;
    public GameObject attackPanel;
    public GameObject defensePanel;
    public GameObject healPanel;
    public GameObject player;

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
    public void AddCard(Card newCard){
    	card = newCard;
        cardPanel.SetActive(true);
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
            break;
            case 1:
                cardPanel.GetComponent<Image>().color = tier1Color;
            break;
            case 2:
                cardPanel.GetComponent<Image>().color = tier2Color;
            break;
            case 3:
                cardPanel.GetComponent<Image>().color = tier3Color;
            break;
            case 4:
                cardPanel.GetComponent<Image>().color = tier4Color;
            break;
        }
        //Action Panel
        switch((int)card.cardType){
            case 0:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString();      
            break;
            case 1:
                attackPanel.SetActive(false);
                defensePanel.SetActive(true);
                healPanel.SetActive(false);
                defense.text = (card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<TownPlayerStats>().GetStatValue("defense"))).ToString();
            break;
            case 2:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(true);
                heal.text = (card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<TownPlayerStats>().vitality)).ToString();
            break;
            case 3:
                attackPanel.SetActive(true);
                defensePanel.SetActive(true);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString();
                defense.text = (card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<TownPlayerStats>().GetStatValue("defense"))).ToString();      
            break;
            case 4:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString();
            break;
            case 5:
                attackPanel.SetActive(false);
                defensePanel.SetActive(true);
                healPanel.SetActive(false);
                defense.text = (card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<TownPlayerStats>().GetStatValue("defense"))).ToString();
            break;
            case 6:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
            break;
            case 7:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
            break;
            case 8:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
            break;
            case 9:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
            break;
            case 10:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
            break;
            case 11:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString(); 
            break;
            case 12:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString(); 
            break;
            case 13:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString(); 
            break;
            case 14:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString(); 
            break;
            case 15:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString(); 
            break;
            case 16:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
            break;
            case 17:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString(); 
            break;
            case 18:
                attackPanel.SetActive(false);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
            break;
            case 19:
                attackPanel.SetActive(true);
                defensePanel.SetActive(false);
                healPanel.SetActive(false);
                attack.text = (card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<TownPlayerStats>().GetStatValue("attack"))).ToString(); 
            break;
        }
        name.text = card.name;
        desc.text = card.description;
        stamina.text = card.staminaCost.ToString();
    	icon.sprite = card.icon;
    	icon.enabled = true;
    }
    public void ClearCard(){
    	card = null;
    	icon.sprite = null;
    	icon.enabled = false;
        cardPanel.SetActive(false);
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
