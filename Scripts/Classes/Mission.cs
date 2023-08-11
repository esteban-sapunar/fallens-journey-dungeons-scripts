using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    //Values
    public int id;
	public string title;
	public Sprite image;
	public MissionType type;
	public string name;
	public NPC npc;
	public string description;

	//Objective Values
	public int objectiveNumber;
	public string objectiveStage;
	public Item objectiveItem;
	public Enemy objectiveEnemy;

	//Reward Values
	public int rewardExp;
	public Item rewardItem;
	public int rewardItemNumber;
	public int rewardGold;
	public int rewardCardPoints;

	//Contructor
	public Mission(
		int newId,
		MissionType newType, 
		string newTitle, 
		NPC newNPC,
		string newDescription = "",
		int newObjectiveNumber = 0,
		string newObjectiveStage = "",
		Item newObjectiveItem = null, 
		Enemy newObjectiveEnemy = null,
		int newRewardExp = 0,
		Item newRewardItem = null,
		int newRewardItemNumber = 0,
		int newRewardGold = 0,
		int newRewardCardPoints = 0
	){
		this.id = newId;
		this.type = newType;
		this.title = newTitle;
		this.npc = newNPC;
		this.image = newNPC.image;
		this.name = newNPC.name;
		this.description = newDescription;
		
		this.objectiveNumber = newObjectiveNumber;
		this.objectiveStage = newObjectiveStage;
		this.objectiveItem = newObjectiveItem;
		this.objectiveEnemy = newObjectiveEnemy;

		this.rewardExp = newRewardExp;
		this.rewardItem = newRewardItem;
		this.rewardItemNumber = newRewardItemNumber;
		this.rewardGold = newRewardGold;
		this.rewardCardPoints = newRewardCardPoints;
	}

    //Methods

}

public enum MissionType {Item,Stage,Level,Hunt}
