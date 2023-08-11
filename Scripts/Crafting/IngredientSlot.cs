using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour
{
    //Values
    public Item item;
    public bool ready = false;
    public int number;

    //UI
    public Image icon;
    public Text numberText;
    public GameObject blacksmithPanel;
    public GameObject panel;

    //Items
    Inventory inventory;
    Vault vault;

    //Colors
    Color activeColor;
    Color inactiveColor;
    
    public void Awake(){
	    ColorUtility.TryParseHtmlString("#36A94C",out activeColor);
	    ColorUtility.TryParseHtmlString("#E0604B",out inactiveColor);
	}

    //Base Functions
    public void Init(){
    	inventory = Inventory.instance;
    	vault = Vault.instance;
    	inventory.OnItemChangedCallback += Check;
    	vault.OnVaultItemChangedCallback += Check;
    	icon.sprite = item.icon;
    	numberText.text = number.ToString();
    	Check();
    }

    //Methods
    public void Check(){
    	int count = inventory.Count(item)+vault.Count(item);
    	if(count >= number){
    		ready = true;
    		panel.GetComponent<Image>().color = activeColor;
    	}
    	else{
    		ready = false;
    		panel.GetComponent<Image>().color = inactiveColor;
    	}
    }

    public void OpenPanel(){
    	blacksmithPanel.SetActive(true);
    	blacksmithPanel.GetComponent<BlacksmithPanelController>().OpenPanel(item);
    }
}
