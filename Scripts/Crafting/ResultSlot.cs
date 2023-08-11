using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSlot : MonoBehaviour
{
    //Values
    public Item item;
    public int number;

    //UI
    public Image icon;
    public Text numberText;
    public GameObject blacksmithPanel;
    public GameObject panel;

    //Colors
    Color tier0Color;
    Color tier1Color;
    Color tier2Color;
    Color tier3Color;
    Color tier4Color;
    
    public void Awake(){
        ColorUtility.TryParseHtmlString("#74430E",out tier0Color);
        ColorUtility.TryParseHtmlString("#A6552A",out tier1Color);
        ColorUtility.TryParseHtmlString("#CBBCAF",out tier2Color);
        ColorUtility.TryParseHtmlString("#E5BF08",out tier3Color);
        ColorUtility.TryParseHtmlString("#32AEA8",out tier4Color);
	    icon.sprite = item.icon;
	    numberText.text = number.ToString();
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

	//Base Functions

    public void OpenPanel(){
    	blacksmithPanel.SetActive(true);
    	blacksmithPanel.GetComponent<BlacksmithPanelController>().OpenPanel(item);
    }
}
