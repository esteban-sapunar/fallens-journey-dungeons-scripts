using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDesk : MonoBehaviour
{
    //Delegate
	public delegate void OnCardChanged();
	public OnCardChanged OnCardChangedCallback;

	//Values
	public int space = 30;
	public int deskPower;
	public List<Card> desk = new List<Card>();
	
	public static TownDesk instance;
	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of TownDesk found!");
			return;
		}
		instance = this;
	}


	//Methods
	public bool Add (Card newCard){
		if(desk.Count < space){
			desk.Add(newCard);
			if(OnCardChangedCallback != null){
				OnCardChangedCallback.Invoke();
			}
			SetPower();
			TownPlayerStats.instance.SetBattlePower();
			return true;
		}
		else{
			Debug.Log("Not Enough Space");
			return false;
		}
	}

	public void Remove (Card card){
		desk.Remove(card);
		if(OnCardChangedCallback != null){
			OnCardChangedCallback.Invoke();	
		}
		SetPower();
		TownPlayerStats.instance.SetBattlePower();
	}

	public void RemoveAll(){
		List<Card> currentDesk = new List<Card>(desk);
		foreach (Card card in currentDesk){
			CardsVault.instance.Add(card);
			desk.Remove(card);
			if(OnCardChangedCallback != null){
				OnCardChangedCallback.Invoke();	
			}
		}
		SetPower();
		TownPlayerStats.instance.SetBattlePower();
	}

	public void RemoveType(Card refCard){
		List<Card> currentDesk = new List<Card>(desk);
		foreach (Card card in currentDesk){
			if(refCard == card){
				CardsVault.instance.Add(card);
				desk.Remove(card);
				if(OnCardChangedCallback != null){
					OnCardChangedCallback.Invoke();	
				}
			}
		}
		SetPower();
		TownPlayerStats.instance.SetBattlePower();
	}

	public void AddType(Card newCard){
		while(desk.Count < space && CardsVault.instance.ContainCard(newCard)){
			desk.Add(newCard);
			CardsVault.instance.Remove(newCard);
			if(OnCardChangedCallback != null){
				OnCardChangedCallback.Invoke();
			}
		}
		SetPower();
		TownPlayerStats.instance.SetBattlePower();
	}

	void SetPower(){
		deskPower = 0;
		foreach (Card card in desk){
			switch (card.tier){
	            case 0:
	                deskPower +=1;
	            break;
	            case 1:
	                deskPower +=5;
	            break;
	            case 2:
	                deskPower +=25;
	            break;
	            case 3:
	                deskPower +=125;
	            break;
	            case 4:
	                deskPower +=600;
	            break;
	        }
		}
	}
}
