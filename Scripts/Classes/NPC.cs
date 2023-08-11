using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC
{
    //Value
    public string name;
    public Sprite image;

    //Contructor
	public NPC(string newName,Sprite newImage){
		this.name = newName;
		this.image = newImage;
	}
}
