using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusSlot : MonoBehaviour
{
    // Values
    public EnemyStatus status;
    public Image icon;
    public Text durationText;
    public Sprite stunned_icon;

    public void SetStatus(EnemyStatus newStatus){
    	status = newStatus;
    	if(status.type == EnemyStatusTypes.stunned){
    		icon.sprite = stunned_icon;
    	}
    	durationText.text = status.duration.ToString();
    }    
}
