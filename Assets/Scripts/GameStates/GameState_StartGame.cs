using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_StartGame : GameState_Base
{
    public override void EnterState(GameManager gameManager)
    {
        gameManager.SetAlertStatus(gameManager.StartingAlertLevel);
    }

    public override void UpdateState(GameManager gameManager)
    {

    }

    public override void ExitState(GameManager gameManager)
    {

    }
}
