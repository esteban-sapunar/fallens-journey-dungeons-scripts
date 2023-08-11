using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
	//Singleton
	public static BattleController instance;

	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of Battle Controller found!");
			return;
		}
		instance = this;
	}

	//Desks
	Desk baseDesk;
	Desk activeDesk;
	Desk discardDesk;

	//Player
	public GameObject player;
	int currentExp = 0;
	int currentGold = 0;
	public int earnedExp = 0;
	public int earnedGold = 0;
	public int earnedCardPoints = 0;
	int dropPercent = 0;
	//Enemies
	public List <Enemy> enemies;
	public List <Enemy> bosses;
	public List <Enemy> champions;

	//Card Slots
	public List <GameObject> cardSlots;

	//Enemy Slots
	public List <GameObject> enemySlots;

	//Items for level
	public List <Item> levelItems;

	//Items for hunter
	public List <Item> hunterItems;
	//Items for loot pile
	public List <Item> lootPileItems;

	//Enemies Items
	public List <Item> battleItems;

	//Values
	public bool cardsPaused = false;
	bool onBattle = false;
	bool onPlayerTurn = false;
	bool onFinalBattle = false;

	//UI
	public Text deskCount;
	public Text discardCount;
	public GameObject actionsPanel;
	public Text turnText;
	public Slider healthSlider;
    public Text currentHealthNumber;
    public Text maxHealthNumber;

    public Text expNumber;
    public Text goldNumber;
    public Text pointsNumber;

    public GameObject championPanel;
    public GameObject goldRewardAlert;
    public GameObject expRewardAlert;
    public GameObject itemRewardAlert;

	//Base Functions
	void Start(){
		healthSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
        healthSlider.value = player.GetComponent<PlayerStats>().maxHealth;
        maxHealthNumber.text = player.GetComponent<PlayerStats>().maxHealth.ToString();
        currentHealthNumber.text = player.GetComponent<PlayerStats>().maxHealth.ToString();
		baseDesk = gameObject.GetComponent<DeskController>().GetBaseDesk();
		activeDesk = baseDesk;
		discardDesk = new Desk();
		player.GetComponent<PlayerStats>().OnPlayerDieCallback += LostBattle;
		expNumber.text = earnedExp.ToString();
        goldNumber.text = earnedGold.ToString();
        pointsNumber.text = earnedCardPoints.ToString();
	}

	//Methods
	public void PauseCards(){
		StartCoroutine(CardsPause(0.1f));
	}
	public bool OnBattle(){
		return onBattle;
	}

	public bool OnPlayerTurn(){
		return onPlayerTurn;
	}

	public void DiscardCard(Card discardCard){
		List<Card> discardcards = discardDesk.GetCards();
		discardDesk.AddCard(discardCard);
		discardCount.text = discardcards.Count.ToString();
	}

	public void DrawCard(GameObject slot){
		List<Card> activecards = activeDesk.GetCards();
		List<Card> discardcards = discardDesk.GetCards();
		if(activecards.Count > 0){
			Card randCard = activecards[Random.Range(0,activecards.Count)];
			activeDesk.RemoveCard(randCard);
			deskCount.text = activecards.Count.ToString();
			slot.GetComponent<CardSlot>().SetCard(randCard);
		}
		else{
			foreach(Card card in discardcards){
				activeDesk.AddCard(card);
			}
			discardDesk.RemoveAllCard();
			discardcards = discardDesk.GetCards();
			discardCount.text = discardcards.Count.ToString();
			activecards = activeDesk.GetCards();
			Card randCard = activecards[Random.Range(0,activecards.Count)];
			activeDesk.RemoveCard(randCard);
			deskCount.text = activecards.Count.ToString();
			slot.GetComponent<CardSlot>().SetCard(randCard);
		}
	}
	public void UpdateCardsNumbers(){
		for(int i =0; i < cardSlots.Count;i++){
			if(cardSlots[i].active){
				cardSlots[i].GetComponent<CardSlot>().UpdateCard();
			}
		}
	}

	public void StartBattle(){
		onBattle = true;
		actionsPanel.SetActive(true);
		//Generate enemies
		SetEnemies();
		//Start Turn
		StartPlayerTurn();
	}
	public void StartChampionBattle(){
		onBattle = true;
		actionsPanel.SetActive(true);
		//Start Turn
		StartPlayerTurn();
	}
	public void OpenChampionPanel(){
		championPanel.SetActive(true);
		SetChampion();
	}
	public void StartFinalBattle(){
		onBattle = true;
		onFinalBattle = true;
		actionsPanel.SetActive(true);
		//Generate enemies
		SetFinalEnemies();
		//Start Turn
		StartPlayerTurn();
	}

	public void DropItem(int drop){
		dropPercent += drop;
		int dropratio = (int)Mathf.Round(dropPercent/3f);
		int randomN = Random.Range(0,100);
		if(randomN <= dropratio){
			Inventory.instance.Add(levelItems[Random.Range(0,levelItems.Count)]);
		}
		dropPercent += 0;
	}
	public string DropHunterItem(){
		Item newItem = hunterItems[Random.Range(0,hunterItems.Count)];
		Inventory.instance.Add(newItem);
		return newItem.name;
	}
	public string DropLootPileItem(){
		Item newItem = lootPileItems[Random.Range(0,lootPileItems.Count)];
		Inventory.instance.Add(newItem);
		return newItem.name;
	}
	public string DropItemText(int drop){
		dropPercent += drop;
		int dropratio = (int)Mathf.Round(dropPercent/3f);
		int randomN = Random.Range(0,100);
		Item newItem = levelItems[Random.Range(0,levelItems.Count)];
		if(randomN <= dropratio){
			Inventory.instance.Add(newItem);
		}
		dropPercent += 0;
		return newItem.name;
	}
	public void DropBattleItem(int drop){
		dropPercent += drop;
		int dropratio = (int)Mathf.Round(dropPercent/3f);
		int randomN = Random.Range(0,100);
		string itemName = "";
		if(randomN <= dropratio){
			Item item = battleItems[Random.Range(0,battleItems.Count)];
			itemName = item.name;
			Inventory.instance.Add(item);
		}
		StartCoroutine(SendRewardAlerts(currentExp,currentGold,itemName));
		battleItems.Clear();
		dropPercent += 0;
	}

	public void EndBattle(){
		onBattle = false;
		onPlayerTurn = false;
		DropBattleItem(0);
		StartCoroutine(EndOfBattle());
	}

	public void LostBattle(){
		onBattle = false;
		onPlayerTurn = false;
		for(int i =0; i < cardSlots.Count;i++){
			if(cardSlots[i].active){
				activeDesk.AddCard(cardSlots[i].GetComponent<CardSlot>().GetCard());
				cardSlots[i].GetComponent<CardSlot>().UnsetCard();
				cardSlots[i].SetActive(false);
			}
		}
		List<Card> discardcards = discardDesk.GetCards();
		foreach(Card card in discardcards){
			activeDesk.AddCard(card);
		}
		discardDesk.RemoveAllCard();
		discardcards = discardDesk.GetCards();
		List<Card> activecards = activeDesk.GetCards();
		deskCount.text = activecards.Count.ToString();
		discardCount.text = discardcards.Count.ToString();
		player.GetComponent<PlayerStats>().Heal(player.GetComponent<PlayerStats>().maxHealth);
        healthSlider.value = player.GetComponent<PlayerStats>().currentHealth;
        currentHealthNumber.text = player.GetComponent<PlayerStats>().currentHealth.ToString();
        currentExp = 0;
        currentGold = 0;
        dropPercent = 0;
        cardsPaused = false;
        battleItems.Clear();
        actionsPanel.SetActive(false);
        player.GetComponent<PlayerStats>().RemoveAllStatus();
        gameObject.GetComponent<DataController>().OpenRewardPanel("death");
	}
	public void SetEnemies(){
		foreach(GameObject slot in enemySlots){
			slot.SetActive(true);
			slot.GetComponent<EnemySlot>().SetEnemy(enemies[Random.Range(0,enemies.Count)]);
			foreach(Item item in slot.GetComponent<EnemySlot>().enemy.items){
				battleItems.Add(item);
			}
		}
	}
	public void SetChampion(){
		enemySlots[0].GetComponent<EnemySlot>().death = true;
		enemySlots[0].SetActive(false);
		enemySlots[2].GetComponent<EnemySlot>().death = true;
		enemySlots[2].SetActive(false);
		enemySlots[1].SetActive(true);
		Enemy newEnemy = champions[Random.Range(0,champions.Count)];
		championPanel.GetComponent<ChampionPanelController>().SetPanel(newEnemy);
		enemySlots[1].GetComponent<EnemySlot>().SetEnemy(newEnemy);
		foreach(Item item in enemySlots[1].GetComponent<EnemySlot>().enemy.items){
			battleItems.Add(item);
		}
	}
	public void SetFinalEnemies(){
		foreach(GameObject slot in enemySlots){
			slot.SetActive(true);
			slot.GetComponent<EnemySlot>().SetEnemy(enemies[Random.Range(0,enemies.Count)]);
		}
		enemySlots[1].GetComponent<EnemySlot>().SetEnemy(bosses[Random.Range(0,bosses.Count)]);
		foreach(GameObject slot in enemySlots){
			foreach(Item item in slot.GetComponent<EnemySlot>().enemy.items){
				battleItems.Add(item);
			}
		}
	}
	public void EnemyDied(int exp,int gold,int dropPerc){
		currentExp += exp;
		currentGold += gold;
		dropPercent += dropPerc;
		if(CountEnemies() > 2){
			EndBattle();
		}
	}
	public void StartPlayerTurn(){
		cardsPaused = false;
		onPlayerTurn = true;
		player.GetComponent<PlayerStats>().ResetStamina();
		player.GetComponent<PlayerStats>().ResetBlock();
		for(int i =0; i < cardSlots.Count;i++){
			if(!cardSlots[i].active){
				cardSlots[i].SetActive(true);
				DrawCard(cardSlots[i]);
			}
		}
		player.GetComponent<PlayerStats>().NewTurn();
		turnText.GetComponent<TemporalText>().SetText("Your Turn!","green");
	}

	public void EndPlayerTurn(){
		if(onPlayerTurn){
			onPlayerTurn = false;
			cardsPaused = true;
			player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
			UpdateCardsNumbers();
			BlockCards(true);
			turnText.GetComponent<TemporalText>().SetText("Enemy Turn!","red");
			StartCoroutine(EnemiesTurns());
		}
	}

	public void AttackOneEnemy(int dmg, Vector3 pos, GameObject panel, int armorReduPercent){
		foreach(GameObject slot in enemySlots){
			if(slot.active){
				if(Mathf.Abs(slot.transform.position.x - pos.x) <= 1f && Mathf.Abs(slot.transform.position.y - pos.y) <= 1.6f && !slot.GetComponent<EnemySlot>().death){
					int effectIndex = Random.Range(1,4);
					SoundManager.instance.PlayEffect("Sword "+effectIndex,false,false);
					if(player.GetComponent<PlayerStats>().criticalChance.GetValue() > Random.Range(0,100)){
						dmg = (int)Mathf.Round(dmg*player.GetComponent<PlayerStats>().criticalMulti.GetValue()/100f);
						slot.GetComponent<EnemySlot>().TakeCritical();
					}
					slot.GetComponent<EnemySlot>().TakeDamage(dmg,armorReduPercent);
					player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
					panel.GetComponent<CardPanelController>().FinishCardUse();
					break;
				}
			}
		}
	}

	public void AttackOneEnemyEvasion(int dmg, Vector3 pos, GameObject panel, int armorReduPercent,int evasion){
		foreach(GameObject slot in enemySlots){
			if(slot.active){
				if(Mathf.Abs(slot.transform.position.x - pos.x) <= 1f && Mathf.Abs(slot.transform.position.y - pos.y) <= 1.6f && !slot.GetComponent<EnemySlot>().death){
					player.GetComponent<PlayerStats>().SetOneTurnEvasion(evasion);
					int effectIndex = Random.Range(1,4);
					SoundManager.instance.PlayEffect("Sword "+effectIndex,false,false);
					if(player.GetComponent<PlayerStats>().criticalChance.GetValue() > Random.Range(0,100)){
						dmg = (int)Mathf.Round(dmg*player.GetComponent<PlayerStats>().criticalMulti.GetValue()/100f);
						slot.GetComponent<EnemySlot>().TakeCritical();
					}
					slot.GetComponent<EnemySlot>().TakeDamage(dmg,armorReduPercent);
					player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
					panel.GetComponent<CardPanelController>().FinishCardUse();
					break;
				}
			}
		}
	}

	public void StunOneEnemy(int dmg, Vector3 pos, GameObject panel, int stunPercent){
		foreach(GameObject slot in enemySlots){
			if(slot.active){
				if(Mathf.Abs(slot.transform.position.x - pos.x) <= 1f && Mathf.Abs(slot.transform.position.y - pos.y) <= 1.6f && !slot.GetComponent<EnemySlot>().death){
					SoundManager.instance.PlayEffect("Sword 2",false,false);
					if(!slot.GetComponent<EnemySlot>().IsStunned()){
						int rnd = Random.Range(1,101);
						if(rnd <= stunPercent) {
							slot.GetComponent<EnemySlot>().Stunned();
						}
					}
					if(player.GetComponent<PlayerStats>().criticalChance.GetValue() > Random.Range(0,101)){
						dmg = (int)Mathf.Round(dmg*player.GetComponent<PlayerStats>().criticalMulti.GetValue()/100f);
						slot.GetComponent<EnemySlot>().TakeCritical();
					}
					slot.GetComponent<EnemySlot>().TakeDamage(dmg,40);			
					player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
					panel.GetComponent<CardPanelController>().FinishCardUse();
					break;
				}
			}
		}
	}

	public void AttackOneStunnedEnemy(int dmg, Vector3 pos, GameObject panel, int extraDamagePercent){
		foreach(GameObject slot in enemySlots){
			if(slot.active){
				if(Mathf.Abs(slot.transform.position.x - pos.x) <= 1f && Mathf.Abs(slot.transform.position.y - pos.y) <= 1.6f && !slot.GetComponent<EnemySlot>().death){
					SoundManager.instance.PlayEffect("Sword 2",false,false);
					if(slot.GetComponent<EnemySlot>().IsStunned()){
						dmg = (int)Mathf.Round(dmg*(100f+extraDamagePercent)/100f);
					}
					if(player.GetComponent<PlayerStats>().criticalChance.GetValue() > Random.Range(0,101)){
						dmg = (int)Mathf.Round(dmg*player.GetComponent<PlayerStats>().criticalMulti.GetValue()/100f);
						slot.GetComponent<EnemySlot>().TakeCritical();
					}
					slot.GetComponent<EnemySlot>().TakeDamage(dmg,40);			
					player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
					panel.GetComponent<CardPanelController>().FinishCardUse();
					break;
				}
			}
		}
	}
	public void AttackAllEnemies(int dmg, GameObject panel){
		int effectIndex = Random.Range(1,4);
		SoundManager.instance.PlayEffect("Sword "+effectIndex,false,false);
		StartCoroutine(AttackAll(dmg));
		panel.GetComponent<CardPanelController>().FinishCardUse();
	}

	public void MultiAttackOneEnemy(int dmg, Vector3 pos, GameObject panel,int percent, int limit){
		foreach(GameObject slot in enemySlots){
			if(slot.active){
				if(Mathf.Abs(slot.transform.position.x - pos.x) <= 1f && Mathf.Abs(slot.transform.position.y - pos.y) <= 1.6f && !slot.GetComponent<EnemySlot>().death){
					int effectIndex = Random.Range(1,4);
					SoundManager.instance.PlayEffect("Sword "+effectIndex,false,false);
					if(player.GetComponent<PlayerStats>().criticalChance.GetValue() > Random.Range(0,100)){
						int initialDmg = (int)Mathf.Round(dmg*player.GetComponent<PlayerStats>().criticalMulti.GetValue()/100f);
						slot.GetComponent<EnemySlot>().TakeCritical();
						slot.GetComponent<EnemySlot>().TakeDamage(initialDmg,0);
					}
					else{
						slot.GetComponent<EnemySlot>().TakeDamage(dmg,0);
					}
					player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
					StartCoroutine(MultiAttacks(limit,dmg,percent,slot));
					panel.GetComponent<CardPanelController>().FinishCardUse();
					break;
				}
			}
		}
	}
	public int CountEnemies(){
		int count =0;
		for(int i =0; i < enemySlots.Count;i++){
			if(enemySlots[i].GetComponent<EnemySlot>().death == true){
				count +=1;
			}
		}
		return count;
	}
	public void BlockCards(bool isBlock){
		if(isBlock){
			for(int i =0; i < cardSlots.Count;i++){
				if(cardSlots[i].active){
					cardSlots[i].GetComponent<Button>().interactable = false;
				}
			}
		}
		else{
			for(int i =0; i < cardSlots.Count;i++){
				if(cardSlots[i].active){
					cardSlots[i].GetComponent<Button>().interactable = true;
				}
			}
			UpdateCardsNumbers();
		}
	}
	public void ActivePlayerStatus(int type,int val,int val2,int val3,int val4,int dur){
		PlayerStatus newStatus = new PlayerStatus(type,val,val2,val3,val4,dur);
		player.GetComponent<PlayerStats>().SetStatus(newStatus);
	}

	//inumerators
	IEnumerator EnemiesTurns(){
		for(int i =0; i < enemySlots.Count;i++){
			if(enemySlots[i].active){
				yield return new WaitForSeconds(1f);
    			switch ((int)enemySlots[i].GetComponent<EnemySlot>().currentAction.type){
		            case 0:
		            	SoundManager.instance.PlayEffect("Sword 2",false,false);
		            	enemySlots[i].GetComponent<EnemySlot>().Attack();
		            	int block = player.GetComponent<PlayerStats>().blockAmount;
		                player.GetComponent<PlayerStats>().Damage(enemySlots[i].GetComponent<EnemySlot>().currentAction.value);
		                if((block + player.GetComponent<PlayerStats>().GetStatValue("armor")) >= enemySlots[i].GetComponent<EnemySlot>().currentAction.value && player.GetComponent<PlayerStats>().counterDamage > 0){
		            		enemySlots[i].GetComponent<EnemySlot>().TakeDamage(player.GetComponent<PlayerStats>().counterDamage,0);
		            	}
		            break;

		            case 1:

		            break;

		            case 2:
		            	enemySlots[i].GetComponent<EnemySlot>().Heal(enemySlots[i].GetComponent<EnemySlot>().currentAction.value);
		            break;

		            case 3:
		            	enemySlots[i].GetComponent<EnemySlot>().Attack();
		            	SoundManager.instance.PlayEffect("Debuff",false,false);
		            	if(!PlayerStats.instance.doesPlayerDodge()){
		                	ActivePlayerStatus(1,enemySlots[i].GetComponent<EnemySlot>().currentAction.value,0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
		            	}
		            	else{
		            		PlayerStats.instance.dodgeAnim();
		            		ActivePlayerStatus(1,PlayerStats.instance.dodgeResult(enemySlots[i].GetComponent<EnemySlot>().currentAction.value),0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
		            	}
		            break;
		            case 4:
		            	enemySlots[i].GetComponent<EnemySlot>().Attack();
		            	SoundManager.instance.PlayEffect("Debuff",false,false);
						if(!PlayerStats.instance.doesPlayerDodge()){
		                	ActivePlayerStatus(0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value,0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
		            	}
		            	else{
		            		PlayerStats.instance.dodgeAnim();
		            		ActivePlayerStatus(0,PlayerStats.instance.dodgeResult(enemySlots[i].GetComponent<EnemySlot>().currentAction.value),0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
		            	}	            
		            break;
		            case 5:
		            	switch ((int)enemySlots[i].GetComponent<EnemySlot>().currentAction.type_aux){
		                    case 0:
		                    	SoundManager.instance.PlayEffect("Sword 2",false,false);
		                    	enemySlots[i].GetComponent<EnemySlot>().Attack();
		                        int block2 = player.GetComponent<PlayerStats>().blockAmount;
				                player.GetComponent<PlayerStats>().Damage(enemySlots[i].GetComponent<EnemySlot>().currentAction.value);
				                if((block2 + player.GetComponent<PlayerStats>().GetStatValue("armor")) >= enemySlots[i].GetComponent<EnemySlot>().currentAction.value && player.GetComponent<PlayerStats>().counterDamage > 0){
				            		enemySlots[i].GetComponent<EnemySlot>().TakeDamage(player.GetComponent<PlayerStats>().counterDamage,0);
				            	}
		                    break;
		                    case 1:

		                    break;

		                    case 2:
		                    	enemySlots[i].GetComponent<EnemySlot>().Heal(enemySlots[i].GetComponent<EnemySlot>().currentAction.value);
		                    break;

		                    case 3:
				            	enemySlots[i].GetComponent<EnemySlot>().Attack();
				            	SoundManager.instance.PlayEffect("Debuff",false,false);
				            	if(!PlayerStats.instance.doesPlayerDodge()){
				                	ActivePlayerStatus(1,enemySlots[i].GetComponent<EnemySlot>().currentAction.value,0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
				            	}
				            	else{
				            		PlayerStats.instance.dodgeAnim();
				            		ActivePlayerStatus(1,PlayerStats.instance.dodgeResult(enemySlots[i].GetComponent<EnemySlot>().currentAction.value),0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
				            	}
				            break;
				            case 4:
				            	enemySlots[i].GetComponent<EnemySlot>().Attack();
				            	SoundManager.instance.PlayEffect("Debuff",false,false);
								if(!PlayerStats.instance.doesPlayerDodge()){
				                	ActivePlayerStatus(0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value,0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
				            	}
				            	else{
				            		PlayerStats.instance.dodgeAnim();
				            		ActivePlayerStatus(0,PlayerStats.instance.dodgeResult(enemySlots[i].GetComponent<EnemySlot>().currentAction.value),0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2);
				            	}	            
				            break;
		                }
		                yield return new WaitForSeconds(1f);
		                switch ((int)enemySlots[i].GetComponent<EnemySlot>().currentAction.type_aux_2){
		                    case 0:
		                    	SoundManager.instance.PlayEffect("Sword 2",false,false);
		                    	enemySlots[i].GetComponent<EnemySlot>().Attack();
		                        int block3 = player.GetComponent<PlayerStats>().blockAmount;
				                player.GetComponent<PlayerStats>().Damage(enemySlots[i].GetComponent<EnemySlot>().currentAction.value_2);
				                if((block3 + player.GetComponent<PlayerStats>().GetStatValue("armor")) >= enemySlots[i].GetComponent<EnemySlot>().currentAction.value_2 && player.GetComponent<PlayerStats>().counterDamage > 0){
				            		enemySlots[i].GetComponent<EnemySlot>().TakeDamage(player.GetComponent<PlayerStats>().counterDamage,0);
				            	}
		                    break;
		                    case 1:

		                    break;

		                    case 2:
		                    	enemySlots[i].GetComponent<EnemySlot>().Heal(enemySlots[i].GetComponent<EnemySlot>().currentAction.value_2);
		                    break;
		                    case 3:
				            	enemySlots[i].GetComponent<EnemySlot>().Attack();
				            	SoundManager.instance.PlayEffect("Debuff",false,false);
				            	if(!PlayerStats.instance.doesPlayerDodge()){
				                	ActivePlayerStatus(1,enemySlots[i].GetComponent<EnemySlot>().currentAction.value_2,0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2_2);
				            	}
				            	else{
				            		PlayerStats.instance.dodgeAnim();
				            		ActivePlayerStatus(1,PlayerStats.instance.dodgeResult(enemySlots[i].GetComponent<EnemySlot>().currentAction.value_2),0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2_2);
				            	}
				            break;
				            case 4:
				            	enemySlots[i].GetComponent<EnemySlot>().Attack();
				            	SoundManager.instance.PlayEffect("Debuff",false,false);
								if(!PlayerStats.instance.doesPlayerDodge()){
				                	ActivePlayerStatus(0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value_2,0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2_2);
				            	}
				            	else{
				            		PlayerStats.instance.dodgeAnim();
				            		ActivePlayerStatus(0,PlayerStats.instance.dodgeResult(enemySlots[i].GetComponent<EnemySlot>().currentAction.value_2),0,0,0,enemySlots[i].GetComponent<EnemySlot>().currentAction.value2_2);
				            	}	            
				            break;
		                }

		            break;
		            case 6:

		            break;

		        }
		        enemySlots[i].GetComponent<EnemySlot>().EnemyTurn();
			}
		}
		yield return new WaitForSeconds(1f);
		StartPlayerTurn();
    }

    IEnumerator EndOfBattle(){
    	yield return new WaitForSeconds(1.5f);
    	for(int i =0; i < cardSlots.Count;i++){
			if(cardSlots[i].active){
				cardSlots[i].GetComponent<CardSlot>().ResetPos();
				cardSlots[i].GetComponent<CardSlot>().touching = false;
				cardSlots[i].GetComponent<CardSlot>().cardPanelActive = false;
				activeDesk.AddCard(cardSlots[i].GetComponent<CardSlot>().GetCard());
				cardSlots[i].GetComponent<CardSlot>().UnsetCard();
				cardSlots[i].SetActive(false);
			}
		}
		List<Card> discardcards = discardDesk.GetCards();
		foreach(Card card in discardcards){
			activeDesk.AddCard(card);
		}
		for(int i =0; i < enemySlots.Count;i++){
			enemySlots[i].GetComponent<EnemySlot>().death = false;
		}
		discardDesk.RemoveAllCard();
		discardcards = discardDesk.GetCards();
		List<Card> activecards = activeDesk.GetCards();
		deskCount.text = activecards.Count.ToString();
		discardCount.text = discardcards.Count.ToString();
        healthSlider.value = player.GetComponent<PlayerStats>().currentHealth;
        currentHealthNumber.text = player.GetComponent<PlayerStats>().currentHealth.ToString();
        earnedExp += currentExp;
        earnedGold += currentGold;
        currentExp = 0;
        currentGold = 0;
        dropPercent = 0;
        cardsPaused = false;
        expNumber.text = earnedExp.ToString();
        goldNumber.text = earnedGold.ToString();
        player.GetComponent<PlayerStats>().RemoveAllStatus();
        player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
        if(onFinalBattle == true){
        	gameObject.GetComponent<DataController>().OpenRewardPanel("win");
        }
		actionsPanel.SetActive(false);	
    }
    IEnumerator MultiAttacks(int limit,int dmg,int percent,GameObject slot){
    	int count = limit-1;
    	int rnd = Random.Range(1,101);
    	while(count > 0 && rnd <= percent){
    		yield return new WaitForSeconds(0.5f);
    		if(slot.active){ 
    			int effectIndex = Random.Range(1,4);
				SoundManager.instance.PlayEffect("Sword "+effectIndex,false,false);
    			if(player.GetComponent<PlayerStats>().criticalChance.GetValue() > Random.Range(0,100)){
					int currentDmg = (int)Mathf.Round(dmg*player.GetComponent<PlayerStats>().criticalMulti.GetValue()/100f);
					slot.GetComponent<EnemySlot>().TakeCritical();
					slot.GetComponent<EnemySlot>().TakeDamage(currentDmg,0);
				}
				else{
					slot.GetComponent<EnemySlot>().TakeDamage(dmg,0);
				}
    		}    		
			player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
			count -= 1;
			rnd = Random.Range(1,101);
    	}
    }
    IEnumerator AttackAll(int dmg){
    	foreach(GameObject slot in enemySlots){
			if(slot.active && !slot.GetComponent<EnemySlot>().death){
				if(player.GetComponent<PlayerStats>().criticalChance.GetValue() > Random.Range(0,100)){
					int currentDmg = (int)Mathf.Round(dmg*player.GetComponent<PlayerStats>().criticalMulti.GetValue()/100f);
					slot.GetComponent<EnemySlot>().TakeCritical();
					slot.GetComponent<EnemySlot>().TakeDamage(currentDmg,0);
				}
				else{
					slot.GetComponent<EnemySlot>().TakeDamage(dmg,0);
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
		player.GetComponent<PlayerStats>().RemoveOneTimeDamageMulti();
    }
    IEnumerator SendRewardAlerts(int exp,int gold, string itemName){
    	yield return new WaitForSeconds(0.3f);
    	if(exp > 0){
    		expRewardAlert.SetActive(true);
    		expRewardAlert.GetComponent<RewardAlertController>().SendAlert("+"+exp.ToString()+" exp","exp");
			yield return new WaitForSeconds(0.12f);
    	}
    	if(gold >0){
    		goldRewardAlert.SetActive(true);
    		goldRewardAlert.GetComponent<RewardAlertController>().SendAlert("+"+gold+" gold","gold");
			yield return new WaitForSeconds(0.12f);
    	}
		if(itemName != ""){
			itemRewardAlert.SetActive(true);
			itemRewardAlert.GetComponent<RewardAlertController>().SendAlert("+1 "+itemName,"item");
			yield return new WaitForSeconds(0.12f);
		}
    	
    }
    IEnumerator CardsPause(float time){ 
    	cardsPaused = true;
		yield return new WaitForSeconds(time);
		cardsPaused = false;
    }
}
