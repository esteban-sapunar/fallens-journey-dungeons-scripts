using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSlot : MonoBehaviour
{
    //Values
    public PlayerStatus status;
    //UI
    public Image icon;
    public Text valueText;
    public Text durationText;
    public Text turnText;
    public Sprite poison_icon;
    public Sprite strength_icon;
    public Sprite defensive_icon;
    public Sprite weaken_icon;
    public Sprite healing_icon;
    public Sprite spike_icon;
    public Sprite breathing_icon;
    public Sprite evasion_icon;


    public void SetStatus(PlayerStatus newStatus){
    	status = newStatus;
        valueText.text = newStatus.value.ToString();
    	switch ((int)newStatus.statusType){
            case 0:
                if(status.value >= 0){
                    icon.sprite = strength_icon;
                }
                else{
                    icon.sprite = weaken_icon;
                }          	
            break;
            case 1:
            	icon.sprite = poison_icon;
            break;
            case 2:
                icon.sprite = defensive_icon;
            break;
            case 3:
                icon.sprite = healing_icon;
            break;
            case 4:
                icon.sprite = spike_icon;
            break;
            case 5:
                icon.sprite = breathing_icon;
                valueText.text = "";
            break;
            case 6:
                icon.sprite = evasion_icon;
            break;
            case 7:
                icon.sprite = evasion_icon;
            break;
        }
        if(newStatus.duration >= 0){
            durationText.text = newStatus.duration.ToString();
            turnText.text = "Turns"; 
        }
        else{
            durationText.text = "";
            turnText.text = ""; 
        }
        
    }
    public PlayerStatusTypes StatusType(){
        return status.statusType;
    }
}
