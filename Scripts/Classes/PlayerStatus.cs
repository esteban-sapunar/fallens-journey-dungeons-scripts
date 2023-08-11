using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
	//Values
	public PlayerStatusTypes statusType;
	public int value;
	public int value2;
	public int value3;
	public int value4;
	public int duration;

	//Contructor
	public PlayerStatus(int type,int newValue,int newValue2,int newValue3,int newValue4,int newDuration){
		switch (type){
			case 0:
				this.statusType = PlayerStatusTypes.strengthened;
			break;
			case 1:
				this.statusType = PlayerStatusTypes.poisoned;
			break;
			case 2:
				this.statusType = PlayerStatusTypes.defensive;
			break;
			case 3:
				this.statusType = PlayerStatusTypes.healing;
			break;
			case 4:
				this.statusType = PlayerStatusTypes.offblock;
			break;
			case 5:
				this.statusType = PlayerStatusTypes.breathing;
			break;
			case 6:
				this.statusType = PlayerStatusTypes.evasion;
			break;
			case 7:
				this.statusType = PlayerStatusTypes.oneturnevasion;
			break;
		}
		this.value = newValue;
		this.value2 = newValue2;
		this.value3 = newValue3;
		this.value4 = newValue4;
		this.duration = newDuration;
	}

}
public enum PlayerStatusTypes {strengthened,poisoned,defensive,healing,offblock,breathing,evasion,oneturnevasion}