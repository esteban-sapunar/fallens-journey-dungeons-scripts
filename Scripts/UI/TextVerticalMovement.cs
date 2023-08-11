using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextVerticalMovement : MonoBehaviour
{
    //Values
    public float speed = 0.5f;
    public float lifeTime = 1.5f;
    bool onMovement = false;
    Vector3 initialPos = new Vector3();

    public void StartMovement(){
    	onMovement = true;
    	StartCoroutine(LifeCycle());
    }

    void Awake(){
    	initialPos = transform.localPosition;
    }

    void Update()
    {	
    	if(onMovement){
    		transform.localPosition += Vector3.up * speed * Time.deltaTime;
    	}  
    }

    IEnumerator LifeCycle(){
        yield return new WaitForSeconds(lifeTime);
        onMovement = false;
        transform.localPosition = initialPos;
        gameObject.SetActive(false);
    }
}
