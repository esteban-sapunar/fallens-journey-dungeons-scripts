using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyAction
{
	//Values
	public int value;
	public int value2;
	public EnemyActionType type;
	public EnemyActionType type_aux;

	public int value_2;
	public int value2_2;
	public EnemyActionType type_aux_2;
}
public enum EnemyActionType {Attack,Defend,Healing,Poison,Weaken,DoubleAction}
