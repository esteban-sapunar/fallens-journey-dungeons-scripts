using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
	//Values
    public Equipment item;
    public Image icon;
    public GameObject panel;
    //Colors
    Color emptyColor;
    Color tier0Color;
    Color tier1Color;
    Color tier2Color;
    Color tier3Color;
    Color tier4Color;

    void Awake(){
        ColorUtility.TryParseHtmlString("#CF8A65",out emptyColor);
        ColorUtility.TryParseHtmlString("#74430E",out tier0Color);
        ColorUtility.TryParseHtmlString("#A6552A",out tier1Color);
        ColorUtility.TryParseHtmlString("#CBBCAF",out tier2Color);
        ColorUtility.TryParseHtmlString("#E5BF08",out tier3Color);
        ColorUtility.TryParseHtmlString("#32AEA8",out tier4Color);
    } 

    //Methods
    public void AddEquipment(Equipment newItem){
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
    }
    public void ClearEquipment(){
    	item = null;
    	icon.sprite = null;
    	icon.enabled = false;
        gameObject.GetComponent<Image>().color = emptyColor;
    }
    public void OnRemoveEquipment(){
    	EquipmentManager.instance.Unequip((int)item.equipSlot);
    }
    public void UseEquipment(){
    	if(item != null){
    		item.Use();
    	}
    }
    public void OpenEquipment(){
    	if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemPanelController>().OpenPanel(item,true,false);
        }
    }
}
