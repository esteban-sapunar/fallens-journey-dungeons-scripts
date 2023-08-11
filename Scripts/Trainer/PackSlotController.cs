using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackSlotController : MonoBehaviour
{
    //Values
    public Pack pack;

    //UI
    public Image icon;
    public GameObject packPanel;
    public GameObject innerPack;
    public Text priceNumber;
    public GameObject pricePanel;
    public Text pointsNumber;
    public GameObject pointsPanel;
    public GameObject panel;


    //Colors
    Color tier0Color;
    Color tier1Color;
    Color tier2Color;
    Color tier3Color;
    Color tier4Color;

    void Awake(){
        ColorUtility.TryParseHtmlString("#593B19",out tier0Color);
        ColorUtility.TryParseHtmlString("#FF6623",out tier1Color);
        ColorUtility.TryParseHtmlString("#CCD9D9",out tier2Color);
        ColorUtility.TryParseHtmlString("#FFE84A",out tier3Color);
        ColorUtility.TryParseHtmlString("#00FFF3",out tier4Color);
    }

    //Basic Functions
    void Start(){
    	if(pack != null){
            packPanel.SetActive(true);
            switch (pack.tier){
                case 0:
                    packPanel.GetComponent<Image>().color = tier0Color;
                break;
                case 1:
                    packPanel.GetComponent<Image>().color = tier1Color;
                break;
                case 2:
                    packPanel.GetComponent<Image>().color = tier2Color;
                break;
                case 3:
                    packPanel.GetComponent<Image>().color = tier3Color;
                break;
                case 4:
                    packPanel.GetComponent<Image>().color = tier4Color;
                break;
            }
	    	icon.sprite = pack.icon;
	    	icon.enabled = true;
	    	pricePanel.SetActive(true);
	    	priceNumber.text = pack.price.ToString();
	    	pointsPanel.SetActive(true);
	    	pointsNumber.text = pack.points.ToString();
    	}
    }

    //Methods
    public void OpenPanel(){
    	if(pack != null){
            panel.SetActive(true);
            panel.GetComponent<TradePackController>().OpenPanel(pack);
        }
    }
    
}
