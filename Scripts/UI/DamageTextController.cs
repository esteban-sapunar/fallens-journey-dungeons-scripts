using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextController : MonoBehaviour
{
    //Values
    public float speed = 0.9f;
    public float lifeTime = 0.5f;
    public Text damageText;
    bool onMovement = false;
    Vector3 initialPos = new Vector3();

    void Awake(){
    	initialPos = transform.localPosition;
    }

    void Update()
    {	
    	if(onMovement){
    		transform.localPosition += Vector3.up * speed * Time.deltaTime;
    	}  
    }

    public void StartDamage(string newText){
    	damageText.text = newText;
    	onMovement = true;
    	StartCoroutine(LifeCycle());
    }

    public void ResetText(){
    	onMovement = false;
        transform.localPosition = initialPos;
        gameObject.SetActive(false);
    }

    IEnumerator LifeCycle(){
        yield return new WaitForSeconds(lifeTime);
        onMovement = false;
        transform.localPosition = initialPos;
        gameObject.SetActive(false);
    }
}
