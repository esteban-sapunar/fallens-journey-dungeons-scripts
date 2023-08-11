using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStatus
{
	//Values
	public EnemyStatusTypes type;
	public int value;
	public int duration;

	//Methods
    public EnemyStatus(EnemyStatusTypes newType,int newValue,int newDuration){
    	this.type = newType;
    	this.value = newValue;
    	this.duration = newDuration;
    }
}
public enum EnemyStatusTypes {stunned}
