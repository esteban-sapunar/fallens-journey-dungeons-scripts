using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapController : MonoBehaviour
{
	//Singleton
	public static RandomMapController instance;
    //Values
    public List <Tile> tilesSet;
    public List <GameObject> objectsSet;
    public GameObject tileObj;
    public int goldBagsMin = 10;
    public int goldBagsMax = 50;
    public int statuesMin = 1;
    public int statuesMax = 5;
    public GameObject alertPanel;
    public GameObject battleAnimPanel;
    public int championChance = 20;
    public int hunterChance = 10;
    public int lootPileChance = 10;
    //Tiles
    public Tile backgroundTile;
    public Tile borderTile;
    public Tile stoneTile;
    public Tile campTile;
    public Tile treeTile;
    public Tile pathTile;
    public Tile bossTile;
    //Map
    public Tile [,] mapArray;
    public Item food;
    public int mapHeight = 17;
    public int mapHeightMid = 8;
    public int mapWidth = 25;
    

    void Awake()
    {
      	if(instance != null){
			Debug.LogWarning("More than one instace of Random Map Controller found!");
			return;
		}
		instance = this;
      	GenerateMap();
    }



    void GenerateMap(){
    	mapArray = new Tile[mapWidth,mapHeight];
    	for(int x =0; x < mapWidth; x++){
    		for(int y =0; y < mapHeight; y++){
    			AddTile(pathTile,x,y);
    		}
    	}
    	for(int x =-10; x < mapWidth+10; x++){
    		for(int y =-10; y < 0; y++){
    			AddBackgroundTile(backgroundTile,x,y);
    		}
    		for(int y =mapHeight; y < mapHeight+10; y++){
    			AddBackgroundTile(backgroundTile,x,y);
    		}
    	}
    	for(int y =0; y < mapHeight; y++){
    		for(int x =-10; x < 0; x++){
    			AddBackgroundTile(backgroundTile,x,y);
    		}
    		for(int x =mapWidth; x < mapWidth+10; x++){
    			AddBackgroundTile(backgroundTile,x,y);
    		}
    	}
    	for(int i=0;i<mapWidth;i++){
    		changeTile(borderTile,i,0,null);
    		changeTile(borderTile,i,mapHeight-1,null);
    	}
    	for(int i=0;i<mapHeight;i++){
    		changeTile(borderTile,0,i,null);
    		changeTile(borderTile,mapWidth-1,i,null);
    	}
    	for(int x =1; x < mapWidth-1;x++){
    		for(int y =1; y < mapHeight-1;y++){
    			Tile tile = tilesSet[Random.Range(0,tilesSet.Count)];
    			switch ((int)tile.tileType) {
    				case 0:
    					if((int)mapArray[x+1,y].tileType != 0 &&
    						(int)mapArray[x-1,y].tileType != 0 &&
    						(int)mapArray[x,y+1].tileType != 0 &&
    						(int)mapArray[x,y-1].tileType != 0 &&
    						(int)mapArray[x+1,y+1].tileType != 0 &&
    						(int)mapArray[x-1,y+1].tileType != 0 &&
    						(int)mapArray[x+1,y-1].tileType != 0 &&
    						(int)mapArray[x-1,y-1].tileType != 0
    					) {
    						changeTile(tile,x,y,null);
    					}
    					else{
    						changeTile(pathTile,x,y,null);
    					}
    				break;
    				case 3:
    					changeTile(tile,x,y,null);
    				break;
    				case 4:
    					if((int)mapArray[x+1,y].tileType != 4 &&
    						(int)mapArray[x-1,y].tileType != 4 &&
    						(int)mapArray[x,y+1].tileType != 4 &&
    						(int)mapArray[x,y-1].tileType != 4 &&
    						(int)mapArray[x+1,y+1].tileType != 4 &&
    						(int)mapArray[x-1,y+1].tileType != 4 &&
    						(int)mapArray[x+1,y-1].tileType != 4 &&
    						(int)mapArray[x-1,y-1].tileType != 4
    					) {
    						changeTile(tile,x,y,objectsSet[0]);

    					}
    					else{
    						changeTile(pathTile,x,y,null);
    					}
    				break;
    				case 5:
    					if((int)mapArray[x+1,y].tileType != 5 &&
    						(int)mapArray[x-1,y].tileType != 5 &&
    						(int)mapArray[x,y+1].tileType != 5 &&
    						(int)mapArray[x,y-1].tileType != 5 &&
    						(int)mapArray[x+1,y+1].tileType != 5 &&
    						(int)mapArray[x-1,y+1].tileType != 5 &&
    						(int)mapArray[x+1,y-1].tileType != 5 &&
    						(int)mapArray[x-1,y-1].tileType != 5
    					) {
    						changeTile(tile,x,y,objectsSet[1]);
    					}
    					else{
    						changeTile(pathTile,x,y,null);
    					}
    				break;
    				case 6:
    					changeTile(tile,x,y,objectsSet[3]);
    				break;
    				case 7:
    					changeTile(tile,x,y,objectsSet[2]);
    				break;
    				case 8:
    					changeTile(tile,x,y,objectsSet[4]);
    				break;
    				case 9:
    					changeTile(tile,x,y,objectsSet[5]);
    				break;
    				case 10:
    					int chanceChamp = Random.Range(0,100);
    					if(chanceChamp < championChance){
    						changeTile(tile,x,y,objectsSet[6]);
    					}
    					else{
    						changeTile(pathTile,x,y,null);
    					}
    				break;
    				case 11:
    					int chanceHunter = Random.Range(0,100);
    					if(chanceHunter < hunterChance){
    						changeTile(tile,x,y,objectsSet[7]);
    					}
    					else{
    						changeTile(pathTile,x,y,null);
    					}
    				break;
    				case 12:
    					int chanceLootPile = Random.Range(0,100);
    					if(chanceLootPile < lootPileChance){
    						changeTile(tile,x,y,objectsSet[8]);
    					}
    					else{
    						changeTile(pathTile,x,y,null);
    					}
    				break;
    			}
    		}
    	}
    	//Start and End
    	changeTile(pathTile,0,mapHeightMid,null);
    	changeTile(pathTile,1,mapHeightMid,null);
    	changeTile(bossTile,mapWidth-1,mapHeightMid,objectsSet[3]);
    	changeTile(pathTile,mapWidth-2,mapHeightMid,null);

    }

    void AddTile(Tile newTile,int x,int y) {
    	mapArray[x,y] = newTile;
    	GameObject tile = Instantiate(tileObj,gameObject.transform);
    	tile.transform.parent = gameObject.transform;
    	tile.transform.localPosition = new Vector3(x,y,0);
    	tile.GetComponent<SpriteRenderer>().sprite = newTile.sprites[Random.Range(0,newTile.sprites.Length)];
    	tile.name = string.Format("Tile_{0}_{1}",x,y);
    }

    void AddBackgroundTile(Tile newTile,int x,int y) {
    	GameObject tile = Instantiate(tileObj,gameObject.transform);
    	tile.transform.parent = gameObject.transform;
    	tile.transform.localPosition = new Vector3(x,y,0);
    	tile.GetComponent<SpriteRenderer>().sprite = newTile.sprites[Random.Range(0,newTile.sprites.Length)];
    	tile.name = string.Format("Tile_{0}_{1}",x,y);
    }

    void changeTile(Tile newTile,int x,int y, GameObject obj) {
    	mapArray[x,y] = newTile;
    	GameObject tile = GameObject.Find(string.Format("Tile_{0}_{1}",x,y));
    	tile.GetComponent<SpriteRenderer>().sprite = newTile.sprites[Random.Range(0,newTile.sprites.Length)];
    	tile.GetComponent<MapTileController>().DeleteObject();
    	if(obj != null){
    		tile.GetComponent<MapTileController>().SetObject(obj);
    	}

    }

    void ChangeTileType(Tile newTile,int x,int y) {
    	mapArray[x,y] = newTile;
    	GameObject tile = GameObject.Find(string.Format("Tile_{0}_{1}",x,y));
    }

    public void UseTile(int x,int y,int xMove,int yMove){
    	GameObject tile = GameObject.Find(string.Format("Tile_{0}_{1}",x,y));
    	string textAlert = "";
    	switch ((int)mapArray[x,y].tileType) {
    		case 3:
    		if(y > 0 && x > 0){
    			if((int)mapArray[x+1,y].tileType == 4 || (int)mapArray[x+1,y].tileType == 6 || (int)mapArray[x+1,y].tileType == 10) {
    				UseTile(x+1,y,1,0);
    				break;
    			}
    			if((int)mapArray[x+1,y+1].tileType == 4 || (int)mapArray[x+1,y+1].tileType == 6 || (int)mapArray[x+1,y+1].tileType == 10) {
    				UseTile(x+1,y+1,1,1);
    				break;
    			}
    			if((int)mapArray[x+1,y-1].tileType == 4 || (int)mapArray[x+1,y-1].tileType == 6 || (int)mapArray[x+1,y-1].tileType == 10) {
    				UseTile(x+1,y-1,1,-1);
    				break;
    			}    		
    			if((int)mapArray[x,y+1].tileType == 4 || (int)mapArray[x,y+1].tileType == 6 || (int)mapArray[x,y+1].tileType == 10) {
    				UseTile(x,y+1,0,1);
    				break;
    			}
    			if((int)mapArray[x,y-1].tileType == 4 || (int)mapArray[x,y-1].tileType == 6 || (int)mapArray[x,y-1].tileType == 10) {
    				UseTile(x,y-1,0,-1);
    				break;    				
    			}
    			if((int)mapArray[x-1,y].tileType == 4 || (int)mapArray[x-1,y].tileType == 6 || (int)mapArray[x-1,y].tileType == 10) {
    				UseTile(x-1,y,-1,0);
    				break;
    			}
    			if((int)mapArray[x-1,y+1].tileType == 4 || (int)mapArray[x-1,y+1].tileType == 6 || (int)mapArray[x-1,y+1].tileType == 10) {
    				UseTile(x-1,y+1,-1,1);
    				break;
    			}
    			if((int)mapArray[x-1,y-1].tileType == 4 || (int)mapArray[x-1,y-1].tileType == 6 || (int)mapArray[x-1,y-1].tileType == 10) {
    				UseTile(x-1,y-1,-1,-1);
    				break;
    			}
    		}

    		break;
    		case 4:
                battleAnimPanel.SetActive(true);
                battleAnimPanel.GetComponent<BattleAnimController>().InitAnim();
    			SoundManager.instance.PlayEffect("Fight",false,false);
    			tile.GetComponent<MapTileController>().MoveObject(x-xMove,y-yMove);
    			StartCoroutine(Attacking(tile));
    			ChangeTileType(pathTile,x,y);
    		break;
    		case 5:
    			SoundManager.instance.PlayEffect("Chest",false,false);
    			string itemName = BattleController.instance.DropItemText(300);
    			ChangeTileType(pathTile,x,y);
    			tile.GetComponent<MapTileController>().DeleteObject();
    			alertPanel.SetActive(true);
    			alertPanel.GetComponent<MapAlertController>().SendAlert("+1 "+itemName,"item");
    		break;
    		case 6:
                battleAnimPanel.SetActive(true);
                battleAnimPanel.GetComponent<BattleAnimController>().InitAnim();
    			SoundManager.instance.PlayEffect("Fight",false,false);
    			tile.GetComponent<MapTileController>().MoveObject(x-xMove,y-yMove);
    			StartCoroutine(AttackingFinal(tile));
    			ChangeTileType(pathTile,x,y);
    		break;
    		case 7:
    			SoundManager.instance.PlayEffect("Equip",false,false);
    			Inventory.instance.Add(food);
    			ChangeTileType(pathTile,x,y);
    			tile.GetComponent<MapTileController>().DeleteObject();
    			alertPanel.SetActive(true);
    			textAlert = "+1 "+food.name;
    			alertPanel.GetComponent<MapAlertController>().SendAlert(textAlert,"item");
    		break;
    		case 8:
    			SoundManager.instance.PlayEffect("Coins",false,false);
    			int goldEarned = Random.Range(goldBagsMin,goldBagsMax);
    			BattleController.instance.earnedGold += goldEarned;
    			BattleController.instance.goldNumber.text = BattleController.instance.earnedGold.ToString();
    			ChangeTileType(pathTile,x,y);
    			tile.GetComponent<MapTileController>().DeleteObject();
    			alertPanel.SetActive(true);
    			textAlert = "+ "+goldEarned+" Gold";
    			alertPanel.GetComponent<MapAlertController>().SendAlert(textAlert,"gold");
    		break;
    		case 9:
    			SoundManager.instance.PlayEffect("Point",false,false);
                int pointsEarned = Random.Range(statuesMin,statuesMax);
    			BattleController.instance.earnedCardPoints += pointsEarned;
    			BattleController.instance.pointsNumber.text = BattleController.instance.earnedCardPoints.ToString();
    			ChangeTileType(pathTile,x,y);
    			tile.GetComponent<MapTileController>().DeleteObject();
    			alertPanel.SetActive(true);
                textAlert = "+ "+pointsEarned+" Card Points";
    			alertPanel.GetComponent<MapAlertController>().SendAlert(textAlert,"card_points");
    		break;
    		case 10:
                battleAnimPanel.SetActive(true);
                battleAnimPanel.GetComponent<BattleAnimController>().InitAnim();
    			SoundManager.instance.PlayEffect("Fight",false,false);
    			tile.GetComponent<MapTileController>().MoveObject(x-xMove,y-yMove);
    			StartCoroutine(AttackingChampion(tile));
    			ChangeTileType(pathTile,x,y);
    		break;
    		case 11:
    			SoundManager.instance.PlayEffect("Equip",false,false);
    			string hunterItemName = BattleController.instance.DropHunterItem();
    			ChangeTileType(pathTile,x,y);
    			tile.GetComponent<MapTileController>().DeleteObject();
    			alertPanel.SetActive(true);
    			alertPanel.GetComponent<MapAlertController>().SendAlert("+1 "+hunterItemName,"item");
    		break;
    		case 12:
    			SoundManager.instance.PlayEffect("Equip",false,false);
    			string pileItemName = BattleController.instance.DropLootPileItem();
    			ChangeTileType(pathTile,x,y);
    			tile.GetComponent<MapTileController>().DeleteObject();
    			alertPanel.SetActive(true);
    			alertPanel.GetComponent<MapAlertController>().SendAlert("+1 "+pileItemName,"item");
    		break;
    		default:
    		break;
    	}
    }

    IEnumerator Attacking(GameObject tile){ 
		yield return new WaitForSeconds(0.5f);
		BattleController.instance.StartBattle();
		tile.GetComponent<MapTileController>().DeleteObject();
    }
    IEnumerator AttackingFinal(GameObject tile){ 
		yield return new WaitForSeconds(0.5f);
		BattleController.instance.StartFinalBattle();
		tile.GetComponent<MapTileController>().DeleteObject();
    }
    IEnumerator AttackingChampion(GameObject tile){ 
		yield return new WaitForSeconds(0.5f);
		BattleController.instance.OpenChampionPanel();
		tile.GetComponent<MapTileController>().DeleteObject();
    }
}
