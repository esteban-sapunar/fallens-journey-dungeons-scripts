using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class TouchController : MonoBehaviour
{
	//Singleton
	public static TouchController instance;

    private TouchControls touchControls;

    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnStartTouch;
    public delegate void DuringTouchEvent(Vector2 position);
    public event DuringTouchEvent OnDuringTouch;
    public delegate void EndTouchEvent(Vector2 position);
    public event EndTouchEvent OnEndTouch;

    private void Awake(){
    	if(instance != null){
			Debug.LogWarning("More than one instace of Touch Controller found!");
			return;
		}
		instance = this;
    	touchControls = new TouchControls();
    }

    private void Update(){
    	if(OnDuringTouch != null){
	    	if(touchControls.Touch.TouchPress.phase == InputActionPhase.Performed){
	    		OnDuringTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
	    		/*Debug.Log("During | "+touchControls.Touch.TouchPosition.ReadValue<Vector2>());*/
	    	}
    	}
    }

    private void OnEnable(){
    	touchControls.Enable();
    }

    private void OnDisable(){
    	touchControls.Disable();
    }

    private void Start(){
    	touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
    	touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context){
    	if(OnStartTouch != null){
    		/*Debug.Log("Start | "+touchControls.Touch.TouchPosition.ReadValue<Vector2>());*/
    		OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    	}
    }

    private void EndTouch(InputAction.CallbackContext context){
    	if(OnEndTouch != null){
    		/*Debug.Log("End | "+touchControls.Touch.TouchPosition.ReadValue<Vector2>());*/
    		OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    	}
    }
}
