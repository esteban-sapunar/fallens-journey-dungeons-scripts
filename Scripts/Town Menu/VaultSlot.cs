using UnityEngine;
using UnityEngine.UI;

public class VaultSlot : MonoBehaviour
{
    //Values
    Item item;
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
    public void AddItem(Item newItem){
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
    public void ClearItem(){
    	item = null;
    	icon.sprite = null;
    	icon.enabled = false;
        gameObject.GetComponent<Image>().color = emptyColor;
    }
    public virtual void OnVaultRemoveButton(){
        Vault.instance.Remove(item);
    }
    public void OpenItem(){
    	if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemPanelController>().OpenPanel(item,false,true);
        }
    }
    public void OpenTradeItem(){
        if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemTradePanel>().OpenPanel(item,item.value,true,false);
        }
    }
}
