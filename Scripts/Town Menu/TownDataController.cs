using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDataController : MonoBehaviour
{
  //Values
  public GameData data;
  Dictionary <string, int> itemsData = new Dictionary<string, int>();

  //Objects
  public GameObject player;
  public GameObject equipUI;
  public GameObject inventoryUI;
  public GameObject vaultUI;
  public GameObject deskUI;
  public GameObject cardsVaultUI;
  public GameObject cardsVaultTrainerUI;
  public GameObject inventoryShopUI;
  public GameObject vaultShopUI;
  public GameObject playerPanel;
  public GameObject cardsPanel;
  public GameObject trainerPanel;
  public GameObject shopPanel;
  public GameObject blacksmithPanel;
  public GameObject innPanel;
  public GameObject inventoryBlacksmithUI;
  public GameObject vaultBlacksmithUI;
  public Transform cardsvaultParent;
  public Transform blacksmithParent;

  //Library
  public ItemsDatabase itemsDatabase;
  private List <Item> allItems;
  public CardsDatabase cardsDatabase;
  private List <Card> allCards;

  //UI

    //Basic Functions
    void Awake(){
      allItems = itemsDatabase.items;
      allCards = cardsDatabase.cards;
      //Load
      DataPersistenceManager.instance.LoadGame();
      data = DataPersistenceManager.instance.GetData();
      //Player
      player.GetComponent<TownPlayerStats>().maxHealth = 50 +(data.vitality*5);
      player.GetComponent<TownPlayerStats>().baseStamina = 25 +(data.resistance*2);
      player.GetComponent<TownPlayerStats>().exp = data.exp;
      player.GetComponent<TownPlayerStats>().gold = data.gold;
      player.GetComponent<TownPlayerStats>().cardPoints = data.cards_points;
      player.GetComponent<TownPlayerStats>().level = data.level;
      player.GetComponent<TownPlayerStats>().craftingLevel = data.crafting_level;
      player.GetComponent<TownPlayerStats>().craftingExp = data.crafting_exp;
      player.GetComponent<TownPlayerStats>().points = data.skill_points;
      player.GetComponent<TownPlayerStats>().strength = data.strength;
      player.GetComponent<TownPlayerStats>().agility = data.agility;
      player.GetComponent<TownPlayerStats>().dexterity = data.dexterity;
      player.GetComponent<TownPlayerStats>().resistance = data.resistance;
      player.GetComponent<TownPlayerStats>().vitality = data.vitality;
      player.GetComponent<TownPlayerStats>().InitData();
      //Stages
        string StagesString = data.stages;
        string[] arrayStages =  StagesString.Split(char.Parse("|"));
        player.GetComponent<TownPlayerStats>().arrayStages = arrayStages;
      //Missions
        string MissionsString = data.missions;
        string[] arrayMissions =  MissionsString.Split(char.Parse("|"));
        player.GetComponent<TownPlayerStats>().arrayMissions = arrayMissions;

      //Equip
      player.GetComponent<EquipmentManager>().Init();
      equipUI.GetComponent<EquipmentUI>().Init();
      foreach (int slot in data.equip.GetData()){
        if(slot != 0){
          player.GetComponent<EquipmentManager>().Equip((Equipment)allItems[slot-1]);
        }
      }
      inventoryUI.GetComponent<InventoryUI>().Init();
      vaultUI.GetComponent<VaultUI>().Init();
      deskUI.GetComponent<TownDeskUI>().Init();
      cardsVaultUI.GetComponent<CardsVaultUI>().Init();
      cardsVaultTrainerUI.GetComponent<CardsVaultUI>().Init();
      inventoryShopUI.GetComponent<InventoryUI>().Init();
      vaultShopUI.GetComponent<VaultUI>().Init();
      inventoryBlacksmithUI.GetComponent<InventoryUI>().Init();
      vaultBlacksmithUI.GetComponent<VaultUI>().Init();
      //Blacksmith
        BlacksmithSlot[] blacksmithslots = blacksmithParent.GetComponentsInChildren<BlacksmithSlot>();
        foreach(BlacksmithSlot slot in blacksmithslots){
          slot.Init();
        }

      //Inventory
      foreach (int slot in data.inventory.slots){
        if(slot != 0){
          gameObject.GetComponent<Inventory>().Add(allItems[slot-1]);
        }
      }
      //Load Rewards
        string RewardItemsString = data.reward_items;
        string[] RewardItemsArray =  RewardItemsString.Split(char.Parse("|"));
        for(int i=0; i<RewardItemsArray.Length;i++){
          if(int.Parse(RewardItemsArray[i]) > 0){
            gameObject.GetComponent<Inventory>().Add(allItems[int.Parse(RewardItemsArray[i])-1]);
          }
        }
        data.reward_items = "0";
      //Vault
      foreach (int slot in data.vault.slots){
        if(slot != 0){
          gameObject.GetComponent<Vault>().Add(allItems[slot-1]);
        }
      }
      //Desk
      foreach (int slot in data.desk.slots){
        if(slot != 0){
          gameObject.GetComponent<TownDesk>().Add(allCards[slot-1]);
        }
      }

      //CardsVault
        string CardsVaultString = data.cards_vault;
        string[] arrayCardsTypes =  CardsVaultString.Split(char.Parse("|"));
        for(int i=0; i<arrayCardsTypes.Length;i++){
          string[] auxArray = arrayCardsTypes[i].Split(char.Parse(","));
          int type = int.Parse(auxArray[0]);
          int number = int.Parse(auxArray[1]);
          for(int j = 0; j<number; j++){
            gameObject.GetComponent<CardsVault>().Add(allCards[type-1]);
          }
        }
      TownPlayerStats.instance.SetBattlePower();
      playerPanel.SetActive(false);
      cardsPanel.SetActive(false);
      trainerPanel.SetActive(false);
      shopPanel.SetActive(false);
      blacksmithPanel.SetActive(false);
      innPanel.SetActive(false);

    }

    public void SaveData(){
      data.level = player.GetComponent<TownPlayerStats>().level;
      data.exp = player.GetComponent<TownPlayerStats>().exp;
      data.crafting_level = player.GetComponent<TownPlayerStats>().craftingLevel;
      data.crafting_exp = player.GetComponent<TownPlayerStats>().craftingExp;
      data.gold = player.GetComponent<TownPlayerStats>().gold;
      data.cards_points = player.GetComponent<TownPlayerStats>().cardPoints;
      data.strength = player.GetComponent<TownPlayerStats>().strength;
      data.agility = player.GetComponent<TownPlayerStats>().agility;
      data.dexterity = player.GetComponent<TownPlayerStats>().dexterity;
      data.resistance = player.GetComponent<TownPlayerStats>().resistance;
      data.vitality = player.GetComponent<TownPlayerStats>().vitality;
      data.skill_points = player.GetComponent<TownPlayerStats>().points;

      //Equip
      data.equip.head = allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[0])+1;
      data.equip.chest = allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[1])+1;
      data.equip.legs = allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[2])+1;
      data.equip.feet = allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[3])+1;
      data.equip.weapon = allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[4])+1;
      data.equip.shield = allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[5])+1;
      data.equip.bag = allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[6])+1;

      data.inventory.slots = new List<int>();
      for(int i = 0;i < gameObject.GetComponent<Inventory>().items.Count;i++){
        if(gameObject.GetComponent<Inventory>().items[i]){
           data.inventory.slots.Add(allItems.IndexOf(gameObject.GetComponent<Inventory>().items[i])+1);
        }
      }

      data.vault.slots = new List<int>();
      for(int i = 0;i < gameObject.GetComponent<Vault>().items.Count;i++){
        if(gameObject.GetComponent<Vault>().items[i]){
          data.vault.slots.Add(allItems.IndexOf(gameObject.GetComponent<Vault>().items[i])+1);
        }
      }

      data.desk.slots = new List<int>();
      for(int i=1;i<=30;i++){
        data.desk.slots.Add(allCards.IndexOf(gameObject.GetComponent<TownDesk>().desk[i-1])+1);
      }
      //Cards Vault
      CardVaultSlot[] slots;
      slots = cardsvaultParent.GetComponentsInChildren<CardVaultSlot>();
      string cardsVaultInfo ="";
      for(int i=0;i< slots.Length;i++){
        if(slots[i].cards.Count > 0){
          string auxString = (allCards.IndexOf(slots[i].cards[0])+1).ToString()+","+slots[i].cards.Count.ToString();
          cardsVaultInfo += auxString+"|";
        }
      }
      if(cardsVaultInfo.Length > 0){
        cardsVaultInfo = cardsVaultInfo.Substring(0,cardsVaultInfo.Length-1);
        data.cards_vault = cardsVaultInfo;
      }
      //Missions
      string MissionsString = "";
      foreach(string mission in player.GetComponent<TownPlayerStats>().arrayMissions){
        MissionsString += mission+"|";      
      }
      string FinishMissionsString = MissionsString.Substring(0,MissionsString.Length-1);
      data.missions = FinishMissionsString;

      DataPersistenceManager.instance.SetData(data);
      DataPersistenceManager.instance.SaveGame();
    }
}
