using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    //Values
    public Item item;
    public int price;

    //UI
    public Image icon;
    public Text priceNumber;
    public GameObject pricePanel;
    public GameObject panel;

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
    

    //Basic Functions
    void Start(){
    	if(item != null){
	    	icon.sprite = item.icon;
	    	icon.enabled = true;
	    	pricePanel.SetActive(true);
	    	priceNumber.text = price.ToString();
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
    }

    //Methods
    public void OpenPanel(){
    	if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemTradePanel>().OpenPanel(item,price,false,true);
        }
    }
}
