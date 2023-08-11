using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pack", menuName ="Cards/Pack")]
public class Pack : ScriptableObject
{
    //values
	new public string name = "New Pack";
	public Sprite icon = null;
	public string description;
	public int tier = 0;
	public int price = 0;
	public int points = 0;

	public List<Card> slot1 = new List<Card>();
	public List<Card> slot2 = new List<Card>();
	public List<Card> slot3 = new List<Card>();
	public List<Card> slot4 = new List<Card>();
	public List<Card> slot5 = new List<Card>();

}
