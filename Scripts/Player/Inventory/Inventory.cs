using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	//Delegate
	public delegate void OnItemChanged();
	public OnItemChanged OnItemChangedCallback;

	//Values
	public int space = 16;
	public List<Item> items = new List<Item>();
	
	#region Singleton
	public static Inventory instance;
	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

	//Methods
	public bool Add (Item item){
		if(items.Count < space){
			items.Add(item);
			if(OnItemChangedCallback != null){
				OnItemChangedCallback.Invoke();
			}
			return true;
		}
		else{
			Debug.Log("Not Enough Space");
			return false;
		}
		
	}

	public void Remove (Item item){
		items.Remove(item);
		if(OnItemChangedCallback != null){
			OnItemChangedCallback.Invoke();	
		}
	}

	public int Count (Item new_item){
		int count = 0;
		foreach(Item item in items){
    		if(item == new_item){
    			count += 1;
    		}
    	}
    	return count;
	}
}
