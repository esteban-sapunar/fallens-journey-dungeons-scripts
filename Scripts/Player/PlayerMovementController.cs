using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
	//Singleton
	public static PlayerMovementController instance;
	//Values
	public Vector2 playerPos = new Vector2(0,0);
	public Vector2 newPos = new Vector2(0,0);
	public float speed = 1f; 
	Vector3 movementVector;
	bool moving = false;

	void Awake()
    {
      	if(instance != null){
			Debug.LogWarning("More than one instace of Player Movement Controller found!");
			return;
		}
		instance = this;
    }


    void FixedUpdate()
    {
    	if(moving) {
    		if(Mathf.Abs(gameObject.transform.localPosition.x - newPos.x) > 0.01 || Mathf.Abs(gameObject.transform.localPosition.y - newPos.y) > 0.01) {
    			gameObject.transform.localPosition += movementVector*speed*Time.fixedDeltaTime;
    		}  
    	}
    	      
    }

    //Methods
    public void MovingUp() {
    	if((int)RandomMapController.instance.mapHeight > (int)playerPos.y && (int)playerPos.y > 0 ){
	    	if((int)RandomMapController.instance.mapArray[(int)playerPos.x,(int)playerPos.y+1].tileType != 0 && (int)RandomMapController.instance.mapArray[(int)playerPos.x,(int)playerPos.y+1].tileType != 1 && moving == false){
	    		moving = true;
	    		newPos = new Vector2(playerPos.x,playerPos.y+1); 
		    	playerPos = newPos;
		    	movementVector = new Vector3(0,1,0);
		    	RandomMapController.instance.UseTile((int)playerPos.x,(int)playerPos.y,0,1);
		    	StartCoroutine(Moving());
	    	}
    	}
    }

    public void MovingDown() {
    	if((int)RandomMapController.instance.mapHeight > (int)playerPos.y && (int)playerPos.y > 0 ){
	    	if((int)RandomMapController.instance.mapArray[(int)playerPos.x,(int)playerPos.y-1].tileType != 0 && (int)RandomMapController.instance.mapArray[(int)playerPos.x,(int)playerPos.y-1].tileType != 1 && moving == false){
	    		moving = true;
	    		newPos = new Vector2(playerPos.x,playerPos.y-1);
	    		playerPos = newPos;
	    		movementVector = new Vector3(0,-1,0);
	    		RandomMapController.instance.UseTile((int)playerPos.x,(int)playerPos.y,0,-1);
	    		StartCoroutine(Moving());
	    	}
	    }
    }

    public void MovingLeft() {
    	if((int)RandomMapController.instance.mapWidth > (int)playerPos.x  && (int)playerPos.x > 0 ){
	    	if((int)RandomMapController.instance.mapArray[(int)playerPos.x-1,(int)playerPos.y].tileType != 0 && (int)RandomMapController.instance.mapArray[(int)playerPos.x-1,(int)playerPos.y].tileType != 1 && moving == false){
	    		moving = true;
	    		newPos = new Vector2(playerPos.x-1,playerPos.y);
	    		playerPos = newPos;
	    		movementVector = new Vector3(-1,0,0);
	    		RandomMapController.instance.UseTile((int)playerPos.x,(int)playerPos.y,-1,0);
	    		StartCoroutine(Moving());
	    	}
	    }
    }

    public void MovingRight() {
    	if((int)RandomMapController.instance.mapWidth > (int)playerPos.x ){
	    	if((int)RandomMapController.instance.mapArray[(int)playerPos.x+1,(int)playerPos.y].tileType != 0 && (int)RandomMapController.instance.mapArray[(int)playerPos.x+1,(int)playerPos.y].tileType != 1 && moving == false){
		    	moving = true;
		    	newPos = new Vector2(playerPos.x+1,playerPos.y);
		    	movementVector = new Vector3(1,0,0);
		    	playerPos = newPos;
		    	RandomMapController.instance.UseTile((int)playerPos.x,(int)playerPos.y,1,0);
		    	StartCoroutine(Moving());
	    	}
	    }
    }

    IEnumerator Moving(){
		yield return new WaitForSeconds(0.4f);
		moving = false;
    }
}
