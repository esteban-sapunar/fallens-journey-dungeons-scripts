using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InnSlot : MonoBehaviour
{
    //Values
    public Mission mission;
	public Text name;
	public Image image;
	public GameObject missionPanel;

	//Methods
	public void SetMission(Mission newMission){
		gameObject.SetActive(true);
		mission = newMission;
		name.text = mission.title;
		image.sprite = mission.image;
	}

	public void Talk(){
		missionPanel.SetActive(true);
		missionPanel.GetComponent<MissionPanelController>().SetPanel(mission,gameObject);
	}
}
