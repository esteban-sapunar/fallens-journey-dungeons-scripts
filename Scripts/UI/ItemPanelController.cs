using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelController : MonoBehaviour
{
	//Values
    Item item;
    bool onVault = false;
    public bool onTown = false;
    public GameObject validationPanel;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text tier;
    public Text type;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;
    public GameObject moveButton;
    public GameObject iconPanel;
    //Mod UI
    public GameObject damageSingle;
    public GameObject armorSingle;
    public GameObject defenseSingle;
    public GameObject weightSingle;
    public GameObject dodgeSingle;
    public GameObject criticalSingle;
    public GameObject criticalMultiSingle;

    //Colors
    Color tier0Color;
    Color tier1Color;
    Color tier2Color;
    Color tier3Color;
    Color tier4Color;

    void Awake(){
        ColorUtility.TryParseHtmlString("#74430E",out tier0Color);
        ColorUtility.TryParseHtmlString("#A6552A",out tier1Color);
        ColorUtility.TryParseHtmlString("#CBBCAF",out tier2Color);
        ColorUtility.TryParseHtmlString("#E5BF08",out tier3Color);
        ColorUtility.TryParseHtmlString("#32AEA8",out tier4Color);
    }

    //Methods
    public void ClosePanel(){
        if(onTown == false){
            useButton.SetActive(false);
        }
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    	gameObject.SetActive(false);
        damageSingle.SetActive(false);
        armorSingle.SetActive(false);
        defenseSingle.SetActive(false);
        weightSingle.SetActive(false);
        dodgeSingle.SetActive(false);
        criticalSingle.SetActive(false);
        criticalMultiSingle.SetActive(false);
        if(onTown){
            moveButton.SetActive(false);
        }
    }
    public void OpenPanel(Item newItem, bool fromEquip, bool fromVault){
    	if(onTown == false){
        	useButton.SetActive(false);
    	}
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);

        damageSingle.SetActive(false);
        armorSingle.SetActive(false);
        defenseSingle.SetActive(false);
        weightSingle.SetActive(false);
        dodgeSingle.SetActive(false);
        criticalSingle.SetActive(false);
        criticalMultiSingle.SetActive(false);

        if(fromVault){
            onVault = true;
        }
        else{
            onVault = false;
        }
    	item = newItem;
    	name.text = item.name;
    	icon.sprite = item.icon;
    	description.text = item.description;

    	switch (item.tier){
            case 0:
                iconPanel.GetComponent<Image>().color = tier0Color;
                tier.text = "I";
            break;
            case 1:
                iconPanel.GetComponent<Image>().color = tier1Color;
                tier.text = "II";
            break;
            case 2:
                iconPanel.GetComponent<Image>().color = tier2Color;
                tier.text = "III";
            break;
            case 3:
                iconPanel.GetComponent<Image>().color = tier3Color;
                tier.text = "IV";
            break;
            case 4:
                iconPanel.GetComponent<Image>().color = tier4Color;
                tier.text = "V";
            break;
        }

    	if(item is Equipment){
            Equipment equip = (Equipment)item;
            if((int)equip.weaponType != 0){
            	type.text = equip.weaponType.ToString();
            }
            else {
            	type.text = "";
            }
            
            if(fromEquip){
                unequipButton.SetActive(true);
            }
            else{
                if(onVault == false){
                    equipButton.SetActive(true);
                }

                dropButton.SetActive(true);

                if(onTown){
                    moveButton.SetActive(true);
                }
            }
            //mods
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
            if(equip.dodgeModifier > 0){
                dodgeSingle.SetActive(true);
                dodgeSingle.GetComponent<SingleModSlotController>().SetMod(equip.dodgeModifier);
            }
            if(equip.criticalModifier > 0){
                criticalSingle.SetActive(true);
                criticalSingle.GetComponent<SingleModSlotController>().SetMod(equip.criticalModifier);
            }
            if(equip.criticalMultiModifier > 0){
                criticalMultiSingle.SetActive(true);
                criticalMultiSingle.GetComponent<SingleModSlotController>().SetMod(equip.criticalMultiModifier);
            }
    	}
        else{
        	type.text = "";
            dropButton.SetActive(true);

            if(onTown){
                moveButton.SetActive(true);
            }
            else{
               useButton.SetActive(true); 
            }
        }
    }
    public void UseItem(){
    	SoundManager.instance.PlayEffect("Equip",false,false);
        item.Use();
        ClosePanel();
    }
    public void UnEquipItem(){
    	SoundManager.instance.PlayEffect("Equip",false,false);
        if(item is Equipment){
            item.UnEquip();
        }
        ClosePanel();
    }
    public void DropItem(){
        if(onVault){
            Vault.instance.Remove(item);
        }
        else{
            Inventory.instance.Remove(item);
        }
        ClosePanel();
    }
    public void ConfirmDropItem(){
        validationPanel.SetActive(true);
        validationPanel.GetComponent<ValidationPanelController>().delegatedMethodCall = DropItem;
    }
    public void MoveItem(){
    	SoundManager.instance.PlayEffect("Equip",false,false);
        if(onVault){
            Inventory.instance.Add(item);
            Vault.instance.Remove(item);
        }
        else{
            Vault.instance.Add(item);
            Inventory.instance.Remove(item);
        }
        ClosePanel();
    }
}
