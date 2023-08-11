using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChampionPanelController : MonoBehaviour
{
    // Values
    public Text name;
    public Image icon;
    public Text description;

    //Methods
    public void SetPanel(Enemy enemy){
    	name.text = enemy.name;
    	icon.sprite = enemy.sprite;
    	description.text = enemy.description;
    }

    public void ClosePanel(){
        BattleController.instance.StartChampionBattle();
    	gameObject.SetActive(false);
    }
}
