using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainerPanelsController : MonoBehaviour
{
	//Tabs
	public GameObject tier1Tab;
	public GameObject tier2Tab;
	public GameObject packsTab;

	//Buttons
	public GameObject buttonTier1;
	public GameObject buttonTier2;
	public GameObject buttonPacks;

	//Colors
    Color normalColor;
    Color selectedColor;

    void Awake(){
        ColorUtility.TryParseHtmlString("#9A6040",out normalColor);
        ColorUtility.TryParseHtmlString("#986E44",out selectedColor);
    }

	//Methods
	public void OpenTier1() {
		CloseAll();
		tier1Tab.SetActive(true);
		buttonTier1.GetComponent<Image>().color = selectedColor;
	}

	public void OpenTier2() {
		CloseAll();
		tier2Tab.SetActive(true);
		buttonTier2.GetComponent<Image>().color = selectedColor;
	}

	public void OpenPacks() {
		CloseAll();
		packsTab.SetActive(true);
		buttonPacks.GetComponent<Image>().color = selectedColor;
	}

	private void CloseAll() {
		buttonTier1.GetComponent<Image>().color = normalColor;
		buttonTier2.GetComponent<Image>().color = normalColor;
		buttonPacks.GetComponent<Image>().color = normalColor;
		tier1Tab.SetActive(false);
		tier2Tab.SetActive(false);
		packsTab.SetActive(false);

	}

}
