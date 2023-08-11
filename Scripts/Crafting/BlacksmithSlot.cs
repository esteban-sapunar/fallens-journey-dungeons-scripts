using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithSlot : MonoBehaviour
{
    //Values
    public GameObject resultItem;
    public GameObject player;
    public int price;
    public int exp;
    public int lvl;
    public List <GameObject> materials;
    public Text goldText;
    public Text expText;

    //Items
    Inventory inventory;
    Vault vault;

     //Colors
    Color activeColor;
    Color blockedColor;

    void Awake(){
        ColorUtility.TryParseHtmlString("#709A76",out activeColor);
        ColorUtility.TryParseHtmlString("#9A7471",out blockedColor);
    }

    
    //Basic Functions
    public void Init(){
    	foreach(GameObject material in materials){
    		material.GetComponent<IngredientSlot>().Init();
    	}
    	inventory = Inventory.instance;
    	vault = Vault.instance;
    	goldText.text = price.ToString();
        expText.text = "+"+exp;
        if(player.GetComponent<TownPlayerStats>().craftingLevel >= lvl){
            gameObject.GetComponent<Image>().color = activeColor;
        }
        else{
            gameObject.GetComponent<Image>().color = blockedColor;
        }
    }

    public void UpdateSlot(){
        if(player.GetComponent<TownPlayerStats>().craftingLevel >= lvl){
            gameObject.GetComponent<Image>().color = activeColor;
        }
        else{
            gameObject.GetComponent<Image>().color = blockedColor;
        }
    }

    //Methods
    public void Craft(){
    	if(materials.TrueForAll(material => material.GetComponent<IngredientSlot>().ready) && player.GetComponent<TownPlayerStats>().gold >= price && player.GetComponent<TownPlayerStats>().craftingLevel >= lvl){
    		SoundManager.instance.PlayEffect("Anvil",false,false);
            foreach(GameObject material in materials){
    			for(int i = 0;i < material.GetComponent<IngredientSlot>().number;i++){
    				if(vault.Count(material.GetComponent<IngredientSlot>().item) > 0){
	    				vault.Remove(material.GetComponent<IngredientSlot>().item);
	    			}
	    			else{
	    				inventory.Remove(material.GetComponent<IngredientSlot>().item);
	    			}
    			}
    		}
    		player.GetComponent<TownPlayerStats>().PayGold(price);
            player.GetComponent<TownPlayerStats>().CraftingExpUp(exp);
            for(int i = 0;i < resultItem.GetComponent<ResultSlot>().number;i++){
        		inventory.Add(resultItem.GetComponent<ResultSlot>().item);
            }
    	}
    }
}
