using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
	//Singleton
	public static MapController instance;

    //Map UI
    public GameObject MapPanel;
    List <GameObject> Tiles =new List<GameObject>();

    //Map
    public Tile borderTile;
    public Tile secBorderTile;
    public Tile nullTile;
    public Tile currentTile;
    public Tile pathTile;
    public Tile bossTile;
    public List <Tile> initialTiles;
    public Tile [,] mapArray = new Tile[15,7];

    //Items
    public Item food;

    //Base Functions

    void Awake(){
    	if(instance != null){
			Debug.LogWarning("More than one instace of Map Controller found!");
			return;
		}
		instance = this;

		for(int i =0; i < MapPanel.transform.childCount ;i++){
			Tiles.Add(MapPanel.transform.GetChild(i).gameObject);
		}

    	for(int i =0; i < 7;i++){
    		for(int j =0; j < 15;j++){
    			SetTile(j,i,pathTile);
    		}
    	}
    	GenerateMap();
    }

    //Methods

    void SetTile(int x,int y, Tile newTile){
    	mapArray[x,y] = newTile;
    	int index = (y*15)+x;
    	GameObject tileSlot = MapPanel.transform.GetChild(index).gameObject;
    	tileSlot.GetComponent<Image>().sprite = newTile.sprites[0];
    	tileSlot.GetComponent<TileSlot>().SetTile(x,y,newTile);
    }

    void GenerateMap(){
    	//borders
    	for(int i=0;i<15;i++){
    		SetTile(i,0,borderTile);
    		SetTile(i,6,borderTile);
    	}
    	for(int i=0;i<7;i++){
    		SetTile(0,i,borderTile);
    		SetTile(14,i,borderTile);
    	}
    	//center tiles
    	for(int i =1; i < 14;i++){
    		for(int z =1; z < 6;z++){
    			Tile tile = initialTiles[Random.Range(0,initialTiles.Count)];
    			switch ((int)tile.tileType) {
    				case 0:
    					if((int)mapArray[i+1,z].tileType != 0 &&
    						(int)mapArray[i-1,z].tileType != 0 &&
    						(int)mapArray[i,z+1].tileType != 0 &&
    						(int)mapArray[i,z-1].tileType != 0 &&
    						(int)mapArray[i+1,z+1].tileType != 0 &&
    						(int)mapArray[i-1,z+1].tileType != 0 &&
    						(int)mapArray[i+1,z-1].tileType != 0 &&
    						(int)mapArray[i-1,z-1].tileType != 0
    					) {
    						SetTile(i,z,tile); 
    					}
    					else{
    						SetTile(i,z,pathTile);
    					}
    				break;
    				case 3:
    					SetTile(i,z,tile);
    				break;
    				case 4:
    					if((int)mapArray[i+1,z].tileType != 4 &&
    						(int)mapArray[i-1,z].tileType != 4 &&
    						(int)mapArray[i,z+1].tileType != 4 &&
    						(int)mapArray[i,z-1].tileType != 4 &&
    						(int)mapArray[i+1,z+1].tileType != 4 &&
    						(int)mapArray[i-1,z+1].tileType != 4 &&
    						(int)mapArray[i+1,z-1].tileType != 4 &&
    						(int)mapArray[i-1,z-1].tileType != 4
    					) {
    						SetTile(i,z,tile);
    					}
    					else{
    						SetTile(i,z,pathTile);
    					}
    				break;
    				case 5:
    					if((int)mapArray[i+1,z].tileType != 5 &&
    						(int)mapArray[i-1,z].tileType != 5 &&
    						(int)mapArray[i,z+1].tileType != 5 &&
    						(int)mapArray[i,z-1].tileType != 5 &&
    						(int)mapArray[i+1,z+1].tileType != 5 &&
    						(int)mapArray[i-1,z+1].tileType != 5 &&
    						(int)mapArray[i+1,z-1].tileType != 5 &&
    						(int)mapArray[i-1,z-1].tileType != 5
    					) {
    						SetTile(i,z,tile);
    					}
    					else{
    						SetTile(i,z,pathTile);
    					}
    				break;
    				case 6:
    					SetTile(i,z,tile);
    				break;
    				case 7:
    					SetTile(i,z,tile);
    				break;
    			}
    		}
    	}
    	//inital and final tiles
    	SetTile(0,3,currentTile);
    	SetTile(1,3,pathTile);
    	SetTile(14,3,bossTile);
    	SetTile(14,2,secBorderTile);
    	SetTile(14,4,secBorderTile);
    	SetTile(13,3,pathTile);
    	UseSlot(0,3,currentTile);
    	int x = 1;
    	int y = 3;
    	/*for(int i =0; i < 15;i++){
    		Tile tile = initialTiles[Random.Range(0,initialTiles.Count)];
			int dir = Random.Range(0,3);
			switch (dir) {
				case 0:
					if(y <= 5){
						if((int)mapArray[x,y+1].tileType == 0 && (int)mapArray[x-1,y+1].tileType == 0){
							y+=1;
							SetTile(x,y,tile);
						}
						else{
							if(x < 14){
								x+=1;
								SetTile(x,y,tile);
							}
						}
					}
					else{
						if(x < 14){
							x+=1;
							SetTile(x,y,tile);
						}
					}
				break;
				case 1:
					if(x < 14){
						x+=1;
						SetTile(x,y,tile);
					}
				break;
				case 2:
					if(y > 0){
						if((int)mapArray[x,y-1].tileType == 0 && (int)mapArray[x-1,y-1].tileType == 0){
							y-=1;
							SetTile(x,y,tile);
						}
						else{
							if(x < 14){
								x+=1;
								SetTile(x,y,tile);
							}
						}
					}
					else{
						if(x < 14){
							x+=1;
							SetTile(x,y,tile);
						}
					}
				break;
			}
		}
		SetTile(x,y,bossTile);*/
    }

    public void UseSlot(int x,int y, Tile usedTile){
    	SetTile(x,y,currentTile);
    	foreach(GameObject tile in Tiles){
    		tile.GetComponent<Button>().interactable = false;
    	}
    	if(x < 14){
	    	if((int)mapArray[x+1,y].tileType != 0 && (int)mapArray[x+1,y].tileType != 1){	
	    		MapPanel.transform.GetChild((y*15)+x+1).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x+1,y].tileType == 2){
	    		SetTile(x+1,y,pathTile);
	    	}	
    	}
    	if(x > 0){ 	 		
	    	if((int)mapArray[x-1,y].tileType != 0 && (int)mapArray[x-1,y].tileType != 1){
	    		MapPanel.transform.GetChild((y*15)+x-1).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x-1,y].tileType == 2){
	    		SetTile(x-1,y,pathTile);
	    	}
    	}
    	if(y < 6){  		
	    	if((int)mapArray[x,y+1].tileType != 0 && (int)mapArray[x,y+1].tileType != 1){
	    		MapPanel.transform.GetChild(((y+1)*15)+x).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x,y+1].tileType == 2){
	    		SetTile(x,y+1,pathTile);
	    	}
   	 	}
   	 	if(y > 0){  	 		
	    	if((int)mapArray[x,y-1].tileType != 0 && (int)mapArray[x,y-1].tileType != 1){
	    		MapPanel.transform.GetChild(((y-1)*15)+x).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x,y-1].tileType == 2){
	    		SetTile(x,y-1,pathTile);
	    	}
    	}
    	switch ((int)usedTile.tileType) {
    		case 3:
    			List <Tile> aroundTiles = new List<Tile>();
    			List <Vector2> aroundTilesCoor = new List<Vector2>();
    			for(int i =-1; i < 2;i++){
    				for(int z =-1; z < 2;z++){
    					if((int)mapArray[x+i,y+z].tileType == 4 || (int)mapArray[x+i,y+z].tileType == 6){
    						aroundTiles.Add(mapArray[x+i,y+z]);
    						aroundTilesCoor.Add(new Vector2(x+i,y+z));
    					}
    				}
    			}
    			if(aroundTiles.Count > 0){
    				SetTile(x,y,pathTile);
    				UseSlot((int)aroundTilesCoor[0].x,(int)aroundTilesCoor[0].y,aroundTiles[0]);
    			}
    		break;
    		case 4:
    			BattleController.instance.StartBattle();
    		break;
    		case 5:
    			BattleController.instance.DropItem(300);
    		break;
    		case 6:
    			BattleController.instance.StartFinalBattle();
    		break;
    		case 7:
    			Inventory.instance.Add(food);
    		break;
    		default:
    		break;
    	}
    }
}
