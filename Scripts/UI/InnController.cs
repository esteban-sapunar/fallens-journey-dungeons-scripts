using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InnController : MonoBehaviour
{
    // Values
    public List <InnSlot> innSlots = new List<InnSlot>();
	public List <Mission> missions = new List<Mission>();
	public List <NPC> npcs = new List<NPC>();
	public List <Item> rewardItems = new List<Item>();
	public List <Item> objectiveItems = new List<Item>();
	public GameObject missionPanel;

	//Singleton
	public static InnController instance;

	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of Inn Controller found!");
			return;
		}
		instance = this;
	}

    void Start()
    {
        SetMissions();
    }

    public void Closepanel(){
    	missionPanel.GetComponent<MissionPanelController>().ResetPanel();
    	gameObject.SetActive(false);
    }

    public void SetMissions(){
    	missions.Add(new Mission(
        	1,
        	MissionType.Stage,
        	"The Forest",
        	npcs[0],
        	"Greetings young man. It's been a while since someone visited the forest around the dungeon. If you really want to explore our catacombs, you should start by exploring the forest. (Finish the Road I stage)",
        	0,
            "1",
        	null,
        	null,
        	30,
        	rewardItems[0],
        	2,
        	30
        ));
        missions.Add(new Mission(
        	2,
        	MissionType.Item,
        	"Spider Fangs",
        	npcs[1],
        	"Greetings lad. I heard you're adventuring to the woods around the dungeon. I'm in the need of crafting some poison for my arrows. If you bring me one spider fang, I'll pay you and even give you two pieces of leather.",
        	1,
            "",
        	objectiveItems[0],
        	null,
        	20,
        	rewardItems[0],
        	2,
        	50
        ));
        missions.Add(new Mission(
        	3,
        	MissionType.Item,
        	"Feathers",
        	npcs[1],
        	"Hey lad. This time I'm in the need of crafting a long arrow. If you bring me one giant crow feather, I'll pay you and also give you my old hunting knife.",
        	1,
            "",
        	objectiveItems[1],
        	null,
        	80,
        	rewardItems[1],
        	1,
        	120
        ));
        missions.Add(new Mission(
        	4,
        	MissionType.Stage,
        	"Wolves",
        	npcs[0],
        	"Greetings my friend. I got reports of fierce wolves deeper in the woods. As you are eager to explore, could you look around the area? (Finish the Road II stage)",
        	0,
            "2",
        	null,
        	null,
        	100,
        	rewardItems[16],
        	1,
        	100
        ));
        missions.Add(new Mission(
        	5,
        	MissionType.Stage,
        	"The Bandits",
        	npcs[0],
        	"My friend. If you want to reach the dungeon, you'll have to fight your way through the bandits ruling the forest. You should start by spying their outer camps. (Finish the Road IV stage)",
        	0,
            "3",
        	null,
        	null,
        	180,
        	rewardItems[17],
        	1,
        	180
        ));
        missions.Add(new Mission(
            6,
            MissionType.Level,
            "Training",
            npcs[2],
            "Are you the one looking for adventures? What a joke. You look like a beggar to me. But the mayor asked me to train you... Well, I'll give you the chance to proof me that there is something useful in you. Try to reach level 5 and come back to me.",
            5,
            "",
            null,
            null,
            500,
            null,
            0,
            0,
            20
        ));
        missions.Add(new Mission(
            7,
            MissionType.Level,
            "Training To Live",
            npcs[2],
            "Well boy, it looks like there is some hope for you. Hear me lad.. if you want to survive in that dungeon full of bandits, you've to be stronger than them. Now reach level 10.",
            10,
            "",
            null,
            null,
            1200,
            null,
            0,
            0,
            40
        ));
        missions.Add(new Mission(
            8,
            MissionType.Item,
            "Wolf Fur",
            npcs[1],
            "How are you today lad? A costumer asked me for a piece of wolf fur, Bring me one and I'll pay you and also give you some fabric I took from a death bandit.",
            1,
            "",
            objectiveItems[2],
            null,
            120,
            rewardItems[17],
            1,
            250
        ));
        missions.Add(new Mission(
            9,
            MissionType.Stage,
            "The Spearman",
            npcs[0],
            "Greetings my friend. I'm glad to know about your achievements against the bandits... But I heard of a strong bandit with a spear defending one of the entrance to the dungeons. You'll have to defeat him. (Finish the Road V stage)",
            0,
            "4",
            null,
            null,
            800,
            rewardItems[2],
            4,
            500
        ));
        missions.Add(new Mission(
            10,
            MissionType.Level,
            "Never Stop Training",
            npcs[2],
            "You're looking more like a warrior lad. But you need to improve even more. Reach level 15.",
            15,
            "",
            null,
            null,
            3200,
            null,
            0,
            0,
            60
        ));
        missions.Add(new Mission(
            11,
            MissionType.Level,
            "Road To Strength",
            npcs[2],
            "Good work lad. I bet those bandits must be having nightmares about you. Now reach level 20.",
            20,
            "",
            null,
            null,
            7000,
            null,
            0,
            0,
            80
        ));
        missions.Add(new Mission(
            12,
            MissionType.Stage,
            "The Captain",
            npcs[0],
            "Well done my friend! You defeated that spearman. But I received a new report... It seems that in a camp next to the entrance there is a strong bandit captain. If you manage to defeat him, it would be a great blow to them. (Finish the Road VI stage)",
            0,
            "end2",
            null,
            null,
            6000,
            rewardItems[4],
            1,
            2000
        ));
        missions.Add(new Mission(
            13,
            MissionType.Level,
            "Never Back Down",
            npcs[2],
            "Well lad, you really look like a warrior now, but you're still a rookie. Now reach level 25.",
            25,
            "",
            null,
            null,
            10000,
            null,
            0,
            0,
            100
        ));
        missions.Add(new Mission(
            14,
            MissionType.Item,
            "Bat Wings",
            npcs[1],
            "Good day lad. I have been ask for a few of bat wings, Bring me two and I'll pay you and also give you a nice pair of light boots.",
            2,
            "",
            objectiveItems[3],
            null,
            800,
            rewardItems[5],
            1,
            1200
        ));
        missions.Add(new Mission(
            15,
            MissionType.Item,
            "Spider Head",
            npcs[1],
            "I heard that you have reached the dungeon. Maybe you would be able to bring me a nice trophy, Bring me one red spider head and I'll pay you and also give you a nice heavy dagger.",
            1,
            "",
            objectiveItems[4],
            null,
            1000,
            rewardItems[6],
            1,
            1400
        ));
        missions.Add(new Mission(
            16,
            MissionType.Item,
            "Shoes",
            npcs[3],
            "Greetings newcomer. If you are looking for basic gear for your adventures, you should pass by my shop. By the way I may have a job for you. It would be great if you bring me a new pair of shoes. Of course I will pay you and even give you a nice bread.",
            1,
            "",
            objectiveItems[5],
            null,
            50,
            rewardItems[7],
            1,
            100
        ));
        missions.Add(new Mission(
            17,
            MissionType.Item,
            "Stolen Fabric",
            npcs[3],
            "My Friend! Would you believe that a thief stole a nice piece of fabric from my shop?. If you manage to catch him and bring my fabric back, I will pay you a good reward.",
            1,
            "",
            objectiveItems[6],
            null,
            100,
            null,
            1,
            200
        ));
        missions.Add(new Mission(
            18,
            MissionType.Stage,
            "White Wolf",
            npcs[0],
            "Young man! You manage to kill many wolves. If you feel ready, you should hunt down the white leader of the wolves, I will pay you a nice reward. But be aware, he is much stronger. (Finish the Road III stage)",
            0,
            "end1",
            null,
            null,
            850,
            rewardItems[8],
            2,
            450
        ));
        missions.Add(new Mission(
            19,
            MissionType.Item,
            "Steal Their Clothes",
            npcs[3],
            "I must confess I'm still sulky about the fabric robbery. I want to make things even, would you steal something from them?... Maybe a pair of pants! I'll pay you and also give you pieces of fabric from the pants.",
            1,
            "",
            objectiveItems[7],
            null,
            200,
            rewardItems[2],
            4,
            400
        ));
        missions.Add(new Mission(
            20,
            MissionType.Level,
            "Toughness",
            npcs[2],
            "Nice job! Now reach level 30.",
            30,
            "",
            null,
            null,
            18000,
            null,
            0,
            0,
            120
        ));
        missions.Add(new Mission(
            21,
            MissionType.Stage,
            "Enter The Dungeon",
            npcs[0],
            "You are the first one able to reach the entrance of the dungeon in years. Those bandits should be losing composure. Now take a look around the first floor. (Finish the Floor I stage)",
            0,
            "6",
            null,
            null,
            1600,
            rewardItems[9],
            1,
            1000
        ));
        missions.Add(new Mission(
            22,
            MissionType.Level,
            "Advanced Warrior",
            npcs[2],
            "Well my friend you're quite the fighter now. Keep going and reach level 35.",
            35,
            "",
            null,
            null,
            30000,
            null,
            0,
            0,
            150
        ));
        missions.Add(new Mission(
            23,
            MissionType.Item,
            "Gambeson",
            npcs[4],
            "Greetings! It was a nice hat that you gave me. I have more work for you, bring me a new gambeson and I'll pay you with some iron.",
            1,
            "",
            objectiveItems[8],
            null,
            200,
            rewardItems[10],
            1,
            400
        ));
        missions.Add(new Mission(
            24 ,
            MissionType.Item,
            "Padded Coif",
            npcs[4],
            "Good job with that gambeson. If you want more work, I'm in the need of a new padded coif. I'll pay you of course.",
            1,
            "",
            objectiveItems[9],
            null,
            250,
            rewardItems[10],
            1,
            450
        ));
        missions.Add(new Mission(
            25 ,
            MissionType.Item,
            "Reiforced Boots",
            npcs[4],
            "Greetings Friend. If you can manage, I'm in the need of a pair of Reiforced Boots.",
            1,
            "",
            objectiveItems[10],
            null,
            350,
            rewardItems[10],
            1,
            600
        ));
        missions.Add(new Mission(
            26 ,
            MissionType.Item,
            "Wolf Helmet",
            npcs[4],
            "My Friend. One of my customers asked for a wolf helmet. Craft me one and I will pay you with a black dye pot.",
            1,
            "",
            objectiveItems[11],
            null,
            350,
            rewardItems[12],
            1,
            550
        ));
        missions.Add(new Mission(
            27 ,
            MissionType.Item,
            "Heavy Dagger",
            npcs[3],
            "Good day! I have a job for you! A customer ask me for a heavy dagger. I don't have of those on my shop, but if you bring me one, I can pay you with some black dye.",
            1,
            "",
            objectiveItems[12],
            null,
            200,
            rewardItems[11],
            2,
            650
        ));
        missions.Add(new Mission(
            28 ,
            MissionType.Item,
            "Hunter Tunic",
            npcs[4],
            "My Friend. One of my customers asked for a hunter tunic. Craft me one and I will pay you with a big iron ore.",
            1,
            "",
            objectiveItems[13],
            null,
            400,
            rewardItems[14],
            1,
            600
        ));
        missions.Add(new Mission(
            29 ,
            MissionType.Item,
            "Hunter Pants",
            npcs[4],
            "My Friend. One of my customers asked for a pair of hunter pants. Craft me one and I will pay you with a big coal ore.",
            1,
            "",
            objectiveItems[14],
            null,
            350,
            rewardItems[13],
            1,
            550
        ));
        missions.Add(new Mission(
            30,
            MissionType.Stage,
            "Cave Worms",
            npcs[0],
            "There're old records of huge worms living in the catacombs, take a look around. This creatures may carry valuable minerals inside. (Finish the Floor Extra I stage)",
            0,
            "8",
            null,
            null,
            2000,
            rewardItems[10],
            1,
            1200
        ));
        missions.Add(new Mission(
            31,
            MissionType.Stage,
            "Twin Heads",
            npcs[0],
            "I have been reading some records and it seems to exist a big worm with two heads deeper inside the dungeon. It would be great to hunt it down. (Finish the Floor Extra II stage)",
            0,
            "10",
            null,
            null,
            7000,
            rewardItems[15],
            1,
            3000
        ));
        missions.Add(new Mission(
            32,
            MissionType.Stage,
            "The Mother",
            npcs[0],
            "It seems that deep inside the worm's area, the biggest worm of all is waiting for prays. It's known in the records as The Mother. If you defeat that monster you'll be one step closer to conquer the dungeon. (Finish the Mother's stage)",
            0,
            "end3",
            null,
            null,
            15000,
            rewardItems[10],
            3,
            5000
        ));
        missions.Add(new Mission(
            33,
            MissionType.Level,
            "Seasoned Warrior",
            npcs[2],
            "Well my friend you're a strong fighter now. Keep going and reach level 40.",
            40,
            "",
            null,
            null,
            40000,
            null,
            0,
            0,
            200
        ));
        missions.Add(new Mission(
            34,
            MissionType.Item,
            "A New Hat",
            npcs[4],
            "Greetings... So you're the new adventurer. I heard you have some crafting skills, Anna told me about the shoes. I may have work for you, bring me a new peasant hat and I'll pay you with some fabric.",
            1,
            "",
            objectiveItems[15],
            null,
            100,
            rewardItems[2],
            2,
            150
        ));


        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[0].id.ToString())){
        	innSlots[0].SetMission(missions[0]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[1].id.ToString())){
        	innSlots[1].SetMission(missions[1]);
    	}
    	if(!TownPlayerStats.instance.arrayMissions.Contains(missions[2].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[1].id.ToString())){
        	innSlots[2].SetMission(missions[2]);
    	}
    	if(!TownPlayerStats.instance.arrayMissions.Contains(missions[3].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[0].id.ToString())){
        	innSlots[3].SetMission(missions[3]);
    	}
    	if(!TownPlayerStats.instance.arrayMissions.Contains(missions[4].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[3].id.ToString())){
        	innSlots[4].SetMission(missions[4]);
    	}
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[5].id.ToString())){
            innSlots[5].SetMission(missions[5]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[6].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[5].id.ToString())){
            innSlots[6].SetMission(missions[6]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[7].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[2].id.ToString())){
            innSlots[7].SetMission(missions[7]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[8].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[4].id.ToString())){
            innSlots[8].SetMission(missions[8]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[9].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[6].id.ToString())){
            innSlots[9].SetMission(missions[9]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[10].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[9].id.ToString())){
            innSlots[10].SetMission(missions[10]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[11].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[8].id.ToString())){
            innSlots[11].SetMission(missions[11]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[12].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[10].id.ToString())){
            innSlots[12].SetMission(missions[12]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[13].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[7].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[8].id.ToString())){
            innSlots[13].SetMission(missions[13]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[14].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[7].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[8].id.ToString())){
            innSlots[14].SetMission(missions[14]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[15].id.ToString())){
            innSlots[15].SetMission(missions[15]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[16].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[15].id.ToString())){
            innSlots[16].SetMission(missions[16]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[17].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[3].id.ToString())){
            innSlots[17].SetMission(missions[17]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[18].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[16].id.ToString())){
            innSlots[18].SetMission(missions[18]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[19].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[12].id.ToString())){
            innSlots[19].SetMission(missions[19]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[20].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[8].id.ToString())){
            innSlots[20].SetMission(missions[20]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[21].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[19].id.ToString())){
            innSlots[21].SetMission(missions[21]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[22].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[33].id.ToString())){
            innSlots[22].SetMission(missions[22]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[23].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[22].id.ToString())){
            innSlots[23].SetMission(missions[23]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[24].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[23].id.ToString())){
            innSlots[24].SetMission(missions[24]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[25].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[22].id.ToString())){
            innSlots[25].SetMission(missions[25]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[26].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[18].id.ToString())){
            innSlots[26].SetMission(missions[26]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[27].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[22].id.ToString())){
            innSlots[27].SetMission(missions[27]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[28].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[22].id.ToString())){
            innSlots[28].SetMission(missions[28]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[29].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[20].id.ToString())){
            innSlots[29].SetMission(missions[29]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[30].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[29].id.ToString())){
            innSlots[30].SetMission(missions[30]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[31].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[30].id.ToString())){
            innSlots[31].SetMission(missions[31]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[32].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[21].id.ToString())){
            innSlots[32].SetMission(missions[32]);
        }
        if(!TownPlayerStats.instance.arrayMissions.Contains(missions[33].id.ToString()) && TownPlayerStats.instance.arrayMissions.Contains(missions[15].id.ToString())){
            innSlots[33].SetMission(missions[33]);
        }
    }

}
