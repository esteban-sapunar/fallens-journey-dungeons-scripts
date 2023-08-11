using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
	//Values
	Card card;
	public GameObject player;
	public GameObject panel;
	public GameObject cardPanel;
    public GameObject innerPanel;
    private TouchController touchController;
    private Camera cameraMain;
    public bool touching = false;
    private Vector3 initialPos;
    public bool cardPanelActive = false;
    public GameObject discardDesk;

	//UI
	public Text name;
	public Text type;
	public Image icon;
	public Text stamina;
	public Image actionIcon;
	public Text actionValue;
	public Sprite attack;
	public Sprite defense;
	public Sprite healing;

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
    	touchController = TouchController.instance;
    	cameraMain = Camera.main;
        ColorUtility.TryParseHtmlString("#BC4715",out attackColor);
        ColorUtility.TryParseHtmlString("#0A90BC",out defenseColor);
        ColorUtility.TryParseHtmlString("#5BCD1B",out healingColor);
        ColorUtility.TryParseHtmlString("#CACACA",out passiveColor);
        ColorUtility.TryParseHtmlString("#593B19",out tier0Color);
        ColorUtility.TryParseHtmlString("#FF6623",out tier1Color);
        ColorUtility.TryParseHtmlString("#CCD9D9",out tier2Color);
        ColorUtility.TryParseHtmlString("#FFE84A",out tier3Color);
        ColorUtility.TryParseHtmlString("#00FFF3",out tier4Color);
        initialPos = transform.position;
    }

    private void OnEnable(){
    	touchController.OnStartTouch += StartMove;
    	touchController.OnDuringTouch += Move;
    	touchController.OnEndTouch += EndMove;
    }

    private void OnDisable(){
    }

    public void StartMove(Vector2 screenPosition){
    	initialPos = transform.position;
    	Vector3 screenCoordinates = new Vector3(screenPosition.x,screenPosition.y,0);
    	Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
    	worldCoordinates.z = 0;
    	if(Mathf.Abs(initialPos.x - worldCoordinates.x) <= 1f && Mathf.Abs(initialPos.y - worldCoordinates.y) <= 1.6f && gameObject.active && !cardPanelActive && !touching){
    		OpenCardPanel();
    		cardPanelActive = true;
    		touching = true;
    	}
    }

    public void Move(Vector2 screenPosition){
    	Vector3 screenCoordinates = new Vector3(screenPosition.x,screenPosition.y,0);
    	Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
    	worldCoordinates.z = 0;
    	if(touching){
	    	if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 0.5f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 0.8f){
	    		if(cardPanelActive){
	    			ClosePanel();
	    		}	    		
	    		transform.position = worldCoordinates;  		
	    	}
    	}
    }

    public void EndMove(Vector2 screenPosition){
    	Vector3 screenCoordinates = new Vector3(screenPosition.x,screenPosition.y,0);
    	Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
    	worldCoordinates.z = 0;
    	if(touching){
	    	if(Mathf.Abs(discardDesk.transform.position.x - worldCoordinates.x) <= 1f && Mathf.Abs(discardDesk.transform.position.y - worldCoordinates.y) <= 1.6f){
	    		transform.position = initialPos;
	    		panel.GetComponent<CardPanelController>().DiscardCard();
	    	}
	    	else{
	    		switch((int)card.cardType){
		            case 0:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 1:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 2:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 3:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 4:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 5:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 6:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 7:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 8:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}               
		            break;
		            case 9:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}              
		            break;
		            case 10:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 11:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 12:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 13:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 14:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 15:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 16:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 17:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 18:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		            case 19:
				    		if(Mathf.Abs(initialPos.x - worldCoordinates.x) >= 1f || Mathf.Abs(initialPos.y - worldCoordinates.y) >= 1.6f){
					    		transform.position = initialPos;
					    		panel.GetComponent<CardPanelController>().UseCard(worldCoordinates);
					    	}
		            break;
		        }
	    	}
	    	if(cardPanelActive){
	    		ClosePanel();
	    	} 	
	    	touching = false;
    	}
    	transform.localPosition = new Vector3(0,0,1);
    }

	//Methods

	public void SetCard(Card newCard){
		card = newCard;
		if(card != null){
			cardPanel.SetActive(true);
	        switch ((int)newCard.cardType){
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
	        switch (newCard.tier){
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
			name.text = card.name;
			icon.sprite = card.icon;
			stamina.text = card.staminaCost.ToString();

			if((int)card.cardSubType != 0){
	            type.text = card.cardSubType.ToString();
	        }
	        else {
	            type.text = "";
	        }

			switch ((int)card.cardType){
				case 0:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 1:
					actionIcon.sprite = defense;
					actionValue.text = (card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))).ToString();
					break;
				case 2:
					actionIcon.sprite = healing;
					actionValue.text = (card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality)).ToString();
					break;
				case 3:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 4:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 5:
					actionIcon.sprite = defense;
					actionValue.text =  (card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))).ToString();
					break;
				case 6:
					actionIcon.sprite = attack;
					actionValue.text = ((int)Mathf.Round(card.percent*player.GetComponent<PlayerStats>().blockAmount/100f)).ToString();
					break;
				case 7:
					actionIcon.sprite = attack;
					actionValue.text = card.strength.ToString();
					break;
				case 8:
					actionIcon.sprite = defense;
					actionValue.text = (player.GetComponent<PlayerStats>().GetStatValue("defense")+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/3f)).ToString();
					break;
				case 9:
					actionIcon.sprite = healing;
					actionValue.text = (card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality)).ToString();
					break;
				case 10:
					actionIcon.sprite = attack;
					actionValue.text = (card.damage+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/1f)).ToString();
					break;
				case 11:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 12:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 13:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 14:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 15:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 16:
					actionIcon.sprite = attack;
					actionValue.text = card.strength.ToString();
					break;
				case 17:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 18:
					actionIcon.sprite = attack;
					actionValue.text = card.evasion.ToString();
					break;
				case 19:
					actionIcon.sprite = attack;
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
			}
		}
	}

	public void OpenCardPanel(){
		if(card != null){
            panel.SetActive(true);
            panel.GetComponent<CardPanelController>().OpenPanel(card,gameObject);
        }
	}
	public void ClosePanel(){
        panel.GetComponent<CardPanelController>().ClosePanel();
        cardPanelActive = false;
    }
	public Card GetCard(){
		return card;
	}
	public void UnsetCard(){
		if(card != null){
			card = null;
			name.text = "";
			icon.sprite = null;
			stamina.text = "";
			actionIcon.sprite = null;
			actionValue.text = "";
			gameObject.SetActive(false);
		}
	}
	public void ResetPos(){
		transform.position = initialPos;
	}
	public void UpdateCard(){
			switch ((int)card.cardType){
				case 0:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 1:
					actionValue.text = (card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))).ToString();
					break;
				case 2:
					actionValue.text = (card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality)).ToString();
					break;
				case 3:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 4:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 5:
					actionValue.text = (card.defense+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))).ToString();
					break;
				case 6:
					actionValue.text = ((int)Mathf.Round(card.percent*player.GetComponent<PlayerStats>().blockAmount/100f)).ToString();
					break;
				case 7:
					actionValue.text = card.strength.ToString();
					break;
				case 8:
					actionValue.text = (player.GetComponent<PlayerStats>().GetStatValue("defense")+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/3f)).ToString();
					break;
				case 9:
					actionValue.text = (card.healing+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().vitality)).ToString();
					break;
				case 10:
					actionValue.text = (card.damage+(int)Mathf.Round((card.percent/100f)*player.GetComponent<PlayerStats>().GetStatValue("defense"))+(int)Mathf.Round((player.GetComponent<PlayerStats>().dexterity+player.GetComponent<PlayerStats>().extraDexterity)/3f)).ToString();
					break;
				case 11:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 12:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 13:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 14:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 15:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 16:
					actionValue.text = card.strength.ToString();
					break;
				case 17:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
				case 18:
					actionValue.text = card.evasion.ToString();
					break;
				case 19:
					actionValue.text = ((card.damage+(int)Mathf.Round((card.percent/100f)*WeaponBonus()*player.GetComponent<PlayerStats>().GetStatValue("attack")))*player.GetComponent<PlayerStats>().oneTimeDamageMulti).ToString();
					break;
			}
	}

	private float WeaponBonus() {
        if(card.hasWeaponsBuffs == true){
            int weaponType = player.GetComponent<EquipmentManager>().WeaponType();
            foreach(WeaponType type in card.weaponsBuff){
                if((int)type == weaponType){
                    return 1f + card.weaponsBuffPercent/100f;
                }
            }
            foreach(WeaponType type in card.weaponsDebuff){
                if((int)type == weaponType){
                    return 1f + card.weaponsDebuffPercent/100f;
                }
            }
            return 1f;
        }
        else {
            return 1f;
        }
    }
}
