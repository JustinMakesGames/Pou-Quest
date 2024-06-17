using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttacks", menuName = "PlayerAttack")]
public class AttackStats : ScriptableObject
{
    public int id;
    public int spCost;
    public int attackPower;
    public float projectileSpeed;
    public float attackInterval;
    public AudioClip sound;
    

}
