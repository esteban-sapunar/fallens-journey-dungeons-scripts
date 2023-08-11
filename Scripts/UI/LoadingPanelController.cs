using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanelController : MonoBehaviour
{
    public Animator animator;

    public void FadeIn(){
    	animator.SetTrigger("FadeIn");
    }

    void Awake(){
    	StartCoroutine(LoadingFadeOut());
    }

    IEnumerator LoadingFadeOut(){
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
