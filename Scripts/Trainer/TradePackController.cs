using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradePackController : MonoBehaviour
{
	//Values
	Pack pack;
	public GameObject GM;
    public GameObject player;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text priceNumber;
    public Text pointsNumber;
    public GameObject innerPanel;
    public GameObject packResultPanel;
    public GameObject packCardPanel1;
    public GameObject packCardPanel2;
    public GameObject packCardPanel3;
    public GameObject packCardPanel4;
    public GameObject packCardPanel5;

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

    //Methods
    public void ClosePanel(){
        gameObject.SetActive(false);
    }

    public void OpenPanel(Pack newPack){
        pack = newPack;
        name.text = pack.name;
        icon.sprite = pack.icon;
        description.text = pack.description;
        priceNumber.text = pack.price.ToString();
        pointsNumber.text = pack.points.ToString();

        
        //Colors

        switch (pack.tier){
            case 0:
                innerPanel.GetComponent<Image>().color = tier0Color;
            break;
            case 1:
                innerPanel.GetComponent<Image>().color = tier1Color;
            break;
            case 2:
                innerPanel.GetComponent<Image>().color = tier2Color;
            break;
            case 3:
                innerPanel.GetComponent<Image>().color = tier3Color;
            break;
            case 4:
                innerPanel.GetComponent<Image>().color = tier4Color;
            break;
        }

    }

    public void BuyPack(){
    	if(player.GetComponent<TownPlayerStats>().gold >= pack.price && player.GetComponent<TownPlayerStats>().cardPoints >= pack.points){
    		player.GetComponent<TownPlayerStats>().PayGold(pack.price);
    		player.GetComponent<TownPlayerStats>().PayCardPoints(pack.points);

    		Card randCard1 = pack.slot1[Random.Range(0,pack.slot1.Count)];
    		CardsVault.instance.Add(randCard1);

    		Card randCard2 = pack.slot2[Random.Range(0,pack.slot2.Count)];
    		CardsVault.instance.Add(randCard2);

    		Card randCard3 = pack.slot3[Random.Range(0,pack.slot3.Count)];
    		CardsVault.instance.Add(randCard3);

    		Card randCard4 = pack.slot4[Random.Range(0,pack.slot4.Count)];
    		CardsVault.instance.Add(randCard4);

    		Card randCard5 = pack.slot5[Random.Range(0,pack.slot5.Count)];
    		CardsVault.instance.Add(randCard5);

            packResultPanel.SetActive(true);
            packCardPanel1.GetComponent<PackCardController>().AddCard(randCard1);
            packCardPanel2.GetComponent<PackCardController>().AddCard(randCard2);
            packCardPanel3.GetComponent<PackCardController>().AddCard(randCard3);
            packCardPanel4.GetComponent<PackCardController>().AddCard(randCard4);
            packCardPanel5.GetComponent<PackCardController>().AddCard(randCard5);

        	ClosePanel();
    	}        
    }
}
