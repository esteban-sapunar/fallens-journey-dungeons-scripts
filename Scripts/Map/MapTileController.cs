using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileController : MonoBehaviour
{
    //Values
    public GameObject mapObject;
    public GameObject objectOnMap;
    public Vector3 newPos;
    public float speed = 0f;
    public Vector3 movementVector;
    public bool moving = false;

    void FixedUpdate()
    {
    	if(moving){
	    	if(Mathf.Abs(objectOnMap.transform.position.x - newPos.x) > 0.01 || Mathf.Abs(objectOnMap.transform.position.y - newPos.y) > 0.01) {
	    		objectOnMap.transform.position += movementVector*speed*Time.fixedDeltaTime;
	    	}
    	}        
    }

    //Methods
    public void SetObject (GameObject newObject){
    	mapObject = newObject; 
    	GameObject obj = Instantiate(mapObject,gameObject.transform);
    	obj.transform.parent = gameObject.transform;
    	obj.transform.localPosition = new Vector3(0,0,0);
    	obj.name = string.Format("Object_{0}",gameObject.name);
    	objectOnMap = obj;
    }

    public void DeleteObject (){
    	GameObject oldObj = GameObject.Find(string.Format("Object_{0}",gameObject.name));
    	if(oldObj != null) {
    		oldObj.SetActive(false);
    	}
    }

    public void MoveObject(int x,int y){
    	movementVector = new Vector3(x-gameObject.transform.localPosition.x,y-gameObject.transform.localPosition.y,0);
    	newPos = new Vector3(x,y,0);
    	moving = true;
    }
}
