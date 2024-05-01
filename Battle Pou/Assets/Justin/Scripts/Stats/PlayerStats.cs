using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public static PlayerHandler Instance;
    public int hp;
    public int maxHp;
    public int sp;
    public int maxSp;
    public int attackPower;
    public int exp;
    public int maxExp;

    public List<Transform> attacks = new List<Transform>(3);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    public void TakeDamage(int damage)
    {
        
        hp -= damage;
    }
}
