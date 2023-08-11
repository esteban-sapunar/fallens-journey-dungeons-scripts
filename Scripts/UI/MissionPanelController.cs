using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class MissionPanelController : MonoBehaviour
{
    //Values
    public Mission mission;
    public GameObject slot;
    public Text title;
    public Image npcImage;
    public Text npcName;
    public Text description;
    public GameObject objectiveItemSlot;
    public Image objectiveItemImage;
    public GameObject expSlot;
    public Text exp;
    public GameObject goldSlot;
    public Text gold;
    public GameObject pointsSlot;
    public Text points;
    public GameObject itemSlot;
    public Text itemNumber;
    public Image itemImage;

    //Method
    public void SetPanel(Mission newMission,GameObject newSlot){
        SoundManager.instance.PlayEffect("Talk",false,false);
    	mission = newMission;
    	slot = newSlot;
    	title.text = mission.title;
    	npcImage.sprite = mission.image;
    	npcName.text = mission.name;
    	description.text = mission.description;
        expSlot.SetActive(false);
        goldSlot.SetActive(false);
        pointsSlot.SetActive(false);
        itemSlot.SetActive(false);
        objectiveItemSlot.SetActive(false);
    	if(mission.rewardExp > 0){
    		expSlot.SetActive(true);
    		exp.text = mission.rewardExp.ToString();
    	}
    	if(mission.rewardGold > 0){
    		goldSlot.SetActive(true);
    		gold.text = mission.rewardGold.ToString();
    	}
        if(mission.rewardCardPoints > 0){
            pointsSlot.SetActive(true);
            points.text = mission.rewardCardPoints.ToString();
        }
    	if(mission.rewardItem != null){
    		itemSlot.SetActive(true);
    		itemNumber.text = mission.rewardItemNumber.ToString();
    		itemImage.sprite = mission.rewardItem.icon;
    	}
        if((int)mission.type == 0){
            objectiveItemSlot.SetActive(true);
            objectiveItemImage.sprite = mission.objectiveItem.icon;
        }
    }

    public void ResetPanel(){
    	expSlot.SetActive(false);
    	goldSlot.SetActive(false);
        pointsSlot.SetActive(false);
    	itemSlot.SetActive(false);
        objectiveItemSlot.SetActive(false);
    	gameObject.SetActive(false);
    }

    public void FinishMission(){
    	switch ((int)mission.type){
    		case 0:
				if(Inventory.instance.Count(mission.objectiveItem) >= mission.objectiveNumber){
					for (int i = 1; i <= mission.objectiveNumber; i++) {
		    			Inventory.instance.Remove(mission.objectiveItem);
		    		}					
					if(mission.rewardExp > 0){
			    		TownPlayerStats.instance.ExpUp(mission.rewardExp);
			    	}
			    	if(mission.rewardGold > 0){
			    		TownPlayerStats.instance.EarnGold(mission.rewardGold);
			    	}
                    if(mission.rewardCardPoints > 0){
                        TownPlayerStats.instance.EarnCardPoints(mission.rewardCardPoints);
                    }
			    	if(mission.rewardItem != null){
			    		for (int i = 1; i <= mission.rewardItemNumber; i++) {
			    			Inventory.instance.Add(mission.rewardItem);
			    		}
			    	}
                    List<string> listMissions = new List <string>();
                    listMissions.AddRange(TownPlayerStats.instance.arrayMissions);
                    listMissions.Add(mission.id.ToString());
                    TownPlayerStats.instance.arrayMissions = listMissions.ToArray();
                    slot.SetActive(false);
                    ResetPanel();
                    InnController.instance.SetMissions();
				}
			break;
			case 1:
				if(Array.Exists(TownPlayerStats.instance.arrayStages,s => s == mission.objectiveStage)){
					if(mission.rewardExp > 0){
			    		TownPlayerStats.instance.ExpUp(mission.rewardExp);
			    	}
			    	if(mission.rewardGold > 0){
			    		TownPlayerStats.instance.EarnGold(mission.rewardGold);
			    	}
                    if(mission.rewardCardPoints > 0){
                        TownPlayerStats.instance.EarnCardPoints(mission.rewardCardPoints);
                    }
			    	if(mission.rewardItem != null){
			    		for (int i = 1; i <= mission.rewardItemNumber; i++) {
			    			Inventory.instance.Add(mission.rewardItem);
			    		}
			    	}
                    List<string> listMissions = new List <string>();
                    listMissions.AddRange(TownPlayerStats.instance.arrayMissions);
                    listMissions.Add(mission.id.ToString());
                    TownPlayerStats.instance.arrayMissions = listMissions.ToArray();
                    slot.SetActive(false);
                    ResetPanel();
                    InnController.instance.SetMissions();
				}
			break;
			case 2:
                if(TownPlayerStats.instance.level >= mission.objectiveNumber){
                    if(mission.rewardExp > 0){
                        TownPlayerStats.instance.ExpUp(mission.rewardExp);
                    }
                    if(mission.rewardGold > 0){
                        TownPlayerStats.instance.EarnGold(mission.rewardGold);
                    }
                    if(mission.rewardCardPoints > 0){
                        TownPlayerStats.instance.EarnCardPoints(mission.rewardCardPoints);
                    }
                    if(mission.rewardItem != null){
                        for (int i = 1; i <= mission.rewardItemNumber; i++) {
                            Inventory.instance.Add(mission.rewardItem);
                        }
                    }
                    List<string> listMissions = new List <string>();
                    listMissions.AddRange(TownPlayerStats.instance.arrayMissions);
                    listMissions.Add(mission.id.ToString());
                    TownPlayerStats.instance.arrayMissions = listMissions.ToArray();
                    slot.SetActive(false);
                    ResetPanel();
                    InnController.instance.SetMissions();
                }
				
			break;
			case 3:
				
			break; 
    	}
    }

}
