using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootSlot : MonoBehaviour
{
    //Values
    Item item;
    public GameObject DC;
    public GameObject RC;
    //UI
    public Image icon;
    public Text name;
    public Image iconBig;
    public Text description;
    public GameObject damageSingle;
    public GameObject armorSingle;
    public GameObject defenseSingle;
    public GameObject weightSingle;

    //Colors
    Color emptyColor;
    Color tier0Color;
    Color tier1Color;
    Color tier2Color;
    Color tier3Color;
    Color tier4Color;

    void Awake(){
        ColorUtility.TryParseHtmlString("#CF8A65",out emptyColor);
        ColorUtility.TryParseHtmlString("#74510E",out tier0Color);
        ColorUtility.TryParseHtmlString("#A6552A",out tier1Color);
        ColorUtility.TryParseHtmlString("#CBBCAF",out tier2Color);
        ColorUtility.TryParseHtmlString("#E5BF08",out tier3Color);
        ColorUtility.TryParseHtmlString("#32AEA8",out tier4Color);
    } 

    //Methods
    public void SetItem(Item newItem){
    	item = newItem;
    	icon.sprite = item.icon;
    	icon.enabled = true;
        switch (item.tier){
            case 0:
                gameObject.GetComponent<Image>().color = tier0Color;
            break;
            case 1:
                gameObject.GetComponent<Image>().color = tier1Color;
            break;
            case 2:
                gameObject.GetComponent<Image>().color = tier2Color;
            break;
            case 3:
                gameObject.GetComponent<Image>().color = tier3Color;
            break;
            case 4:
                gameObject.GetComponent<Image>().color = tier4Color;
            break;
        }
    	gameObject.GetComponent<Button>().interactable = true;
    }
    public void SelectItem(){
    	RC.GetComponent<RewardsController>().AddItem(item,gameObject);
    	name.text = item.name;
    	iconBig.enabled = true;
    	iconBig.sprite = item.icon;
    	description.text = item.description;
    	//mods
        damageSingle.SetActive(false);
        armorSingle.SetActive(false);
        defenseSingle.SetActive(false);
        weightSingle.SetActive(false);
    	if(item is Equipment){
            Equipment equip = (Equipment)item;
            if(equip.attackModifier > 0){
                damageSingle.SetActive(true);
                damageSingle.GetComponent<SingleModSlotController>().SetMod(equip.attackModifier);
            }
            if(equip.armorModifier > 0){
                armorSingle.SetActive(true);
                armorSingle.GetComponent<SingleModSlotController>().SetMod(equip.armorModifier);
            }
            if(equip.defenseModifier > 0){
                defenseSingle.SetActive(true);
                defenseSingle.GetComponent<SingleModSlotController>().SetMod(equip.defenseModifier);
            }
            if(equip.weightModifier > 0){
                weightSingle.SetActive(true);
                weightSingle.GetComponent<SingleModSlotController>().SetMod(equip.weightModifier);
            }
        }
    }
}
