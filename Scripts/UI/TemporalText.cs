using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporalText : MonoBehaviour
{
	//Colors
	Color Green;
    Color Red;

    void Awake() {
        ColorUtility.TryParseHtmlString("#41CB3E",out Green);
        ColorUtility.TryParseHtmlString("#FF2632",out Red);
    }

    public void SetText(string text, string color){
    	gameObject.SetActive(true);
    	switch (color){
            case "green":
            	gameObject.GetComponent<Text>().color = Green;
            break;
            case "red":
            	gameObject.GetComponent<Text>().color = Red;
            break;
        }
    	StartCoroutine(SetOffText(text));
    }
    IEnumerator SetOffText(string text){
    	char[] textArray = text.ToCharArray();
    	foreach(char letter in textArray){
    		gameObject.GetComponent<Text>().text += letter;
    		yield return new WaitForSeconds(0.03f);
    	}
    	yield return new WaitForSeconds(0.5f);
    	gameObject.GetComponent<Text>().text = "";
    	gameObject.SetActive(false);
    }
}
