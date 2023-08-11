using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSlot : MonoBehaviour
{
    //Values
    public string stage;
    public GameObject loadingPanel;

    //Methods
    public void EnterStage(){
    	StartCoroutine(LoadingPlay());
    }

    IEnumerator LoadingPlay(){
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<LoadingPanelController>().FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(stage);
    }
}
