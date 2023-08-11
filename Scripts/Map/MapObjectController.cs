using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectController : MonoBehaviour
{
	//Values
	public ObjectType type;

    public enum ObjectType {Enemy,Chest,Null,Food,Gold,Statue,Champion,HunterSack,LootPile}
}
 