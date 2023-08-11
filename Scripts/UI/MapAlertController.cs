using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAlertController : MonoBehaviour
{
    //Values
    public Text alertText;

    //Colors
    Color itemColor;
    Color goldColor;
    Color cardPointsColor;
    Color expColor;

    void Awake(){
        ColorUtility.TryParseHtmlString("#FFFFFF",out itemColor);
        ColorUtility.TryParseHtmlString("#F1DE3C",out goldColor);
        ColorUtility.TryParseHtmlString("#CB8C1C",out cardPointsColor);
        ColorUtility.TryParseHtmlString("#FF8BDE",out expColor);
    }

    //Methods
    public void SendAlert(string newText, string type) {
    	alertText.text = newText;
    	switch (type){
            case "item":
                alertText.color = itemColor;
            break;
            case "gold":
                alertText.color = goldColor;
            break;
            case "card_points":
                alertText.color = cardPointsColor;
            break;
            case "exp":
                alertText.color = expColor;
            break;
        }
        StartCoroutine(SetAlert());
    }

    IEnumerator SetAlert(){ 
		yield return new WaitForSeconds(0.35f);
		gameObject.SetActive(false);
    }
}    
