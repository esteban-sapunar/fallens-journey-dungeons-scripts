using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName ="Game/Map/Tile")]
public class Tile : ScriptableObject
{
	//Values
	new public string name = "New Tile";
	public Sprite [] sprites = null;
	public TileType tileType;

}
public enum TileType {Null,Border,Current,Path,Enemy,Chest,Boss,Food,Gold,Statue,Champion,HunterSack,LootPile}
