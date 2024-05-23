using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttacks", menuName = "PlayerAttack")]
public class AttackStats : ScriptableObject
{
    public int spCost;
    public int attackPower;
    public float projectileSpeed;
    public int attackInterval;
    

}
