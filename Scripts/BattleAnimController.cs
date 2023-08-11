using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAnimController : MonoBehaviour
{
    public Animator animator;
    public Text text;

    public void InitAnim(){
    	StartCoroutine(AnimFadeOut());
    }

    IEnumerator AnimFadeOut(){
    	yield return new WaitForSeconds(0.4f);
    	text.text = "B";
    	yield return new WaitForSeconds(0.04f);
    	text.text = "Ba";
    	yield return new WaitForSeconds(0.04f);
    	text.text = "Bat";
    	yield return new WaitForSeconds(0.04f);
    	text.text = "Batt";
    	yield return new WaitForSeconds(0.04f);
    	text.text = "Battl";
    	yield return new WaitForSeconds(0.04f);
    	text.text = "Battle";
    	yield return new WaitForSeconds(0.04f);
    	text.text = "Battle!";
    	yield return new WaitForSeconds(0.2f);
    	animator.SetTrigger("OpenBattleAnim");
    	yield return new WaitForSeconds(0.15f);
    	text.text = "";
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}
