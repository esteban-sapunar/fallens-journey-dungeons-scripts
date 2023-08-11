using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class DataController : MonoBehaviour
{
	//Values
  public GameData data;

	public List <Item> initialEquip = new List<Item>();
	public List <Item> initialItems = new List<Item>();
	public List <Item> rewardItems = new List<Item>();
  public List <Item> selectedItems = new List<Item>();
  public int stage;

	//Objects
	public GameObject player;
  public GameObject equipUI;
	public GameObject inventoryUI;
	public GameObject rewardPanel;
  public GameObject rewardsController;
	public GameObject expPanel;
	public GameObject goldPanel;
  public GameObject cardPointsPanel;
	public GameObject itemPanel;
  public GameObject itemPanel2;
  public GameObject itemPanel3;
  public GameObject itemPanel4;
  public GameObject itemPanel5;
	public GameObject lootPanel;

	//Library
	public ItemsDatabase itemsDatabase;
  private List <Item> allItems;
	public CardsDatabase cardsDatabase;
  private List <Card> allCards;

	//UI
	public Text expNumber;
	public Text goldNumber;
  public Text cardPointsNumber;
	public GameObject buttonWin;
	public GameObject buttonRetreat;
	public GameObject buttonDeath;
	public Transform lootSlotParent;
  public GameObject loadingPanel;
	LootSlot[] lootSlots;

    //Basic Functions
   	void Awake(){
      allItems = itemsDatabase.items;
      allCards = cardsDatabase.cards;
   		//Load
      DataPersistenceManager.instance.LoadGame();
      data = DataPersistenceManager.instance.GetData();
      //Player
      player.GetComponent<PlayerStats>().maxHealth = 50 +(data.vitality*5);
      player.GetComponent<PlayerStats>().baseStamina = 25 +(data.resistance*2);
      player.GetComponent<PlayerStats>().strength = data.strength;
      player.GetComponent<PlayerStats>().agility = data.agility;
      player.GetComponent<PlayerStats>().dexterity = data.dexterity;
      player.GetComponent<PlayerStats>().resistance = data.resistance;
      player.GetComponent<PlayerStats>().vitality = data.vitality;
      gameObject.GetComponent<BattleController>().earnedExp = 0;
      gameObject.GetComponent<BattleController>().earnedGold = 0;
      player.GetComponent<PlayerStats>().InitData();
   		

      string StagesString = data.stages;
      string[] arrayStages =  StagesString.Split(char.Parse("|"));
      player.GetComponent<PlayerStats>().arrayStages = arrayStages;

  		//Equip
  		player.GetComponent<EquipmentManager>().Init();
      equipUI.GetComponent<EquipmentUI>().Init();
      foreach (int slot in data.equip.GetData()){
        if(slot != 0){
          player.GetComponent<EquipmentManager>().Equip((Equipment)allItems[slot-1]);
          initialEquip.Add((Equipment)allItems[slot-1]);
        }
      }
   		inventoryUI.GetComponent<InventoryUI>().Init();
   		//Inventory
      foreach (int slot in data.inventory.slots){
        if(slot != 0){
          gameObject.GetComponent<Inventory>().Add(allItems[slot-1]);
          initialItems.Add(allItems[slot-1]);
        }
      }	
   		
   		//Cards
      foreach (int slot in data.desk.slots){
        if(slot != 0){
          gameObject.GetComponent<DeskController>().AddCard(allCards[slot-1]);
        }
      }
   	}

   	//Methods
   	public void OpenRewardPanel(string state){
   		RewardItems();
   		rewardPanel.SetActive(true);
   		switch(state){
   			case "death":
   				expPanel.SetActive(true);
   				buttonDeath.SetActive(true);
   				expNumber.text = gameObject.GetComponent<BattleController>().earnedExp.ToString();
   			break;
   			case "retreat":
   				expPanel.SetActive(true);
   				goldPanel.SetActive(true);
   				buttonRetreat.SetActive(true);
   				expNumber.text = gameObject.GetComponent<BattleController>().earnedExp.ToString();
   				goldNumber.text = gameObject.GetComponent<BattleController>().earnedGold.ToString();
   			break;
   			case "win":
   				expPanel.SetActive(true);
   				goldPanel.SetActive(true);
          cardPointsPanel.SetActive(true);
   				itemPanel.SetActive(true);
          itemPanel2.SetActive(true);
          itemPanel3.SetActive(true);
          itemPanel4.SetActive(true);
          itemPanel5.SetActive(true);
   				lootPanel.SetActive(true);
   				buttonWin.SetActive(true);
          rewardsController.GetComponent<RewardsController>().SetItemsSlots();
   				expNumber.text = gameObject.GetComponent<BattleController>().earnedExp.ToString();
   				goldNumber.text = gameObject.GetComponent<BattleController>().earnedGold.ToString();
          cardPointsNumber.text = gameObject.GetComponent<BattleController>().earnedCardPoints.ToString();
   				//items
   				lootSlots = lootSlotParent.GetComponentsInChildren<LootSlot>();
   				for(int i =0; i < rewardItems.Count;i++){
   					lootSlots[i].SetItem(rewardItems[i]);
   				}
   			break;
   		}

   	}
   	public void RewardItems(){
   		List <Item> loot = new List<Item>();
      foreach(Item item in gameObject.GetComponent<Inventory>().items){
        if(item != null){
          loot.Add(item);
        }
      }
    	Equipment[] lastEquipment = player.GetComponent<EquipmentManager>().currentEquipment;
    	foreach(Item equip in lastEquipment){
    		if(equip != null){
    			loot.Add(equip);
    		}
    	}
    	foreach(Item equip in initialEquip){
    		if(equip != null){
    			loot.Remove(equip);
    		}
    	}
    	foreach(Item item in initialItems){
    		if(item != null){
    			loot.Remove(item);
    		}
    	}
    	rewardItems = loot;
   	}

   	public void SaveData(){
    	data.exp += gameObject.GetComponent<BattleController>().earnedExp;
    	data.gold += gameObject.GetComponent<BattleController>().earnedGold;
      data.cards_points += gameObject.GetComponent<BattleController>().earnedCardPoints;
      List <Item> inventoryArray = gameObject.GetComponent<Inventory>().items;
      data.inventory.slots = new List<int>();
      for(int i = 0;i < initialItems.Count;i++){
        if(inventoryArray.IndexOf(initialItems[i]) >= 0){
          data.inventory.slots.Add(allItems.IndexOf(initialItems[i])+1);
          inventoryArray.Remove(initialItems[i]);
        }
      }
    	//Rewards
      string rewardItemstInfo ="";
      for(int i=0;i< selectedItems.Count;i++){
          string auxString = (allItems.IndexOf(selectedItems[i])+1).ToString();
          rewardItemstInfo += auxString+"|";
      }
      if(rewardItemstInfo.Length > 0){
        rewardItemstInfo = rewardItemstInfo.Substring(0,rewardItemstInfo.Length-1);
        data.reward_items = rewardItemstInfo;
      }

      //Stages
      string StagesString = data.stages;
      string[] arrayStages =  player.GetComponent<PlayerStats>().arrayStages;
      if(!Array.Exists(arrayStages,s => s == (stage+1).ToString()) || stage == 4 || stage == 8 || stage == 10){
        switch (stage){
          case 1:
            StagesString += "|"+(2).ToString()+"|"+(9).ToString();
            data.stages = StagesString;
          break;
          case 3:
            StagesString += "|"+(4).ToString()+"|"+(5).ToString();
            data.stages = StagesString;
          break;
          case 5:
            StagesString += "|"+(6).ToString()+"|"+(7).ToString();
            data.stages = StagesString;
          break;
          case 9:
            if(!Array.Exists(arrayStages,s => s == "end1")){
              StagesString += "|"+"end1";
              data.stages = StagesString;
            }            
          break;
          case 4:
            if(!Array.Exists(arrayStages,s => s == "end2")){
              StagesString += "|"+"end2";
              data.stages = StagesString;
            } 
          break;
          case 8:
            if(!Array.Exists(arrayStages,s => s == "10")){
              StagesString += "|"+(10).ToString();
              data.stages = StagesString;
            }
          break;
          case 10:
            if(!Array.Exists(arrayStages,s => s == "end3")){
              StagesString += "|"+"end3";
              data.stages = StagesString;
            } 
          break;
          default:
            StagesString += "|"+(stage+1).ToString();
            data.stages = StagesString;
          break;
        }
      }
      DataPersistenceManager.instance.SetData(data);
      DataPersistenceManager.instance.SaveGame();
    	StartCoroutine(LoadingExit());
   	}

   	public void SaveDataRetreat(){
    	data.exp += gameObject.GetComponent<BattleController>().earnedExp;
      data.gold += gameObject.GetComponent<BattleController>().earnedGold;
      List <Item> inventoryArray = gameObject.GetComponent<Inventory>().items;
      data.inventory.slots = new List<int>();
      for(int i = 0;i < initialItems.Count;i++){
        if(inventoryArray.IndexOf(initialItems[i]) >= 0){
          data.inventory.slots.Add(allItems.IndexOf(initialItems[i])+1);
          inventoryArray.Remove(initialItems[i]);
        }
      }
      DataPersistenceManager.instance.SetData(data);
      DataPersistenceManager.instance.SaveGame();
    	StartCoroutine(LoadingExit());
   	}

   	public void SaveDataDeath(){
    	data.exp += gameObject.GetComponent<BattleController>().earnedExp;
      List <Item> inventoryArray = gameObject.GetComponent<Inventory>().items;
      data.inventory.slots = new List<int>();
      for(int i = 0;i < initialItems.Count;i++){
        if(inventoryArray.IndexOf(initialItems[i]) >= 0){
          data.inventory.slots.Add(allItems.IndexOf(initialItems[i])+1);
          inventoryArray.Remove(initialItems[i]);
        }
      }
      DataPersistenceManager.instance.SetData(data);
      DataPersistenceManager.instance.SaveGame();
    	StartCoroutine(LoadingExit());
   	}
   	public void OpenRetreatPanel(){
   		OpenRewardPanel("retreat");
   	}

    IEnumerator LoadingExit(){
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<LoadingPanelController>().FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TownMenu");
    }

}
