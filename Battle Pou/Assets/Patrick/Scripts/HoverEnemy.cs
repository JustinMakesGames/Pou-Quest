using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverEnemy : MonoBehaviour
{
    public GameObject panel;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerHandler.Instance.transform.position, PlayerHandler.Instance.transform.forward, out hit, 5f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                int level, health, sp, attack;
                EnemyInfo enemyInfo = hit.collider.GetComponent<EnemyInfo>();
                level = enemyInfo.level;
                health = enemyInfo.health;
                sp = enemyInfo.sp;
                attack = enemyInfo.attack;
                GetComponentInChildren<TMP_Text>().text = "Level: " + level.ToString() + "/nHealth: " + health.ToString() + "/nSpell Power: " + sp + "/nAttack: " + attack.ToString();
            }
        }   
    }
}
