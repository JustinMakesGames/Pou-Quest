using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingPlayerHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            PlayerHandler.Instance.coins = 9999;
            PlayerHandler.Instance.StatsOverworldChange();
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            PlayerHandler.Instance.hp = PlayerHandler.Instance.maxHp;
            PlayerHandler.Instance.sp = PlayerHandler.Instance.maxSp;
            PlayerHandler.Instance.StatsOverworldChange();
        }
    }
}
