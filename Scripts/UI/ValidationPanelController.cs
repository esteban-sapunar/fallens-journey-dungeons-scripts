using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationPanelController : MonoBehaviour
{
	//Delegate
	public delegate void DelegatedMethod();
	public DelegatedMethod delegatedMethodCall;

	//Methods
    public void Confirm(){
    	delegatedMethodCall?.Invoke();
    	gameObject.SetActive(false);
    }

    public void Cancel(){
    	gameObject.SetActive(false);
    }

    public void SetMethod(DelegatedMethod newFunction){
    	delegatedMethodCall = newFunction;
    }
}
