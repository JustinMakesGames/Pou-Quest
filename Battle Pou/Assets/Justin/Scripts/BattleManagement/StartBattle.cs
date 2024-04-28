using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    public static StartBattle instance;
    public delegate void StartBattleDelegate();
    public event StartBattleDelegate startingBattle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }      

    }

    public void ActivateEvent()
    {
        startingBattle = null;
        startingBattle += Fading.instance.ActivateIEnumerator;
        startingBattle += CreateBattleArena.instance.DeactivateOverworldScripts;
        startingBattle += CreateBattleArena.instance.MakeBattleArena;

        startingBattle();
    }
}
