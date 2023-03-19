using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBattleSystem : MonoBehaviour
{
    public NewBattleSystem instance;
    //Class holding enemy prefabs and info
    public EnemyCollection enemyCollection;

    //TEMP enum containing enemy types
    public enum KindOfEnemy { SKELETON, VIKING, RUNECOLOSSUS, BANDIT };

    //Player and enemy prefabs and unit data
    public GameObject playerPrefab;
    Unit playerUnit;

    //Player and enemy positions
    public Transform playerPosition;
    public Transform[] enemyPosition;

    //Dictionary containing currently active enemies at each position
    public Dictionary<Transform, GameObject> activeEnemies;

    //list of all enemies in battle queue by type(non-unique)
    public static List<KindOfEnemy> enemyList = new List<KindOfEnemy>();

    //max number of runes in player's hand
    public int handSize;

    //list of runes in deck
    private List<GameObject> playerDeck = new List<GameObject>();

    //place where runes will spawn
    public Transform runePlace;

    //number in deck
    private int deckNr = 0;

    public Text deckNrText;
    public Text graveyardText;

    //used to hold armor and power
    public EquipmentInventoryObject equippedinventorystorage;               //used to get values of equipped items
    public EquipmentInventoryObject inventorystorage;                   //not needed later: only useful for reseting items when testing

    //exp depends on number of enemies
    private int initialNumOfEnemies = 1;

    void Start()
    {
        //reset logs
        PlayerLog.resetLog();

        //spawn player
        GameObject playerObject = Instantiate(playerPrefab, playerPosition);
        playerUnit = playerObject.GetComponent<Unit>();
        //finds a player and gets unit
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();

        //retrieve player's deck from GlobalRune
        playerDeck = GlobalRune.Instance.getInvAsList();

        //randomize cards in deck
        randomizeCards();
        UpdateTextRunesInGraveyard();

        //get armor and power, and then set it for hero
        playerUnit.armor = calcArmor();
        int pow = calcPower();

        //initialize active enemy dictionary
        activeEnemies = new Dictionary<Transform, GameObject>();

        initialNumOfEnemies = activeEnemies.Count;
        Debug.Log("initial num of enemies: " + initialNumOfEnemies);
        Debug.Log("static enemyList in NBS size: " + enemyList.Count);
        //spawn first enemy and add to activeEnemies dictionary
        activeEnemies.Add(enemyPosition[0], Instantiate(enemyCollection.getEnemyPrefab(enemyList[0]), enemyPosition[0]));
        //remove this enemy from queue
        enemyList.RemoveAt(0);
        //repeat up to 3x if there are >=3 enemies in enemyList
        if (enemyList.Count > 0)
        {
            activeEnemies.Add(enemyPosition[1], Instantiate(enemyCollection.getEnemyPrefab(enemyList[0]), enemyPosition[1]));
            enemyList.RemoveAt(0);
            if (enemyList.Count > 0)
            {
                activeEnemies.Add(enemyPosition[2], Instantiate(enemyCollection.getEnemyPrefab(enemyList[0]), enemyPosition[2]));
                enemyList.RemoveAt(0);
            }
        }

        // sets power of each unit (system is weird - if hero has power of "2", then the enemy must have it's power set to 2, NOT the hero)
        List<Transform> keys = new List<Transform>(activeEnemies.Keys);
        foreach (Transform t in keys)//for each enemy position
        {
            Unit enemy = activeEnemies[t].GetComponent<Unit>();
            enemy.power = pow;
        }


        playerTurn();
    }

    //call at the start of every playerTurn
    private void playerTurn()
    {
        //If health <= 0 or no enemies left, show either a victory screen or loss screen
        victoryScreen();
        lossScreen();

        PlayerLog.addEvent("nextturn");             //tells the player log that it is next turn

        playerUnit.GetComponent<TextBlink>().Blink($"Player takes {playerUnit.damageResistance * 100}% damage", 2);//shows dmg resistance
        spawnRunes();
        //shuffleDraw();
    }

    //call when player presses endTurnButton
    public void endTurn()
    {

        //If health <= 0 or no enemies left, show either a victory screen or loss screen
        victoryScreen();
        lossScreen();
        destroyHand();
        //call enemy turn
        StartCoroutine(enemyTurn());

        //SkillSystemManager.Instance.addExp(SkillSystemManager.SkillSet.FIGHTING, 15f); //adds 15xp per turn
        //SkillSystemManager.AddExpEvent(SkillSet.FIGHTING, 15f);
        SkillSystemManager.AddExpEvent.Invoke(SkillSet.FIGHTING, 15f);
    }

    //call at the start of enemyTurn
    IEnumerator enemyTurn()
    {
        int pow = calcPower();              //for hero's power

        playerUnit.execBuffs();//execute player buffs at the end of player turn(start of enemy turn)
        List<Transform> keys = new List<Transform>(activeEnemies.Keys);
        foreach (Transform t in keys)//for each enemy position
        {
            //if there is no enemy in this position
            if (activeEnemies[t].Equals(null))
            {
                //and there are enemies waiting in queue
                if (enemyList.Count != 0)
                {
                    activeEnemies.Remove(t);
                    activeEnemies.Add(t, Instantiate(enemyCollection.getEnemyPrefab(enemyList[0]), t));
                    enemyList.RemoveAt(0);
                }
                else
                {
                    activeEnemies.Remove(t);
                }
                
            }
            else
            {
                Unit enemy = activeEnemies[t].GetComponent<Unit>();

                enemy.power = pow;          //this is in case a new enemy appears (sets its "power" to appropriate amount)

                enemy.execBuffs();//debuffs before enemy attacks
                if (enemy.canAttack)
                {
                    enemy.GetComponent<TextBlink>().Blink("Attack", 2);//text "attack" shows up above the enemy
                    //enemy takes action
                    float actionDuration = enemy.takeAction(playerUnit);
                    yield return new WaitForSeconds(actionDuration);
                }
            }
        }
        //If health <= 0 or no enemies left, show either a victory screen or loss screen
        victoryScreen();
        lossScreen();
        //call at the end of enemyTurn
        playerTurn();
    }

    //get list of enemies encountered in overworld, called by overworld manager
    public static void enemyGoToEnum(List<GameObject> list)
    {
        enemyList = new List<KindOfEnemy>();
        Debug.Log("Enemy list in NBS of size: " + list.Count);
        foreach (GameObject go in list)
        {
            if (go.GetComponent<EnemyDescription>() != null)
            {
                //Debug.Log("Encountered enemy of type: " + go.GetComponent<EnemyDescription>().GetKindOfEnemy());
                enemyList.Add(go.GetComponent<EnemyDescription>().GetKindOfEnemy());
            }
        }
    }

    //randomize cards in deck method
    public void randomizeCards()
    {
        //   Debug.Log("deck radomized");
        for (int i = 0; i < playerDeck.Count; i++)
        {
            GameObject temp = playerDeck[i];
            int randomIndex = Random.Range(i, playerDeck.Count);
            playerDeck[i] = playerDeck[randomIndex];
            playerDeck[randomIndex] = temp;
        }
    }
    //destroy spawned cards(on end turn)
    public void destroyHand()
    {
        foreach (Transform child in runePlace)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    //spawn cards
    public void spawnRunes()
    {
        //    Debug.Log($"Spawned hand");
        UpdateTextRunesInGraveyard();
        for (int j = 0; j < handSize; j++)
        {
            var card = Instantiate(playerDeck[deckNr], runePlace); //creates a card in an empty UI gameobject
            UpdateTextRunesInDeck();
            if (deckNr < playerDeck.Count - 1)
            {
                deckNr++;
            }
            else
            {
                deckNr = 0;
                randomizeCards();
            }
        }
    }

    public void victoryScreen()
    {
        if (activeEnemies.Count == 0)
        {
            //open victory screen (through BattleSceneVictoryLossPanelScript)
            BattleSceneVictoryLossPanelScript.VictoryOrLoss = 1;

            //Give loot 
            SkillSystemManager.AddExpEvent.Invoke(SkillSet.FIGHTING, 10*initialNumOfEnemies);
        }
    }
    //special victoryScreen function, to be used in Unit script, where "this" is a Unit so activeEnemies.Count is always > 1
    public void victoryScreen(bool inUnit)
    {
        if (activeEnemies.Count == 1)
        {
            //open victory screen (through BattleSceneVictoryLossPanelScript)
            BattleSceneVictoryLossPanelScript.VictoryOrLoss = 1;
        }
    }
    public void lossScreen()
    {
        if (playerUnit.currentHP <= 0)
            BattleSceneVictoryLossPanelScript.VictoryOrLoss = 2;
    }


    public void UpdateTextRunesInDeck()//updates text how many runes are still in deck
    {
        int i = playerDeck.Count - deckNr - 1;
        deckNrText.text = $"{i} Runes in Deck";
    }
    public void UpdateTextRunesInGraveyard()//updates text how many runes are still in deck
    {
        graveyardText.text = $"{deckNr} Runes in Graveyard";
    }
    //kill enemy and replace it if there are enemies in queue
    public void replaceEnemy(GameObject killedEnemy)
    {
        foreach (Transform t in enemyPosition)
        {
            //if 
        }
    }
    public void addRuneToHand(GameObject rune)
    {
        Instantiate(rune, runePlace);
    }

    int calcArmor()
    {
        int calc = 0;               //for armor
        try
        {
            if (equippedinventorystorage.Container.Items.Count > 0)              //check if the inventory isn't empty
            {

                foreach (var itt in equippedinventorystorage.Container.Items)        //loop for every equipped item
                {
                    if (itt.item.type != EquipmentType.Amulet)        //add its power to the total
                        calc += itt.item.strength;
                }
            }

            return calc;   
        }
        catch { }
        return 0;
    }
    int calcPower()
    {
        int pow = 0;                //for amulet
        try
        {
            if (equippedinventorystorage.Container.Items.Count > 0)              //check if the inventory isn't empty
            {

                foreach (var itt in equippedinventorystorage.Container.Items)        //loop for every equipped item
                {
                    if (itt.item.type == EquipmentType.Amulet)
                    {
                        pow = itt.item.strength;
                        break;
                    }
                }
            }
            return pow;
        }
        catch { }
        return 0;
    }


    private void OnApplicationQuit()            //WARNING! Remove from final game - this resets the items in case we quit (So we don't clog up inventory)
    {
        equippedinventorystorage.Container.Items.Clear();
        inventorystorage.Container.Items.Clear();
    }
}