using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    private PlayerHandler playerHandler;
    public Transform player;
    public List<Transform> attacks;
    public List<TMP_Text> attackTexts = new List<TMP_Text>(3);
    public Transform itemList;
    public List<Item> items;
    public GameObject itemButton;

    public ItemInfo itemInfo;
    public ItemInfo inventoryItemInfo;
    private void Awake()
    {
        playerHandler = PlayerHandler.Instance;
        player = FindObjectOfType<BattlePlayerMovement>().transform;

        
    }

    private void Start()
    {
        SetUpPlayerAttacks();
        SetUpItems();
        StartCoroutine(SetUpAttacks());
        
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
        BattleManager.instance.playerAttack = attacks[0];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }

    public void Attack2()
    {
        BattleManager.instance.playerAttack = attacks[1];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }

    public void Attack3()
    {
        BattleManager.instance.playerAttack = attacks[2];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }
    
    public void Flee()
    {
        int chanceToEscape = Random.Range(0,3);

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
}
