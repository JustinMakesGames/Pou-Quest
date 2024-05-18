using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackShop : MonoBehaviour
{
    public int price;
    public Transform attack;
    public string attackName;
    public string attackDescription;
    public GameObject attackButton;

    public GameObject attackButtonClone;
    public Transform notEquipped;
    public void BuyAttack()
    {
        if (PlayerHandler.Instance.coins < price) return;

        PlayerHandler.Instance.coins -= price;

        SetAttackInInventory();
        
    }

    private void SetAttackInInventory()
    {
        attackButtonClone = Instantiate(attackButton, notEquipped);
        AttackButton script = attackButtonClone.GetComponent<AttackButton>();

        attackButtonClone.GetComponentInChildren<TMP_Text>().text = attackName;

        script.attack = attack;
        script.attackName = attackName;
        script.attackDescription = attackDescription;
        

    }

    private void Epic()
    {
        
    }
}
