using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TownDeskSlot : MonoBehaviour
{
    //Values
    Card card;
    public Image icon;
    public GameObject panel;
    public GameObject cardPanel;
    public GameObject innerPanel;

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
        
    	icon.sprite = card.icon;
    	icon.enabled = true;
    }
    public void ClearCard(){
    	card = null;
    	icon.sprite = null;
    	icon.enabled = false;
        cardPanel.SetActive(false);
    }
    public virtual void OnRemoveButton(){
    	TownDesk.instance.Remove(card);
    }
    public void OpenCard(){
    	if(card != null){
            panel.SetActive(true);
            panel.GetComponent<TownCardPanelController>().OpenPanel(card,gameObject,true,false);
        }
    }
}
