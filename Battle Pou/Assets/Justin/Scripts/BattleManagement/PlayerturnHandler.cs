using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerturnHandler : StateHandler
{
    

    public override void HandleState()
    {
        PlayerTurn();
    }
    private void PlayerTurn()
    {
        battleText.text = "What will you do?";
        chooseScreen.SetActive(true);
    }
}
