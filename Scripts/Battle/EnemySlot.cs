using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlot : MonoBehaviour
{
    //Values
    public Enemy enemy;
    public int currentHealth;
    public int maxHealth;
    public EnemyAction currentAction = new EnemyAction();
    public int damageTaken = 0;
    public int blockAmount = 0;
    public bool death = false;
    public List <EnemyStatus> statusList = new List<EnemyStatus>();
    public List <GameObject> statusSlots = new List<GameObject>();

    //UI
    public Text currentHealthNumber;
    public Text maxHealthNumber;
    public Slider healthSlider;
    public Animator anim;
    public GameObject damageEffect;
    public GameObject blockEffect;
    public GameObject blockEffect2;
    public GameObject defenseEffect;
    public GameObject deathEffect;
    public GameObject DamageText;
    public GameObject CritEffect;

	public Text name;
    public Image enemyIcon;
	public Text armor;
    public GameObject action;
	public Image actionIcon;
	public Text actionValue;
    public GameObject action2;
    public Image actionIcon2;
    public Text actionValue2;
    public GameObject action3;
    public Image actionIcon3;
    public Text actionValue3;
	public Sprite attack;
	public Sprite defense;
	public Sprite healing;
    public Sprite poison;
    public Sprite weaken;
    public Sprite stunned;

    public Animator enemyAnim;
    //Colors
    Color attackColor;
    Color defenseColor;
    Color healingColor;
    Color passiveColor;

    void Awake() {
        ColorUtility.TryParseHtmlString("#BC4715",out attackColor);
        ColorUtility.TryParseHtmlString("#0A90BC",out defenseColor);
        ColorUtility.TryParseHtmlString("#5BCD1B",out healingColor);
        ColorUtility.TryParseHtmlString("#CACACA",out passiveColor);
    }

	public void SetEnemy(Enemy newEnemy){
		enemy = newEnemy;
        name.text = enemy.name;
		maxHealth = enemy.health;
		currentHealth = enemy.health;
		enemyIcon.sprite = enemy.sprite;
		armor.text = enemy.armor.ToString();
		maxHealthNumber.text = enemy.health.ToString();
		currentHealthNumber.text = enemy.health.ToString();
		healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        SetAction();
        UpdateAction();
	}

    public void Heal(int life){
        if(maxHealth-currentHealth > life){
            currentHealth += life;
        }
        else{
            currentHealth = maxHealth;
        }
        healthSlider.value = currentHealth;
        currentHealthNumber.text = currentHealth.ToString(); 
    }

    public void TakeDamage(int dmg,int armorReduPercent){
        int damage = dmg - (int)Mathf.Round((100f-armorReduPercent)*enemy.armor/100f);
        if(damage > 0){
            if((int)currentAction.type == 1 || (int)currentAction.type_aux == 1 || (int)currentAction.type_aux_2 == 1) {
                if(blockAmount >= damage){
                    blockAmount -= damage;
                    damage = 0;
                    StartCoroutine(BlockEffect());
                    anim.SetTrigger("Slash");
                }
                else{
                    enemyAnim.SetTrigger("Damage");
                    damage -= blockAmount;
                    blockAmount = 0;
                    DamageText.SetActive(true);
                    DamageText.GetComponent<DamageTextController>().StartDamage(damage.ToString());
                    if(currentHealth <= damage){
		                currentHealth = 0;
		                currentHealthNumber.text = currentHealth.ToString();
		                healthSlider.value = currentHealth;
		                anim.SetTrigger("Slash");
		                StartCoroutine(DamageEffect());
		                Die();
		            }
		            else{
		                currentHealth -= damage;  
		                currentHealthNumber.text = currentHealth.ToString();
		                healthSlider.value = currentHealth;
		                anim.SetTrigger("Slash");
		                StartCoroutine(DamageEffect());
		            }
                }
                if((int)currentAction.type == 1 || (int)currentAction.type_aux == 1){
                    actionValue.text = blockAmount.ToString();
                }
                if((int)currentAction.type_aux_2 == 1){
                    actionValue2.text = blockAmount.ToString();
                }
                
            }
            else{
                DamageText.SetActive(true);
                DamageText.GetComponent<DamageTextController>().StartDamage(damage.ToString());
                enemyAnim.SetTrigger("Damage");
            	if(currentHealth <= damage){
	                currentHealth = 0;
	                currentHealthNumber.text = currentHealth.ToString();
	                healthSlider.value = currentHealth;
	                anim.SetTrigger("Slash");
                    Die();
	                StartCoroutine(DamageEffect());
	            }
	            else{
	                currentHealth -= damage;  
	                currentHealthNumber.text = currentHealth.ToString();
	                healthSlider.value = currentHealth;
	                anim.SetTrigger("Slash");
	                StartCoroutine(DamageEffect());
	            }
            }
        }

    }

    public void Die(){
        death = true;
        RemoveAllStatus();
        StartCoroutine(Dying());
    }

    public void Stunned(){
        SoundManager.instance.PlayEffect("Debuff",false,false);
        EnemyStatus newStatus = new EnemyStatus(EnemyStatusTypes.stunned,50,1);
        statusList.Add(newStatus);
        currentAction.value = (int)Mathf.Round(currentAction.value/2f);
        currentAction.value_2 = (int)Mathf.Round(currentAction.value_2/2f);
        foreach(GameObject statusSlot in statusSlots){
            if(!statusSlot.active){
                statusSlot.SetActive(true);
                statusSlot.GetComponent<EnemyStatusSlot>().SetStatus(newStatus);
                break;
            }
        }
        UpdateAction();
    }

    public bool IsStunned(){
        foreach(EnemyStatus enemyStatus in statusList){
            if(enemyStatus.type == EnemyStatusTypes.stunned){
                return true;
            }
        }

        return false;
    }

    public void EnemyTurn(){
        if(enemy != null){
            UpdateStatus();
            SetAction();
            UpdateAction();
        }
    }

    public void TakeCritical(){
        CritEffect.SetActive(true);
        CritEffect.GetComponent<TextVerticalMovement>().StartMovement();
    }

    public void Attack(){
        enemyAnim.SetTrigger("Attack");
    }

    void UpdateAction(){
        actionValue.text = currentAction.value.ToString();
        switch ((int)currentAction.type){
            case 0:
                action.GetComponent<Image>().color = attackColor;
                actionIcon.sprite = attack;
                action2.SetActive(false);
            break;

            case 1:
                action.GetComponent<Image>().color = defenseColor;
                actionIcon.sprite = defense;
                blockAmount = currentAction.value;
                StartCoroutine(DefenseEffect());
                action2.SetActive(false);
            break;

            case 2:
                action.GetComponent<Image>().color = healingColor;
                actionIcon.sprite = healing;
                action2.SetActive(false);
            break;

            case 3:
                action.GetComponent<Image>().color = attackColor;
                actionIcon.sprite = poison;
                action2.SetActive(false);
            break;
            case 4:
                action.GetComponent<Image>().color = attackColor;
                actionIcon.sprite = weaken;
                action2.SetActive(false);
            break;
            case 5:
                action2.SetActive(true);
                actionValue2.text = currentAction.value_2.ToString();
                switch ((int)currentAction.type_aux){
                    case 0:
                        action.GetComponent<Image>().color = attackColor;
                        actionIcon.sprite = attack;
                    break;
                    case 1:
                        action.GetComponent<Image>().color = defenseColor;
                        actionIcon.sprite = defense;
                        blockAmount = currentAction.value;
                        StartCoroutine(DefenseEffect());
                    break;

                    case 2:
                        action.GetComponent<Image>().color = healingColor;
                        actionIcon.sprite = healing;
                    break;

                    case 3:
                        action.GetComponent<Image>().color = attackColor;
                        actionIcon.sprite = poison;
                    break;
                    case 4:
                        action.GetComponent<Image>().color = attackColor;
                        actionIcon.sprite = weaken;
                    break;
                }
                switch ((int)currentAction.type_aux_2){
                    case 0:
                        action2.GetComponent<Image>().color = attackColor;
                        actionIcon2.sprite = attack;
                    break;
                    case 1:
                        action2.GetComponent<Image>().color = defenseColor;
                        actionIcon2.sprite = defense;
                        blockAmount = currentAction.value_2;
                        StartCoroutine(DefenseEffect2());
                    break;

                    case 2:
                        action2.GetComponent<Image>().color = healingColor;
                        actionIcon2.sprite = healing;
                    break;

                    case 3:
                        action2.GetComponent<Image>().color = attackColor;
                        actionIcon2.sprite = poison;
                    break;
                    case 4:
                        action2.GetComponent<Image>().color = attackColor;
                        actionIcon2.sprite = weaken;
                    break;
                }
            break;
        }
    }

    void UpdateStatus(){
        List <EnemyStatus> removeStatusList = new List<EnemyStatus>();
        foreach(EnemyStatus enemyStatus in statusList){
            enemyStatus.duration -= 1;
            foreach(GameObject statusSlot in statusSlots){
                if(statusSlot.active){
                    if(statusSlot.GetComponent<EnemyStatusSlot>().status == enemyStatus){
                        if(enemyStatus.duration == 0){
                            removeStatusList.Add(enemyStatus);
                            statusSlot.SetActive(false);
                        }
                    }
                }
            }
        }
        foreach(EnemyStatus enemyStatus in removeStatusList){
            statusList.Remove(enemyStatus);
        }
    }

    void RemoveAllStatus(){
        List <EnemyStatus> removeStatusList = new List<EnemyStatus>();
        foreach(EnemyStatus enemyStatus in statusList){
            foreach(GameObject statusSlot in statusSlots){
                if(statusSlot.active){
                    if(statusSlot.GetComponent<EnemyStatusSlot>().status == enemyStatus){
                            removeStatusList.Add(enemyStatus);
                            statusSlot.SetActive(false);
                    }
                }
            }
        }
        foreach(EnemyStatus enemyStatus in removeStatusList){
            statusList.Remove(enemyStatus);
        }
    }

    void SetAction(){
        EnemyAction newAction = enemy.actions[Random.Range(0,enemy.actions.Count)];
        currentAction.value = newAction.value;
        currentAction.value2 = newAction.value2;
        currentAction.type = newAction.type;
        currentAction.type_aux = newAction.type_aux;
        currentAction.type_aux_2 = newAction.type_aux_2;
        currentAction.value_2 = newAction.value_2;
        currentAction.value2_2 = newAction.value2_2;
    }

    IEnumerator DamageEffect(){
        SoundManager.instance.PlayEffect("Hit",false,false);
    	damageEffect.SetActive(true);
    	yield return new WaitForSeconds(1f);
    	damageEffect.SetActive(false);
    }

    IEnumerator BlockEffect(){
        SoundManager.instance.PlayEffect("Block",false,false);
    	blockEffect.SetActive(true);
    	yield return new WaitForSeconds(1f);
    	blockEffect.SetActive(false);
    }

    IEnumerator DefenseEffect(){
        defenseEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        defenseEffect.SetActive(false);
    }
    IEnumerator DefenseEffect2(){
        defenseEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        defenseEffect.SetActive(false);
    }
    IEnumerator Dying(){
        SoundManager.instance.PlayEffect("Enemy Death",false,false);
        enemyIcon.enabled = false;
        deathEffect.SetActive(true);
        BattleController.instance.EnemyDied(enemy.exp,enemy.gold,enemy.dropPercent);
        yield return new WaitForSeconds(0.7f);
        DamageText.GetComponent<DamageTextController>().ResetText();
        deathEffect.SetActive(false);
        damageEffect.SetActive(false);
        blockEffect.SetActive(false);
        enemy = null;
        enemyIcon.enabled = true;
        gameObject.SetActive(false);
    }
}
