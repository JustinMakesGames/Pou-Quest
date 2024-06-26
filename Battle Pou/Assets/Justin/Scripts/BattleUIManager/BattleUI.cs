using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public static BattleUI instance;
    private PlayerHandler playerHandler;
    public Transform player;
    public List<Transform> attacks;
    public List<TMP_Text> attackTexts = new List<TMP_Text>(3);
    public List<TMP_Text> spCostTexts = new List<TMP_Text>(3);
    public Transform itemList;
    public List<Item> items;
    public GameObject itemButton;

    public GameObject chooseAttack;
    public ItemInfo itemInfo;
    public ItemInfo inventoryItemInfo;

    public TMP_Text playerHPText, playerSPText;

    public Slider playerHPSlider, playerSPSlider, enemyHPSlider;
    public Slider turnSlider;
    public TMP_Text turnText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerHandler = PlayerHandler.Instance;
        player = FindObjectOfType<BattlePlayerMovement>().transform;

        
    }

    private void Start()
    {
        SetUpPlayerAttacks();
        SetUpItems();
        StartCoroutine(SetUpAttacks());

        StartCoroutine(StartShowingStats());
        
    }

    private IEnumerator StartShowingStats()
    {
        yield return null;
        StatsChange();
    }
    private IEnumerator SetUpAttacks()
    {
        yield return null;
        yield return null;
        for (int i = 0; i < player.childCount; i++)
        {
            attacks.Add(player.GetChild(i));
        }
    }

    private void SetUpPlayerAttacks()
    {
        for (int i = 0; i < attackTexts.Count; i++)
        {
            attackTexts[i].text = PlayerHandler.Instance.attacks[i].name;
            spCostTexts[i].text = PlayerHandler.Instance.attacks[i].GetComponent<Attacking>().attackStats.spCost.ToString();
        }
    }

    private void SetUpItems()
    {
       
        Transform overworldInventory = GameObject.FindGameObjectWithTag("Inventory").transform;
        ItemInfo[] scripts = overworldInventory.GetComponentsInChildren<ItemInfo>(true);
        for(int i = 0; i < scripts.Length; i++)
        {

            GameObject itemClone = Instantiate(itemButton, itemList);
            itemInfo = itemClone.GetComponentInChildren<ItemInfo>();
            inventoryItemInfo = scripts[i];
            itemInfo.item = inventoryItemInfo.item;
            itemInfo.itemName = inventoryItemInfo.itemName;
            itemInfo.hpPlus = inventoryItemInfo.hpPlus;
            itemInfo.spPlus = inventoryItemInfo.spPlus;
            itemInfo.count = inventoryItemInfo.count;
            TMP_Text itemName = itemClone.transform.GetChild(0).GetComponent<TMP_Text>();
            TMP_Text itemCount = itemClone.transform.GetChild(1).GetComponent<TMP_Text>();

            itemName.text = itemInfo.itemName;
            itemCount.text = "x" + itemInfo.count.ToString();

            


        }

    }
         
    public void Attack1()
    {
        
        if (attacks[0].GetComponent<Attacking>().attackStats.spCost > playerHandler.sp)
        {
            return;
        }

        chooseAttack.SetActive(false);
        playerHandler.sp -= attacks[0].GetComponent<Attacking>().attackStats.spCost;
        StatsChange();
        BattleManager.instance.playerAttack = attacks[0];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }

    public void Attack2()
    {
        if (attacks[1].GetComponent<Attacking>().attackStats.spCost > playerHandler.sp)
        {
            return;
        }
        chooseAttack.SetActive(false);
        playerHandler.sp -= attacks[1].GetComponent<Attacking>().attackStats.spCost;
        StatsChange();
        BattleManager.instance.playerAttack = attacks[1];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }

    public void Attack3()
    {
        if (attacks[2].GetComponent<Attacking>().attackStats.spCost > playerHandler.sp)
        {
            return;
        }

        chooseAttack.SetActive(false);
        playerHandler.sp -= attacks[2].GetComponent<Attacking>().attackStats.spCost;
        StatsChange();
        BattleManager.instance.playerAttack = attacks[2];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }
    
    public void Flee()
    {
        //int chanceToEscape = Random.Range(0,3);
        int chanceToEscape = 0;
        if (chanceToEscape == 0)
        {
            BattleManager.instance.HandlingStates(BattleState.Flee);

        }
        else
        {
            BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HUB()
    {
        print("Go to hub");
    }

    public void StatsChange()
    {
        playerHPText.text = playerHandler.hp.ToString() + "/" + playerHandler.maxHp.ToString();
        playerSPText.text = playerHandler.sp.ToString() + "/" + playerHandler.maxSp.ToString();

        playerHPSlider.maxValue = playerHandler.maxHp;
        playerHPSlider.value = playerHandler.hp;
        playerSPSlider.maxValue = playerHandler.maxSp;
        playerSPSlider.value = playerHandler.sp;

        enemyHPSlider.maxValue = FindObjectOfType<EnemyHandler>().maxHp;
        enemyHPSlider.value = FindObjectOfType<EnemyHandler>().hp;
    }

    public IEnumerator ShowTurnLength(float length)
    {
        turnSlider.gameObject.SetActive(true);
        while (length > 0)
        {
            length -= Time.deltaTime;
            turnSlider.value = length;
            turnText.text = length.ToString("0");
            yield return null;

        }

        turnSlider.gameObject.SetActive(false);
    }
}
