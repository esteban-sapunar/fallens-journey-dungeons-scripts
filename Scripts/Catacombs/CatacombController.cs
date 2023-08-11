using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CatacombController : MonoBehaviour
{	
	//Values
	public GameData data;
	public Transform slotsParent;
	public GameObject loadingPanel;

	//Basic Functions
	void Awake(){
		DataPersistenceManager.instance.LoadGame();
      	data = DataPersistenceManager.instance.GetData();
		string StagesString = data.stages;
      	string[] arrayStages =  StagesString.Split(char.Parse("|"));
		for(int i =1; i < slotsParent.childCount;i++){
			GameObject slot = slotsParent.GetChild(i).gameObject;
			slot.SetActive(false);
			if(Array.Exists(arrayStages,s => s == (i-1).ToString())){
				slot.SetActive(true);
			}
		}
	}
    //Methods
    public void Back(){
    	StartCoroutine(LoadingExit());
    }

    IEnumerator LoadingExit(){
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<LoadingPanelController>().FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("TownMenu");
    }
}
