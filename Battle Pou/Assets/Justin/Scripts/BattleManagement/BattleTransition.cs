using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransition : MonoBehaviour
{
    public static BattleTransition instance;
    public delegate void StartBattleDelegate();
    public event StartBattleDelegate startingBattle;

    public delegate void EndBattleDelegate();
    public event EndBattleDelegate endBattle;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        //Starting Battle
        startingBattle += Fading.instance.FadingOut;
        startingBattle += CreateBattleArena.instance.DeactivateOverworldScripts;
        startingBattle += CreateBattleArena.instance.MakeBattleArena;
        startingBattle += CreateBattleArena.instance.StartCameraChange;

        //EndingBattle
        endBattle += Fading.instance.FadingOut;

    }

    public void StartBattle()
    {
         startingBattle();
    }

    public void EndBattle()
    {
       

    }
}
