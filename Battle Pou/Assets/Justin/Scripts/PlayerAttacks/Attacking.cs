using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacking : MonoBehaviour
{
    public AttackStats attackStats;
    public Animator animator;

    public bool isBought;

    public abstract void StartAttack();
    public abstract void UpdateAttack();
    public abstract void FinishAttack();
}
