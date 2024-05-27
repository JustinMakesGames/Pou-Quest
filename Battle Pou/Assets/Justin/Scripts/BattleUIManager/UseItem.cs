using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseItem : MonoBehaviour
{
    private ItemInfo itemInfo;

    private void Start()
    {
        itemInfo = GetComponent<ItemInfo>();
    }
    public void UsingItem()
    {
        PlayerHandler.Instance.hp += itemInfo.hpPlus;
        PlayerHandler.Instance.sp += itemInfo.spPlus;
        
        if (PlayerHandler.Instance.hp > PlayerHandler.Instance.maxHp)
        {
            PlayerHandler.Instance.hp = PlayerHandler.Instance.maxHp;
        }

        if (PlayerHandler.Instance.sp > PlayerHandler.Instance.maxSp)
        {
            PlayerHandler.Instance.sp = PlayerHandler.Instance.maxSp;
        }

        BattleUI.instance.StatsChange();

        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }

    public void SetActiveFalse()
    {
        transform.parent.parent.gameObject.SetActive(false);
    }

    public void SubstractItemCount()
    {
        itemInfo.count--;
        transform.GetChild(1).GetComponent<TMP_Text>().text = itemInfo.count.ToString();

        if (itemInfo.count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
