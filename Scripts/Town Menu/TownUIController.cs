using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownUIController : MonoBehaviour
{
    //Objects
    public GameObject playerPanel;
    public GameObject cardsPanel;
    public GameObject trainerPanel;
    public GameObject shopPanel;
    public GameObject blacksmithPanel;
    public GameObject innPanel;
    public GameObject GM;
    public GameObject loadingPanel;

    //Methods
    public void Exit(){
    	GM.GetComponent<TownDataController>().SaveData();
    	StartCoroutine(LoadingExit());
    }
    public void Play(){
        if(TownDesk.instance.desk.Count == 30){
            GM.GetComponent<TownDataController>().SaveData();
            StartCoroutine(LoadingPlay());
        }
    }

    public void OpenPlayerPanel(){
        SoundManager.instance.PlayEffect("Player Menu",false,false);
    	playerPanel.SetActive(true);
    }
    public void OpenCardsPanel(){
        SoundManager.instance.PlayEffect("Cards",false,false);
        cardsPanel.SetActive(true);
    }
    public void OpenTrainerPanel(){
        SoundManager.instance.PlayEffect("Training",false,false);
        trainerPanel.SetActive(true);
    }
    public void OpenShopPanel(){
        SoundManager.instance.PlayEffect("Shop",false,false);
        shopPanel.SetActive(true);
    }
    public void OpenBlacksmithPanel(){
        SoundManager.instance.PlayEffect("Anvil",false,false);
        blacksmithPanel.SetActive(true);
    }
    public void OpenInnPanel(){
        SoundManager.instance.PlayEffect("Shop",false,false);
        innPanel.SetActive(true);
    }
    IEnumerator LoadingPlay(){
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<LoadingPanelController>().FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("CatacombMenu");
    }
    IEnumerator LoadingExit(){
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<LoadingPanelController>().FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
