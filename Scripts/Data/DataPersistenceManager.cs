using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
	[Header("File Storage Config")]
	[SerializeField] private string fileName;

	//Values
	private GameData gameData;
	private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }


    //Default Methods
    private void Awake(){
    	if(instance != null){
			Debug.LogWarning("More than one instace of Battle Controller found!");
			return;
		}
		instance = this;

		this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    //Methods
    public void NewGame(){
    	this.gameData = new GameData();
    }

    public void LoadGame(){
    	this.gameData = dataHandler.Load();
    }

    public GameData GetData(){
    	return gameData;
    }

    public void SetData(GameData data){
    	this.gameData = data;
    }

    public void SaveGame(){
    	dataHandler.Save(gameData);
    }
}
