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

    public delegate void FleeBattleDelegate();
    public event FleeBattleDelegate fleeBattle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        //Starting Battle
        startingBattle += Fading.instance.FadingOut;
        startingBattle += CreateBattleArena.instance.OverworldManagement;
        startingBattle += CreateBattleArena.instance.MakeBattleArena;
        startingBattle += CreateBattleArena.instance.StartCameraChange;

        //Ending Battle
        endBattle += Fading.instance.FadingOut;
        endBattle += EndBattle.instance.SettingUpDeletion;
        endBattle += EndBattle.instance.StartingCoroutines;
        endBattle += EndBattle.instance.DestroyEnemy;

        //Fleeing Battle
        fleeBattle += Fading.instance.FadingOut;
        fleeBattle += EndBattle.instance.SettingUpDeletion;
        fleeBattle += EndBattle.instance.StartingCoroutines;
        fleeBattle += EndBattle.instance.KeepEnemyAlife;
    }

    public void StartBattle()
    {
         startingBattle();
    }

    public void EndingBattle()
    {
        endBattle();
    }

    public void FleeingBattle()
    {
        fleeBattle();
    }
}
